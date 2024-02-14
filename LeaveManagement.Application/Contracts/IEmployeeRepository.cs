using LeaveManagement.Common.Models;

namespace LeaveManagement.Application.Contracts
{
	public interface IEmployeeRepository
	{
		Task<List<EmployeeListVM>> GetEmployeeListsAsync();
	}
}
