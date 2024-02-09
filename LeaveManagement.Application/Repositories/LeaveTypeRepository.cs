using AutoMapper;
using AutoMapper.QueryableExtensions;
using LeaveManagement.Application.Contracts;
using LeaveManagement.Common.Models;
using LeaveManagement.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Application.Repositories
{
	public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfigurationProvider _configurationProvider;
        private readonly UserManager<Employee> _userManager;

        public LeaveTypeRepository(
                ApplicationDbContext context,
                IHttpContextAccessor httpContextAccessor,
				IConfigurationProvider configurationProvider,
                UserManager<Employee> userManager
            ) : base(context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _configurationProvider = configurationProvider;
            _userManager = userManager;
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypeByLoggedInUserAsync()
        {
            List<LeaveTypeVM> userLeaveTypes = [];

            if (_httpContextAccessor == null)
                return userLeaveTypes;

			if (_httpContextAccessor.HttpContext == null)
				return userLeaveTypes;

			var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            if (user == null)
				return userLeaveTypes;

            userLeaveTypes.AddRange(await _context.LeaveAllocations.Include(q => q.LeaveType)
			  .Where(q => q.EmployeeId == user.Id)
              .Select(q => q.LeaveType)
              .ProjectTo<LeaveTypeVM>(_configurationProvider)
              .ToListAsync());

			return userLeaveTypes;
        }
    }
}
