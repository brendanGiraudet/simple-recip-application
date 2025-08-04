using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace simple_recip_application.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserEmailProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "CalendarUserAccessModels",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "CalendarUserAccessModels");
        }
    }
}
