using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kvdemy.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class test125 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "BookingMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "BookingMessages",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "BookingMessages");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "BookingMessages");
        }
    }
}
