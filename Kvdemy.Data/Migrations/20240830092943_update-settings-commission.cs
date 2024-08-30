using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kvdemy.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatesettingscommission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dollar",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Euro",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "Pound",
                table: "Settings",
                newName: "Commission");

            migrationBuilder.AddColumn<int>(
                name: "MinimumWithdrawal",
                table: "Settings",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinimumWithdrawal",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "Commission",
                table: "Settings",
                newName: "Pound");

            migrationBuilder.AddColumn<double>(
                name: "Dollar",
                table: "Settings",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Euro",
                table: "Settings",
                type: "float",
                nullable: true);
        }
    }
}
