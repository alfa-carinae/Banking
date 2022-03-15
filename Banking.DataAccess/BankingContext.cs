#nullable disable
using Banking.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Banking.DataAccess
{
    public class BankingContext : DbContext
    {
        public BankingContext(DbContextOptions<BankingContext> options) : base(options) { }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<ServiceCharge> ServiceCharges { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.ConfigureWarnings(c => c.Log((RelationalEventId.CommandExecuting, LogLevel.None)));
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Guid bankId = Guid.NewGuid();
            modelBuilder.Entity<Bank>().HasData(new Bank
            {
                Id = bankId,
                Name = "Central Bank",
                Address = "",
                City = "",
                State = "",
                BranchName = "CB0",
                Phone = "0000000000"
            });
            modelBuilder.Entity<Staff>().HasData(new Staff
            {
                Id = Guid.NewGuid(),
                Name = "Central User",
                Address = "",
                City = "",
                State = "",
                Phone = "0000000000",
                BankId = bankId,
                Clearance = Enums.Clearance.Root
            });
        }
    }
}
