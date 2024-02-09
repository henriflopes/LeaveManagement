using LeaveManagement.Common.Models;
using LeaveManagement.Data;

namespace LeaveManagement.Application.Contracts
{
    public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
    {
        Task<List<LeaveTypeVM>> GetLeaveTypeByLoggedInUserAsync();
    }
}
