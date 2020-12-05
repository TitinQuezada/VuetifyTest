using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class updateSystemUserConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SystemUsers_Email",
                table: "SystemUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemUsers_Username",
                table: "SystemUsers",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SystemUsers_Email",
                table: "SystemUsers");

            migrationBuilder.DropIndex(
                name: "IX_SystemUsers_Username",
                table: "SystemUsers");
        }
    }
}
