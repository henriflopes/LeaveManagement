﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using LeaveManagement.Application.Contracts;
using LeaveManagement.Data;
using LeaveManagement.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace LeaveManagement.Application.Repositories
{
	public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
	{
		private readonly ApplicationDbContext _context;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ILeaveAllocationRepository _leaveAllocationRepository;
		private readonly UserManager<Employee> _userManager;
		private readonly AutoMapper.IConfigurationProvider _configurationProvider;
		private readonly IEmailSender _emailSender;

		public LeaveRequestRepository(
			ApplicationDbContext context,
			IMapper mapper,
			IHttpContextAccessor httpContextAccessor,
			ILeaveAllocationRepository leaveAllocationRepository,
			AutoMapper.IConfigurationProvider configurationProvider,
			IEmailSender emailSender,
			UserManager<Employee> userManager) : base(context)
		{
			_context = context;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
			_leaveAllocationRepository = leaveAllocationRepository;
			_userManager = userManager;
			_configurationProvider = configurationProvider;
			_emailSender = emailSender;
		}

		public async Task CancelLeaveRequest(int leaveRequestId)
		{
			var leaveRequest = await GetAsync(leaveRequestId);

			if (leaveRequest != null)
			{
				leaveRequest.Cancelled = true;
				await UpdateAsync(leaveRequest);

				var user = await _userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId);

				await _emailSender.SendEmailAsync(user.Email, $"Leave Request Cancelled", $"You leave request from " +
							$"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been Cancelled Successfully");
			}
		}

		public async Task ChangeApprovalStatus(int leaveRequestId, bool approved)
		{
			var leaveRequest = await GetAsync(leaveRequestId);
			leaveRequest.Approved = approved;

			if (approved)
			{
				var allocation = await _leaveAllocationRepository.GetEmployeeAllocation(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
				int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
				allocation.NumberOfDays -= (daysRequested + 1);

				await _leaveAllocationRepository.UpdateAsync(allocation);
			}

			await UpdateAsync(leaveRequest);

			var user = await _userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId);
			var approvalStatus = approved ? "Approved" : "Declined";

			await _emailSender.SendEmailAsync(user.Email, $"Leave Request {approvalStatus}", $"You leave request from " +
						$"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been {approvalStatus}");
		}

		public async Task<bool> CreateLeaveRequest(LeaveRequestCreateVM model)
		{
			var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);

			var leaveAllocation = await _leaveAllocationRepository.GetEmployeeAllocation(user.Id, model.LeaveTypeId);
			if (leaveAllocation == null)
				return false;

			var daysRequested = (int)(model.EndDate.Value - model.StartDate.Value).TotalDays + 1;

			if (daysRequested > leaveAllocation.NumberOfDays)
				return false;

			var leaveRequest = _mapper.Map<LeaveRequest>(model);
			leaveRequest.DateRequested = DateTime.Now;
			leaveRequest.RequestingEmployeeId = user.Id;

			await AddAsync(leaveRequest);

			await _emailSender.SendEmailAsync(user.Email, "Leave Request Submitted Sucessfully", $"You leave request from " +
				$"{leaveRequest.StartDate} to {leaveRequest.EndDate} has been submitted for approval");

			return true;
		}

		public async Task<AdminLeaveRequestsViewVM> GetAdminLeaveRequestList()
		{
			var leaveRequests = await _context.LeaveRequests
					.Include(q => q.LeaveType)
					.ProjectTo<LeaveRequestVM>(_configurationProvider)
					.ToListAsync();

			var model = new AdminLeaveRequestsViewVM
			{
				TotalRequests = leaveRequests.Count,
				ApprovedRequests = leaveRequests.Count(q => q.Approved == true),
				PendingRequests = leaveRequests.Count(q => q.Approved == null),
				RejectedRequests = leaveRequests.Count(q => q.Approved == false),
				LeaveRequest = leaveRequests
			};

			foreach (var leaveRequest in model.LeaveRequest)
			{
				leaveRequest.Employee = _mapper.Map<EmployeeListVM>(await _userManager.FindByIdAsync(leaveRequest.RequestingEmployeeId));
			}

			return model;
		}

		public async Task<List<LeaveRequestVM>> GetAllByIdAsync(string employeeId)
		{
			return await _context.LeaveRequests
				.Where(q => q.RequestingEmployeeId == employeeId)
				.ProjectTo<LeaveRequestVM>(_configurationProvider)
				.ToListAsync();
		}

		public async Task<LeaveRequestVM> GetLeaveRequestAsync(int? id)
		{
			var leaveRequest = await _context.LeaveRequests
				.Include(q => q.LeaveType)
				.ProjectTo<LeaveRequestVM>(_configurationProvider)
				.FirstOrDefaultAsync(q => q.Id == id);


			if (leaveRequest == null) { return null; }

			var model = leaveRequest;
			model.Employee = _mapper.Map<EmployeeListVM>(await _userManager.FindByIdAsync(leaveRequest?.RequestingEmployeeId));
			return model;
		}

		public async Task<EmployeeLeaveRequestViewVM> GetMyLeaveDetails()
		{
			var user = await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User);
			var allocations = await _leaveAllocationRepository.GetAllocationsByEmployee(user.Id);
			var allRequests = await GetAllByIdAsync(user.Id);

			allRequests.ForEach(q => { q.NumberOfDays = (int)((q.EndDate.Value - q.StartDate.Value).TotalDays) + 1; });

			var requests = allRequests.Where(q => q.Approved == null && q.Cancelled == false).ToList();
			var requestsHistories = allRequests.Where(q => q.Approved != null || q.Cancelled == true).ToList();

			requestsHistories.ForEach(q => { q.HideCancelButton = true; });

			var model = new EmployeeLeaveRequestViewVM(allocations, requests, requestsHistories);

			return model;
		}
	}
}
