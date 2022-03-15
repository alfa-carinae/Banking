using Banking.DataAccess;
using Banking.DataAccess.Models;
using Banking.Services.Contracts;

namespace Banking.Services.Providers;

public class StaffService : IStaffService
{
    private readonly BankingContext _context;
    private readonly IBankService _bankService;
    public StaffService(BankingContext context, IBankService bankService)
    {
        _context = context;
        _bankService = bankService;
    }

    public Staff AddStaff(Guid bankId, Staff staff)
    {
        Bank bank = _bankService.GetBank(bankId);
        bank.Staff.Add(staff);
        _context.SaveChanges();
        return staff;
    }

    public IEnumerable<Staff>? GetAllStaff(Guid bankId)
    {
        return _bankService.GetBank(bankId).Staff;
    }

    public Staff? GetStaff(Guid StaffId)
    {
        return _context.Staff.SingleOrDefault(Staff => Staff.Id == StaffId);
    }

    public Staff UpdateStaff(Staff Staff)
    {
        _context.Staff.Attach(Staff);
        _context.SaveChanges();
        return Staff;
    }
}
