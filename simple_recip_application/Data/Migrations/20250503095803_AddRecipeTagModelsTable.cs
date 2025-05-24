using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simple_recip_application.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipeTagModelsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecipeTagModels",
                columns: table => new
                {
                    RecipeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTagModels", x => new { x.RecipeId, x.TagId });
                    table.ForeignKey(
                        name: "FK_RecipeTagModels_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeTagModels_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTagModels_TagId",
                table: "RecipeTagModels",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeTagModels");
        }
    }
}
