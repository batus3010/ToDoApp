using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPriority : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PriorityId",
                table: "ToDos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    PriorityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PriorityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.PriorityId);
                });

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "PriorityId", "PriorityName" },
                values: new object[,]
                {
                    { "high", "High" },
                    { "low", "Low" },
                    { "normal", "Normal" }
                });

			// Step 3: Ensure existing ToDos have a valid PriorityId
			migrationBuilder.Sql("UPDATE ToDos SET PriorityId = 'normal' WHERE PriorityId IS NULL OR PriorityId = ''");

			migrationBuilder.CreateIndex(
                name: "IX_ToDos_PriorityId",
                table: "ToDos",
                column: "PriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_Priorities_PriorityId",
                table: "ToDos",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "PriorityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_Priorities_PriorityId",
                table: "ToDos");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_PriorityId",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "ToDos");
        }
    }
}
