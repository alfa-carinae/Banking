using Banking.CLI.Models;

namespace Banking.CLI
{
    public interface IConsoleInstance
    {
        Guid BankId { get; set; }
        UserContext UserContext { get; set; }

        void Start();
    }
}