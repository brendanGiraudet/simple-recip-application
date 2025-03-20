using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simple_recip_application.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanifiedRecipesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanifiedRecipes",
                columns: table => new
                {
                    RecipeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlanifiedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    MomentOftheDay = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanifiedRecipes", x => new { x.RecipeId, x.PlanifiedDateTime, x.UserId });
                    table.ForeignKey(
                        name: "FK_PlanifiedRecipes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanifiedRecipes");
        }
    }
}
