using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kvdemy.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class addfinanceaccount2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinanceAccounts_UserId",
                table: "FinanceAccounts");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceAccounts_UserId",
                table: "FinanceAccounts",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinanceAccounts_UserId",
                table: "FinanceAccounts");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceAccounts_UserId",
                table: "FinanceAccounts",
                column: "UserId");
        }
    }
}
