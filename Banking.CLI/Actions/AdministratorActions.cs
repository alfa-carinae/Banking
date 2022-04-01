using Banking.DataAccess.Models;
using Banking.Services.Contracts;
using Spectre.Console;

namespace Banking.CLI.Actions;

public class AdministratorActions
{
    private readonly IStaffService staffService;
    private readonly IBankService bankService;

    public AdministratorActions(IStaffService staffService, IBankService bankService)
    {
        this.staffService = staffService;
        this.bankService = bankService;
    }
    public void Parse(ConsoleInstance instance, string choice)
    {
        switch (choice)
        {
            case "CreateStaff":
                CreateStaff(instance.BankId);
                break;
            case "EditStaffDetails":
                EditBankDetails();
                break;
            case "RemoveStaff":
                RemoveStaff();
                break;
            case "AddBank":
                AddBank();
                break;
            case "EditBankDetails":
                EditBankDetails();
                break;
            case "SetCurrentInstance":
                SetCurrentInstance(instance);
                break;
            default:
                AnsiConsole.MarkupLine("Invalid choice.");
                break;
        }
    }
    public Guid CreateStaff(Guid bankId)
    {
        var bank = bankService.GetBank(bankId);
        if (bank == null)
        {
            AnsiConsole.MarkupLine("Invalid bank ID.");
            return Guid.Empty;
        }
        AnsiConsole.MarkupLine("Please provide the staff details.");
        Staff staff = new()
        {
            Name = AnsiConsole.Ask<string>("Enter the staff's name:"),
            Phone = AnsiConsole.Ask<string>("Enter the staff's phone number:"),
            Address = AnsiConsole.Ask<string>("Enter the staff's address:"),
            City = AnsiConsole.Ask<string>("Enter the staff's city:"),
            State = AnsiConsole.Ask<string>("Enter the staff's state:"),
            Clearance = DataAccess.Enums.Clearance.Base
        };
        var staffId = staffService.AddStaff(bank, staff).Id;
        AnsiConsole.MarkupLine($"Staff ID is {staffId}.");

        return staffId;
    }
    public Guid EditStaffDetails()
    {
        Guid.TryParse(AnsiConsole.Ask<string>("Enter staff's ID:"), out var staffId);
        if (staffId == Guid.Empty)
        {
            AnsiConsole.MarkupLine("Invalid staff ID.");
            return Guid.Empty;
        }
        Staff? staff = staffService.GetStaff(staffId);
        if (staff == null)
        {
            AnsiConsole.MarkupLine("Invalid staff ID.");
            return Guid.Empty;
        }
        AnsiConsole.MarkupLine("Please provide the staff details.");
        staff.Name = AnsiConsole.Ask<string>("Enter the staff's name:");
        staff.Phone = AnsiConsole.Ask<string>("Enter the staff's phone number:");
        staff.Address = AnsiConsole.Ask<string>("Enter the staff's address:");
        staff.City = AnsiConsole.Ask<string>("Enter the staff's city:");
        staff.State = AnsiConsole.Ask<string>("Enter the staff's state:");
        staffService.UpdateStaff(staff);

        return staffId;
    }
    public void RemoveStaff()
    {
        throw new NotImplementedException();
    }
    public Guid AddBank()
    {
        AnsiConsole.MarkupLine("Please provide the bank details.");
        Bank bank = new()
        {
            Name = AnsiConsole.Ask<string>("Enter the bank's name:"),
            Phone = AnsiConsole.Ask<string>("Enter the bank's phone number:"),
            Address = AnsiConsole.Ask<string>("Enter the bank's address:"),
            City = AnsiConsole.Ask<string>("Enter the bank's city:"),
            State = AnsiConsole.Ask<string>("Enter the bank's state:"),
            BranchName = AnsiConsole.Ask<string>("Enter the branch name:")
        };
        var bankId = bankService.AddBank(bank).Id;
        AnsiConsole.MarkupLine($"Bank ID is {bankId}.");

        return bankId;
    }
    public Guid EditBankDetails()
    {
        Guid.TryParse(AnsiConsole.Ask<string>("Enter bank's ID:"), out var bankId);
        Bank? bank = bankService.GetBank(bankId);
        if (bank == null)
        {
            AnsiConsole.MarkupLine("Invalid bank ID.");
            return Guid.Empty;
        }
        AnsiConsole.MarkupLine("Please provide the bank details.");
        bank.Name = AnsiConsole.Ask<string>("Enter the bank's name:");
        bank.Phone = AnsiConsole.Ask<string>("Enter the bank's phone number:");
        bank.Address = AnsiConsole.Ask<string>("Enter the bank's address:");
        bank.City = AnsiConsole.Ask<string>("Enter the bank's city:");
        bank.State = AnsiConsole.Ask<string>("Enter the bank's state:");
        bankService.UpdateBank(bank);

        return bankId;
    }
    public Guid SetCurrentInstance(ConsoleInstance consoleInstance)
    {
        Guid.TryParse(AnsiConsole.Ask<string>("Enter bank's ID:"), out var bankId);
        Bank? bank = bankService.GetBank(bankId);
        if (bank == null)
        {
            AnsiConsole.MarkupLine("Invalid bank ID.");
            return Guid.Empty;
        }
        consoleInstance.BankId = bankId;
        AnsiConsole.MarkupLine($"Current instance's bank ID changed to {bank.BranchName}.");

        return bankId;
    }
}
