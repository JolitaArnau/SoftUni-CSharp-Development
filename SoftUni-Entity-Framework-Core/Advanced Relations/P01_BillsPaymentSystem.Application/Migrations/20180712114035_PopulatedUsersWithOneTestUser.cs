using Microsoft.EntityFrameworkCore.Migrations;

namespace P01_BillsPaymentSystem.Application.Migrations
{
    public partial class PopulatedUsersWithOneTestUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password" },
                values: new object[] { 1, "anna.winfried@gmail.com", "Anna", "Winfried", "mypasswordshouldnotbeaplainstring" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
