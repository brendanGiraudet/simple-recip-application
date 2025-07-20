using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simple_recip_application.Data.Migrations
{
    /// <inheritdoc />
    public partial class CalendarModelsAndCalendarUserAccessModelsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PlanifiedRecipes",
                newName: "CalendarId");

            migrationBuilder.CreateTable(
                name: "CalendarModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalendarUserAccessModels",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    CalendarId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarUserAccessModels", x => new { x.CalendarId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CalendarUserAccessModels_CalendarModels_CalendarId",
                        column: x => x.CalendarId,
                        principalTable: "CalendarModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.Sql(@"DELETE FROM ""PlanifiedRecipes""");

            migrationBuilder.CreateIndex(
                name: "IX_PlanifiedRecipes_CalendarId",
                table: "PlanifiedRecipes",
                column: "CalendarId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanifiedRecipes_CalendarModels_CalendarId",
                table: "PlanifiedRecipes",
                column: "CalendarId",
                principalTable: "CalendarModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanifiedRecipes_CalendarModels_CalendarId",
                table: "PlanifiedRecipes");

            migrationBuilder.DropTable(
                name: "CalendarUserAccessModels");

            migrationBuilder.DropTable(
                name: "CalendarModels");

            migrationBuilder.DropIndex(
                name: "IX_PlanifiedRecipes_CalendarId",
                table: "PlanifiedRecipes");

            migrationBuilder.RenameColumn(
                name: "CalendarId",
                table: "PlanifiedRecipes",
                newName: "UserId");
        }
    }
}
