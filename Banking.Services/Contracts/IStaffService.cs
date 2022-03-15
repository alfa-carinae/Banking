using Banking.DataAccess.Models;

namespace Banking.Services.Contracts;

public interface IStaffService
{
    Staff AddStaff(Guid bankId, Staff staff);
    IEnumerable<Staff>? GetAllStaff(Guid bankId);
    Staff? GetStaff(Guid staffId);
    Staff UpdateStaff(Staff staff);
}
