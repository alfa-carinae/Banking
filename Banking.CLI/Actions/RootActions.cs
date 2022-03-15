using Banking.DataAccess.Models;
using Banking.Services.Contracts;
using Spectre.Console;

namespace Banking.CLI.Actions;

public class RootActions
{
    private readonly IStaffService staffService;

    public RootActions(IStaffService staffService)
    {
        this.staffService = staffService;
    }
    public void Parse(Guid bankId, string choice)
    {
        switch (choice)
        {
            case "CreateAdministrator":
                CreateAdministrator(bankId);
                break;
            case "EditAdministratorDetails":
                EditAdministratorDetails();
                break;
            case "RemoveAdministrator":
                RemoveAdministrator();
                break;
            default:
                AnsiConsole.MarkupLine("Invalid choice.");
                break;
        }
    }
    public Guid CreateAdministrator(Guid bankId)
    {
        AnsiConsole.MarkupLine("Please provide the administrator details.");
        Staff admin = new()
        {
            Name = AnsiConsole.Ask<string>("Enter the administrator's name:"),
            Phone = AnsiConsole.Ask<string>("Enter the administrator's phone number:"),
            Address = AnsiConsole.Ask<string>("Enter the administrator's address:"),
            City = AnsiConsole.Ask<string>("Enter the administrator's city:"),
            State = AnsiConsole.Ask<string>("Enter the administrator's state:"),
            Clearance = DataAccess.Enums.Clearance.Base
        };
        var adminId = staffService.AddStaff(bankId, admin).Id;
        AnsiConsole.MarkupLine($"Administrator ID is {adminId}.");

        return adminId;
    }
    public Guid? EditAdministratorDetails()
    {
        if (Guid.TryParse(AnsiConsole.Ask<string>("Enter the administrator's ID:"), out var adminId) == false)
        {
            AnsiConsole.MarkupLine("Invalid administrator ID.");
            return Guid.Empty;
        }
        Staff? admin = staffService.GetStaff(adminId);
        if (admin == null || admin.Clearance != DataAccess.Enums.Clearance.Administrator)
        {
            AnsiConsole.MarkupLine("Invalid administrator ID.");
            return Guid.Empty;
        }
        AnsiConsole.MarkupLine("Please provide the administrator details.");
        admin.Name = AnsiConsole.Ask<string>("Enter the administrator's name:");
        admin.Phone = AnsiConsole.Ask<string>("Enter the administrator's phone number:");
        admin.Address = AnsiConsole.Ask<string>("Enter the administrator's address:");
        admin.City = AnsiConsole.Ask<string>("Enter the administrator's city:");
        admin.State = AnsiConsole.Ask<string>("Enter the administrator's state:");
        staffService.UpdateStaff(admin);

        return adminId;
    }
    public string RemoveAdministrator()
    {
        throw new NotImplementedException();
    }
}
