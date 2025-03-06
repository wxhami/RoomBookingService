using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeReservationConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrganizerId",
                schema: "public",
                table: "Reservations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_OrganizerId",
                schema: "public",
                table: "Reservations",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_OrganizerId",
                schema: "public",
                table: "Reservations",
                column: "OrganizerId",
                principalSchema: "public",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_OrganizerId",
                schema: "public",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_OrganizerId",
                schema: "public",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                schema: "public",
                table: "Reservations");
        }
    }
}
