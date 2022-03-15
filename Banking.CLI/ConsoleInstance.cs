#nullable disable
using Banking.CLI.Actions;
using Banking.CLI.Models;
using Banking.DataAccess.Models;
using Banking.Services.Contracts;
using Spectre.Console;

namespace Banking.CLI;

public class ConsoleInstance : IConsoleInstance
{
    private readonly ICustomerService customerService;
    private readonly IStaffService staffService;
    private readonly CustomerActions customerActions;
    private readonly StaffActions staffActions;
    private readonly AdministratorActions administratorActions;
    private readonly RootActions rootActions;

    public Guid BankId { get; set; }
    public UserContext UserContext { get; set; }
    public ConsoleInstance(
        ICustomerService customerService,
        CustomerActions customerActions,
        StaffActions staffActions,
        IStaffService staffService,
        AdministratorActions administratorActions,
        RootActions rootActions)
    {
        this.customerService = customerService;
        this.customerActions = customerActions;
        this.staffActions = staffActions;
        this.staffService = staffService;
        this.administratorActions = administratorActions;
        this.rootActions = rootActions;
    }
    public void Start()
    {
        while (true)
        {
            Guid.TryParse(AnsiConsole.Ask<string>("Enter your user ID:"), out var userId);
            if (userId == Guid.Empty)
            {
                AnsiConsole.MarkupLine("Invalid user ID.");
                return;
            }
            if (customerService.GetCustomer(userId) is not null)
            {
                while (true)
                {
                    var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .PageSize(10)
                        .Title("Welcome! Choose an option to proceed:")
                        .AddChoices(Enum.GetNames(typeof(Enums.CustomerOptions)))
                        .AddChoices(Enum.GetNames(typeof(Enums.GlobalOptions))));
                    if (choice == "Logout") return;
                    customerActions.Parse(choice);
                }
            }
            else if (staffService.GetStaff(userId) is not null)
            {
                Staff staff = staffService.GetStaff(userId);
                if (staff.Clearance == DataAccess.Enums.Clearance.Base)
                {
                    while (true)
                    {
                        var choice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                        .PageSize(10)
                        .Title("Welcome! Choose an option to proceed:")
                        .AddChoices(Enum.GetNames(typeof(Enums.StaffOptions)))
                        .AddChoices(Enum.GetNames(typeof(Enums.GlobalOptions))));
                        if (choice == "Logout") return;
                        staffActions.Parse(BankId, choice);
                    }
                }
                else if (staff.Clearance == DataAccess.Enums.Clearance.Administrator)
                {
                    while (true)
                    {
                        var choice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .PageSize(10)
                            .Title("Welcome! Choose an option to proceed:")
                            .AddChoices(Enum.GetNames(typeof(Enums.AdministratorOptions)))
                            .AddChoices(Enum.GetNames(typeof(Enums.GlobalOptions))));
                        if (choice == "Logout") return;
                        administratorActions.Parse(this, choice);
                    }
                }
                else if (staff.Clearance == DataAccess.Enums.Clearance.Root)
                {
                    while (true)
                    {
                        var choice = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .PageSize(10)
                            .Title("Welcome! Choose an option to proceed:")
                            .AddChoices(Enum.GetNames(typeof(Enums.RootOptions)))
                            .AddChoices(Enum.GetNames(typeof(Enums.GlobalOptions))));
                        if (choice == "Logout") return;
                        rootActions.Parse(BankId, choice);
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("Staff clearance error. " +
                        "Please contact an administrator and verify if your profile has been properly created.");
                }
            }
            else
            {
                AnsiConsole.MarkupLine("User not found!");
            }
        }
    }
}
