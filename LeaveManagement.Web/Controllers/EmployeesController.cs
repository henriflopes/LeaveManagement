using AutoMapper;
using LeaveManagement.Application.Contracts;
using LeaveManagement.Data;
using LeaveManagement.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace LeaveManagement.Web.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly UserManager<Employee> _userManager;
		private readonly IMapper _mapper;
		private readonly ILeaveAllocationRepository _leaveAllocationRepository;
		private readonly ILeaveTypeRepository _leaveTypeRepository;
		private readonly IMemoryCache _memoryCache;
		private readonly IEmployeeRepository _employeeRepository;

		public EmployeesController(UserManager<Employee> userManager, IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository, ILeaveTypeRepository leaveTypeRepository, IMemoryCache memoryCache, IEmployeeRepository employeeRepository)
		{
			_userManager = userManager;
			_mapper = mapper;
			_leaveAllocationRepository = leaveAllocationRepository;
			_leaveTypeRepository = leaveTypeRepository;
			_memoryCache = memoryCache;
			_employeeRepository = employeeRepository;
		}

		// GET: EmployeesController
		public async Task<IActionResult> Index()
		{
			return View(await _employeeRepository.GetEmployeeListsAsync());
		}

		// GET: EmployeesController/ViewAllocations/employeeId
		public async Task<IActionResult> ViewAllocations(string id)
		{
			var model = await _leaveAllocationRepository.GetEmployeeAllocations(id);
			return View(model);
		}

		// GET: EmployeesController/EditAllocation/5
		public async Task<ActionResult> EditAllocation(int id)
		{
			var model = await _leaveAllocationRepository.GetEmployeeAllocation(id);
			if (model == null)
			{
				return NotFound();
			}
			return View(model);
		}

		// POST: EmployeesController/EditAllocation/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> EditAllocation(int id, LeaveAllocationEditVM model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					if (await _leaveAllocationRepository.UpdateEmployeeAllocation(model))
					{
						return RedirectToAction(nameof(ViewAllocations), new { id = model.EmployeeId });
					}
				}
			}
			catch
			{
				ModelState.AddModelError(string.Empty, "An Error Has Occoured. Please Try Again Later.");
			}

			model.Employee = _mapper.Map<EmployeeListVM>(await _userManager.FindByIdAsync(model.EmployeeId));
			model.LeaveType = _mapper.Map<LeaveTypeVM>(await _leaveTypeRepository.GetAsync(model.LeaveTypeId));
			return View(model);
		}
	}
}
