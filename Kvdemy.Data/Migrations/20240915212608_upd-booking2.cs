using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kvdemy.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class updbooking2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRated",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRated",
                table: "Bookings");
        }
    }
}
