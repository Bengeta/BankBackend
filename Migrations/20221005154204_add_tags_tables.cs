using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VTBlockBackend.Migrations
{
    public partial class add_tags_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskTag_Task_TaskModelId",
                table: "TaskTag");

            migrationBuilder.DropIndex(
                name: "IX_TaskTag_TaskModelId",
                table: "TaskTag");

            migrationBuilder.DropColumn(
                name: "TaskModelId",
                table: "TaskTag");

            migrationBuilder.CreateTable(
                name: "UserMarketItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    MarketItemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMarketItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMarketItem_MarketItem_MarketItemId",
                        column: x => x.MarketItemId,
                        principalTable: "MarketItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMarketItem_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskTag_TaskId",
                table: "TaskTag",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMarketItem_MarketItemId",
                table: "UserMarketItem",
                column: "MarketItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMarketItem_UserId",
                table: "UserMarketItem",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTag_Task_TaskId",
                table: "TaskTag",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskTag_Task_TaskId",
                table: "TaskTag");

            migrationBuilder.DropTable(
                name: "UserMarketItem");

            migrationBuilder.DropIndex(
                name: "IX_TaskTag_TaskId",
                table: "TaskTag");

            migrationBuilder.AddColumn<int>(
                name: "TaskModelId",
                table: "TaskTag",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TaskTag_TaskModelId",
                table: "TaskTag",
                column: "TaskModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskTag_Task_TaskModelId",
                table: "TaskTag",
                column: "TaskModelId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
