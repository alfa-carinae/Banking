using Banking.DataAccess.Models;

namespace Banking.Services.Contracts;

public interface IStaffService
{
    Staff AddStaff(Bank bank, Staff staff);
    IEnumerable<Staff> GetAllStaff();
    IEnumerable<Staff> GetAllStaff(Bank bank);
    Staff? GetStaff(Guid staffId);
    Staff UpdateStaff(Staff staff);
}
