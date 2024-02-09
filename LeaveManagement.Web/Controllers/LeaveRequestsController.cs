using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LeaveManagement.Data;
using LeaveManagement.Common.Models;
using LeaveManagement.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using LeaveManagement.Common.Constants;
using LeaveManagement.Application.Repositories;

namespace LeaveManagement.Web.Controllers
{
	[Authorize]
    public class LeaveRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
		private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILogger<LeaveRequestsController> _logger;

		public LeaveRequestsController(
            ApplicationDbContext context, 
            ILeaveRequestRepository leaveRequestRepository, 
            ILeaveAllocationRepository leaveAllocationRepository,
            ILeaveTypeRepository leaveTypeRepository,
            ILogger<LeaveRequestsController> logger
        )
        {
            _context = context;
            _leaveRequestRepository = leaveRequestRepository;
			_leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
		}

        [Authorize(Roles = Roles.Administrator)]
        // GET: LeaveRequests
        public async Task<IActionResult> Index()
        {
            var model = await _leaveRequestRepository.GetAdminLeaveRequestList();
            return View(model);
        }

        public async Task<ActionResult> MyLeave()
        {
            var model = await _leaveRequestRepository.GetMyLeaveDetails();
            return View(model);
        }

        // GET: LeaveRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var model = await _leaveRequestRepository.GetLeaveRequestAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveRequest(int id, bool approved)
        {
            try
            {
				await _leaveRequestRepository.ChangeApprovalStatus(id, approved);
			}
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Approving Request");
				throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: LeaveRequests/Create
        public async Task<IActionResult> CreateAsync()
        {
            var userLeaveTypes = await _leaveTypeRepository.GetLeaveTypeByLoggedInUserAsync();

            var model = new LeaveRequestCreateVM {
                LeaveTypes = new SelectList(userLeaveTypes, "Id", "Name")
            };

			return View(model);
        }

        // POST: LeaveRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isValidRequest = await _leaveRequestRepository.CreateLeaveRequest(model);
                    if (isValidRequest)
                        return RedirectToAction(nameof(MyLeave));
                    ModelState.AddModelError(string.Empty,  "You Have Exceeded Your Allocation With This Request.");
                }
            }
            catch (Exception ex)
            {
				_logger.LogError(ex, "Error Creating Leave Request");
				ModelState.AddModelError(string.Empty, "An Error Has Ocurred. Please Try Again Later");
            }
    
            model.LeaveTypes = new SelectList(_context.LeaveTypes, "Id", "Name", model.LeaveTypeId);
            return View(model);
        }

		// POST: LeaveRequests/CancelRequest/5
		[HttpPost, ActionName("CancelLeaveRequest")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CancelLeaveRequest(int id)
		{
			try
			{
				await _leaveRequestRepository.CancelLeaveRequest(id);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error Canceling Leave Request");
				throw;
			}

			return RedirectToAction(nameof(MyLeave));
		}
	}
}
