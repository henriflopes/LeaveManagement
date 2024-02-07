namespace LeaveManagement.Web.Models
{
	public class EmployeeLeaveRequestViewVM
    {
        public EmployeeLeaveRequestViewVM(
            List<LeaveAllocationVM> leaveAllocations, 
            List<LeaveRequestVM> leaveRequests,
			List<LeaveRequestVM> leaveRequestsHistory
			)
        {
            LeaveAllocations = leaveAllocations;
            LeaveRequests = leaveRequests;
            LeaveRequestsHistory = leaveRequestsHistory;
        }

        public List<LeaveAllocationVM> LeaveAllocations { get; set; }

        public List<LeaveRequestVM> LeaveRequests { get; set; }

		public List<LeaveRequestVM> LeaveRequestsHistory { get; set; }
	}
}