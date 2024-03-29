﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using LeaveManagement.Common.Constants;
using LeaveManagement.Application.Contracts;
using LeaveManagement.Data;
using LeaveManagement.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace LeaveManagement.Application.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<Employee> _userManager;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
		private readonly IEmailSender _emailSender;
		private readonly AutoMapper.IConfigurationProvider _configurationProvider;
		private readonly IMapper _mapper;

        public LeaveAllocationRepository(
            ApplicationDbContext context, 
            IHttpContextAccessor httpContextAccessor, 
            UserManager<Employee> userManager, 
            ILeaveTypeRepository leaveTypeRepository, 
            IEmailSender emailSender,
			AutoMapper.IConfigurationProvider configurationProvider,
            IMapper mapper) : base(context)
        {
            _context = context;
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
            _leaveTypeRepository = leaveTypeRepository;
			_emailSender = emailSender;
			_configurationProvider = configurationProvider;
			_mapper = mapper;
        }

        public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int period)
        {
            return await _context.LeaveAllocations.AnyAsync(q => q.EmployeeId == employeeId && q.LeaveTypeId == leaveTypeId && q.Period == period);
        }

        public async Task<EmployeeAllocationVM> GetEmployeeAllocations(string employeeId)
        {
            var employee = await _userManager.FindByIdAsync(employeeId);
            var allocations = await GetAllocationsByEmployee(employeeId);

            var employeeAllocationModel = _mapper.Map<EmployeeAllocationVM>(employee);
            employeeAllocationModel.LeaveAllocations = allocations;

            return employeeAllocationModel;
        }

        public async Task<List<LeaveAllocationVM>> GetAllocationsByEmployee(string employeeId) 
        {
            var allocations = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Where(q => q.EmployeeId == employeeId)
                .ProjectTo<LeaveAllocationVM>(_configurationProvider)
                .ToListAsync();

            return allocations;
        }


        public async Task<LeaveAllocationEditVM> GetEmployeeAllocation(int id)
        {
            var allocation = await _context.LeaveAllocations
                .Include(q => q.LeaveType) //SQL inner join
				.ProjectTo<LeaveAllocationEditVM>(_configurationProvider)
				.FirstOrDefaultAsync(q => q.Id == id);

            if (allocation == null)
            {
                return null;
            }

            var employee = await _userManager.FindByIdAsync(allocation.EmployeeId);

            var model = allocation;
            model.Employee = _mapper.Map<EmployeeListVM>(employee);

            return model;
        }

        public async Task LeaveAllocation(int leaveTypeId)
        {
            var employees = await _userManager.GetUsersInRoleAsync(Roles.User);
            var period = DateTime.Now.Year;
            var leaveType = await _leaveTypeRepository.GetAsync(leaveTypeId);
            var allocations = new List<LeaveAllocation>();
            var employeeWithNewAllocations = new List<Employee>();

            foreach (var employee in employees)
            {
                if (await AllocationExists(employee.Id, leaveTypeId, period))
                    continue;

                allocations.Add(new LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveTypeId,
                    Period = period,
                    NumberOfDays = leaveType.DefaultDays
                });

				employeeWithNewAllocations.Add(employee);
			}

            await AddRangeAsync(allocations);

            foreach (var employee in employeeWithNewAllocations)
            {
				await _emailSender.SendEmailAsync(employee.Email, $"Leave Allocation Posted for {period}", $"Your {leaveType.Name} " +
						$"has been posted for the period of {period}. You have been {leaveType.DefaultDays}.");
			}
		}

        public async Task<bool> UpdateEmployeeAllocation(LeaveAllocationEditVM model)
        {
            var leaveAllocation = await GetAsync(model.Id);
            if (leaveAllocation == null)
            {
                return false;
            }
            leaveAllocation.Period = model.Period;
            leaveAllocation.NumberOfDays = model.NumberOfDays;

            await UpdateAsync(leaveAllocation);

            return true;
        }

		public async Task<LeaveAllocation> GetEmployeeAllocation(string employeeId, int leaveTypeId)
		{
			return await _context.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == employeeId && q.LeaveTypeId == leaveTypeId);
		}

		public async Task<LeaveAllocation> GetEmployeeAllocationByContext(int leaveTypeId)
		{
			var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);

			return await _context.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == user.Id && q.LeaveTypeId == leaveTypeId);
		}

	}
}
