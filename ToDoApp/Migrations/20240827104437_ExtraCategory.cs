using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.Migrations
{
    /// <inheritdoc />
    public partial class ExtraCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[] { "other", "Other" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: "other");
        }
    }
}
