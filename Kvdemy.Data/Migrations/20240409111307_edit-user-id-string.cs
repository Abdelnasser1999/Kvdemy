using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kvdemy.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class edituseridstring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awards_AspNetUsers_UserId1",
                table: "Awards");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPhoneNumbers_AspNetUsers_UserId1",
                table: "ContactPhoneNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_Downloads_AspNetUsers_UserId1",
                table: "Downloads");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_AspNetUsers_UserId1",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_AspNetUsers_UserId1",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Galleries_AspNetUsers_UserId1",
                table: "Galleries");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLanguages_AspNetUsers_UserId1",
                table: "StudentLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSpecialty_AspNetUsers_UserId1",
                table: "UserSpecialty");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_UserId1",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_UserId1",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_UserSpecialty_UserId1",
                table: "UserSpecialty");

            migrationBuilder.DropIndex(
                name: "IX_StudentLanguages_UserId1",
                table: "StudentLanguages");

            migrationBuilder.DropIndex(
                name: "IX_Galleries_UserId1",
                table: "Galleries");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_UserId1",
                table: "Experiences");

            migrationBuilder.DropIndex(
                name: "IX_Educations_UserId1",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Downloads_UserId1",
                table: "Downloads");

            migrationBuilder.DropIndex(
                name: "IX_ContactPhoneNumbers_UserId1",
                table: "ContactPhoneNumbers");

            migrationBuilder.DropIndex(
                name: "IX_Awards_UserId1",
                table: "Awards");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserSpecialty");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "StudentLanguages");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "StudentLanguages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "RegistrationInfos");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Educations");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Downloads");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ContactPhoneNumbers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Awards");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Videos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserSpecialty",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "StudentLanguages",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Galleries",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Experiences",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Educations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Downloads",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ContactPhoneNumbers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Awards",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_UserId",
                table: "Videos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSpecialty_UserId",
                table: "UserSpecialty",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLanguages_UserId",
                table: "StudentLanguages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_UserId",
                table: "Galleries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_UserId",
                table: "Experiences",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_UserId",
                table: "Educations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Downloads_UserId",
                table: "Downloads",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPhoneNumbers_UserId",
                table: "ContactPhoneNumbers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_UserId",
                table: "Awards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Awards_AspNetUsers_UserId",
                table: "Awards",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPhoneNumbers_AspNetUsers_UserId",
                table: "ContactPhoneNumbers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Downloads_AspNetUsers_UserId",
                table: "Downloads",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AspNetUsers_UserId",
                table: "Educations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_AspNetUsers_UserId",
                table: "Experiences",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Galleries_AspNetUsers_UserId",
                table: "Galleries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLanguages_AspNetUsers_UserId",
                table: "StudentLanguages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSpecialty_AspNetUsers_UserId",
                table: "UserSpecialty",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_UserId",
                table: "Videos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awards_AspNetUsers_UserId",
                table: "Awards");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPhoneNumbers_AspNetUsers_UserId",
                table: "ContactPhoneNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_Downloads_AspNetUsers_UserId",
                table: "Downloads");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_AspNetUsers_UserId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_AspNetUsers_UserId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Galleries_AspNetUsers_UserId",
                table: "Galleries");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentLanguages_AspNetUsers_UserId",
                table: "StudentLanguages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSpecialty_AspNetUsers_UserId",
                table: "UserSpecialty");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_AspNetUsers_UserId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_UserId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_UserSpecialty_UserId",
                table: "UserSpecialty");

            migrationBuilder.DropIndex(
                name: "IX_StudentLanguages_UserId",
                table: "StudentLanguages");

            migrationBuilder.DropIndex(
                name: "IX_Galleries_UserId",
                table: "Galleries");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_UserId",
                table: "Experiences");

            migrationBuilder.DropIndex(
                name: "IX_Educations_UserId",
                table: "Educations");

            migrationBuilder.DropIndex(
                name: "IX_Downloads_UserId",
                table: "Downloads");

            migrationBuilder.DropIndex(
                name: "IX_ContactPhoneNumbers_UserId",
                table: "ContactPhoneNumbers");

            migrationBuilder.DropIndex(
                name: "IX_Awards_UserId",
                table: "Awards");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Videos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Videos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserSpecialty",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserSpecialty",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "StudentLanguages",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "StudentLanguages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "StudentLanguages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "RegistrationInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Galleries",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Galleries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Experiences",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Experiences",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Educations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Educations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Downloads",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Downloads",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ContactPhoneNumbers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ContactPhoneNumbers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Awards",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Awards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_UserId1",
                table: "Videos",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserSpecialty_UserId1",
                table: "UserSpecialty",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_StudentLanguages_UserId1",
                table: "StudentLanguages",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_UserId1",
                table: "Galleries",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_UserId1",
                table: "Experiences",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_UserId1",
                table: "Educations",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Downloads_UserId1",
                table: "Downloads",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPhoneNumbers_UserId1",
                table: "ContactPhoneNumbers",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_UserId1",
                table: "Awards",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Awards_AspNetUsers_UserId1",
                table: "Awards",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPhoneNumbers_AspNetUsers_UserId1",
                table: "ContactPhoneNumbers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Downloads_AspNetUsers_UserId1",
                table: "Downloads",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_AspNetUsers_UserId1",
                table: "Educations",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_AspNetUsers_UserId1",
                table: "Experiences",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Galleries_AspNetUsers_UserId1",
                table: "Galleries",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentLanguages_AspNetUsers_UserId1",
                table: "StudentLanguages",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSpecialty_AspNetUsers_UserId1",
                table: "UserSpecialty",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_AspNetUsers_UserId1",
                table: "Videos",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
