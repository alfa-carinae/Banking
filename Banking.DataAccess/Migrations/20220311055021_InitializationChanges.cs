using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Banking.DataAccess.Migrations
{
    public partial class InitializationChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("f83b2ad8-2e63-469f-b280-c42ad8b21f19"));

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: new Guid("3da987bc-6b2d-4435-abc1-d6686a2f98a3"));

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "Address", "BranchName", "City", "Name", "Phone", "State" },
                values: new object[] { new Guid("7f1ce02c-727d-4a58-810f-69774a6da91b"), "", "CB0", "", "Central Bank", "0000000000", "" });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Address", "BankId", "City", "Clearance", "Name", "Phone", "State" },
                values: new object[] { new Guid("8babb7ee-6d27-47ce-8f03-eea7178e9ffa"), "", new Guid("7f1ce02c-727d-4a58-810f-69774a6da91b"), "", 3, "Central User", "0000000000", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("8babb7ee-6d27-47ce-8f03-eea7178e9ffa"));

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: new Guid("7f1ce02c-727d-4a58-810f-69774a6da91b"));

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "Address", "BranchName", "City", "Name", "Phone", "State" },
                values: new object[] { new Guid("3da987bc-6b2d-4435-abc1-d6686a2f98a3"), "", "CB0", "", "Central Bank", "0000000000", "" });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Address", "BankId", "City", "Clearance", "Name", "Phone", "State" },
                values: new object[] { new Guid("f83b2ad8-2e63-469f-b280-c42ad8b21f19"), "", new Guid("3da987bc-6b2d-4435-abc1-d6686a2f98a3"), "", 3, "Central User", "0000000000", "" });
        }
    }
}
