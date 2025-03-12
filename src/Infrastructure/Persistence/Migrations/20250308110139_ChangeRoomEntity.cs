using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRoomEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmenityRoom",
                schema: "public");

            migrationBuilder.AddColumn<Guid[]>(
                name: "Amenities",
                schema: "public",
                table: "Rooms",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amenities",
                schema: "public",
                table: "Rooms");

            migrationBuilder.CreateTable(
                name: "AmenityRoom",
                schema: "public",
                columns: table => new
                {
                    AmenityId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenityRoom", x => new { x.AmenityId, x.RoomId });
                    table.ForeignKey(
                        name: "FK_AmenityRoom_Amenities_AmenityId",
                        column: x => x.AmenityId,
                        principalSchema: "public",
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmenityRoom_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalSchema: "public",
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmenityRoom_RoomId",
                schema: "public",
                table: "AmenityRoom",
                column: "RoomId");
        }
    }
}
