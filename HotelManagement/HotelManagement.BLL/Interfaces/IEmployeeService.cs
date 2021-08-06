using HotelManagement.BLL.DTO;

namespace HotelManagement.BLL.Interfaces
{
	public interface IEmployeeService : IDataService<EmployeeDTO>
	{
		EmployeeDTO FindByEmail(string email);
	}
}
