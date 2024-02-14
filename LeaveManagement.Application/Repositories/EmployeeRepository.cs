using AutoMapper;
using LeaveManagement.Application.Contracts;
using LeaveManagement.Common.Constants;
using LeaveManagement.Common.Models;
using LeaveManagement.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace LeaveManagement.Application.Repositories
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly UserManager<Employee> _userManager;
		private readonly IMemoryCache _memoryCache;
		private readonly IMapper _mapper;

		public EmployeeRepository(UserManager<Employee> userManager, IMemoryCache memoryCache, IMapper mapper)
		{
			_userManager = userManager;
			_memoryCache = memoryCache;
			_mapper = mapper;
		}

		public async Task<List<EmployeeListVM>> GetEmployeeListsAsync()
		{
			_memoryCache.TryGetValue("CachedUsersInRoleAsync", out List<EmployeeListVM> model);

			if (model == null)
			{
				var employees = await _userManager.GetUsersInRoleAsync(Roles.User);
				model = _mapper.Map<List<EmployeeListVM>>(employees);

				var cacheEntryOptions = new MemoryCacheEntryOptions().
					SetSlidingExpiration(TimeSpan.FromSeconds(20))
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(600))
					.SetPriority(CacheItemPriority.Normal);

				_memoryCache.Set("CachedUsersInRoleAsync", model, cacheEntryOptions);
			}

			return model;
		}
	}
}
