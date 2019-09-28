using Microsoft.EntityFrameworkCore.Migrations;

namespace berua.DAL.Migrations
{
    public partial class FixUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "Users",
                newName: "Domain");

            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                table: "Users",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "Salt");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Domain",
                table: "Users",
                newName: "Login");
        }
    }
}
