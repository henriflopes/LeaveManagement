using LeaveManagement.Data;
using LeaveManagement.Common.Models;

namespace LeaveManagement.Application.Contracts
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
		Task<bool> CreateLeaveRequest(LeaveRequestCreateVM model);
        Task<EmployeeLeaveRequestViewVM> GetMyLeaveDetails();
        Task<List<LeaveRequestVM>> GetAllByIdAsync(string employeeId);
        Task ChangeApprovalStatus(int leaveRequestId, bool approved);
        Task<AdminLeaveRequestsViewVM> GetAdminLeaveRequestList();
		Task<LeaveRequestVM> GetLeaveRequestAsync(int? id);
        Task CancelLeaveRequest(int leaveRequestId);
	}
}
