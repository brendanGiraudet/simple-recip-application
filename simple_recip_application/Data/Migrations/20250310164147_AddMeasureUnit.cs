using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simple_recip_application.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMeasureUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MeasureUnit",
                table: "Ingredients",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MeasureUnit",
                table: "Ingredients");
        }
    }
}
