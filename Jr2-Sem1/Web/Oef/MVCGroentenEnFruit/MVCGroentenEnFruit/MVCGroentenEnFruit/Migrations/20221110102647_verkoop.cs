using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCGroentenEnFruit.Migrations
{
    public partial class verkoop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "VerkoopOrders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VerkoopOrders_IdentityUserId",
                table: "VerkoopOrders",
                column: "IdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VerkoopOrders_AspNetUsers_IdentityUserId",
                table: "VerkoopOrders",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerkoopOrders_AspNetUsers_IdentityUserId",
                table: "VerkoopOrders");

            migrationBuilder.DropIndex(
                name: "IX_VerkoopOrders_IdentityUserId",
                table: "VerkoopOrders");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "VerkoopOrders");
        }
    }
}
