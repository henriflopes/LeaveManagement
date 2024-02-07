﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;
using LeaveManagement.Web.Contracts;
using Microsoft.AspNetCore.Authorization;
using LeaveManagement.Web.Constants;

namespace LeaveManagement.Web.Controllers
{
	[Authorize]
    public class LeaveRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
		private readonly ILeaveAllocationRepository _leaveAllocationRepository;

		public LeaveRequestsController(ApplicationDbContext context, ILeaveRequestRepository leaveRequestRepository, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _context = context;
            _leaveRequestRepository = leaveRequestRepository;
			_leaveAllocationRepository = leaveAllocationRepository;
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
            catch (Exception)
            {

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: LeaveRequests/Create
        public IActionResult CreateAsync()
        {
            var model = new LeaveRequestCreateVM {
                LeaveTypes = new SelectList(_context.LeaveTypes, "Id", "Name")
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
            catch
            {
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
			catch
			{
				throw;
			}

			return RedirectToAction(nameof(MyLeave));
		}
	}
}