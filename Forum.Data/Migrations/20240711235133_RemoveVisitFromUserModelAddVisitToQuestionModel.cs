using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveVisitFromUserModelAddVisitToQuestionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visit",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Visit",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visit",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "Visit",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
