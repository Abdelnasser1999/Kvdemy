using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kvdemy.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserSpecialty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSpecialty_AspNetUsers_UserId",
                table: "UserSpecialty");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSpecialty_Services_ServiceId",
                table: "UserSpecialty");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSpecialty_Specialties_SpecialtyId",
                table: "UserSpecialty");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Specialties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSpecialty",
                table: "UserSpecialty");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "UserSpecialty");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "UserSpecialty");

            migrationBuilder.RenameTable(
                name: "UserSpecialty",
                newName: "UserSpecialties");

            migrationBuilder.RenameColumn(
                name: "SpecialtyId",
                table: "UserSpecialties",
                newName: "SubcategoryId");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "UserSpecialties",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSpecialty_UserId",
                table: "UserSpecialties",
                newName: "IX_UserSpecialties_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSpecialty_SpecialtyId",
                table: "UserSpecialties",
                newName: "IX_UserSpecialties_SubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSpecialty_ServiceId",
                table: "UserSpecialties",
                newName: "IX_UserSpecialties_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSpecialties",
                table: "UserSpecialties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSpecialties_AspNetUsers_UserId",
                table: "UserSpecialties",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSpecialties_Categories_CategoryId",
                table: "UserSpecialties",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSpecialties_Categories_SubcategoryId",
                table: "UserSpecialties",
                column: "SubcategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSpecialties_AspNetUsers_UserId",
                table: "UserSpecialties");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSpecialties_Categories_CategoryId",
                table: "UserSpecialties");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSpecialties_Categories_SubcategoryId",
                table: "UserSpecialties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSpecialties",
                table: "UserSpecialties");

            migrationBuilder.RenameTable(
                name: "UserSpecialties",
                newName: "UserSpecialty");

            migrationBuilder.RenameColumn(
                name: "SubcategoryId",
                table: "UserSpecialty",
                newName: "SpecialtyId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "UserSpecialty",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSpecialties_UserId",
                table: "UserSpecialty",
                newName: "IX_UserSpecialty_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSpecialties_SubcategoryId",
                table: "UserSpecialty",
                newName: "IX_UserSpecialty_SpecialtyId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSpecialties_CategoryId",
                table: "UserSpecialty",
                newName: "IX_UserSpecialty_ServiceId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UserSpecialty",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "UserSpecialty",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSpecialty",
                table: "UserSpecialty",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecialtyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Specialties_SpecialtyId",
                        column: x => x.SpecialtyId,
                        principalTable: "Specialties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Services_SpecialtyId",
                table: "Services",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSpecialty_AspNetUsers_UserId",
                table: "UserSpecialty",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSpecialty_Services_ServiceId",
                table: "UserSpecialty",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSpecialty_Specialties_SpecialtyId",
                table: "UserSpecialty",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
