using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simple_recip_application.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPantryIngredients");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Image = table.Column<byte[]>(type: "BLOB", nullable: false),
                    MeasureUnit = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RemoveDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.CreateTable(
                name: "UserPantryIngredients",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    IngredientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPantryIngredients", x => new { x.UserId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_UserPantryIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPantryIngredients_IngredientId",
                table: "UserPantryIngredients",
                column: "IngredientId");
        }
    }
}
