using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameClub",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameClub", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameClubEvent",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", fixedLength: true, maxLength: 32, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ScheduledAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClubId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameClubEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameClubEvent_GameClub_ClubId",
                        column: x => x.ClubId,
                        principalTable: "GameClub",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameClub_Name",
                table: "GameClub",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameClubEvent_ClubId",
                table: "GameClubEvent",
                column: "ClubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameClubEvent");

            migrationBuilder.DropTable(
                name: "GameClub");
        }
    }
}
