using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kvdemy.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class edituser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RegistrationInfos_RegistrationInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RegistrationInfoId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "RegistrationInfoId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RegistrationInfoId",
                table: "AspNetUsers",
                column: "RegistrationInfoId",
                unique: true,
                filter: "[RegistrationInfoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RegistrationInfos_RegistrationInfoId",
                table: "AspNetUsers",
                column: "RegistrationInfoId",
                principalTable: "RegistrationInfos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_RegistrationInfos_RegistrationInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RegistrationInfoId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "RegistrationInfoId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RegistrationInfoId",
                table: "AspNetUsers",
                column: "RegistrationInfoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_RegistrationInfos_RegistrationInfoId",
                table: "AspNetUsers",
                column: "RegistrationInfoId",
                principalTable: "RegistrationInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
