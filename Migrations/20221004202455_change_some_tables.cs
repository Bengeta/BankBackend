using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VTBlockBackend.Migrations
{
    public partial class change_some_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketItem_MarketItem_CategoryId",
                table: "MarketItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Tag_TagId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_TagId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_MarketItem_CategoryId",
                table: "MarketItem");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Task");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "MarketItem",
                newName: "Category");

            migrationBuilder.CreateTable(
                name: "MarketItemTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MarketItemId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketItemTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketItemTag_MarketItem_MarketItemId",
                        column: x => x.MarketItemId,
                        principalTable: "MarketItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketItemTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskTag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    TaskModelId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskTag_Task_TaskModelId",
                        column: x => x.TaskModelId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketItemTag_MarketItemId",
                table: "MarketItemTag",
                column: "MarketItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketItemTag_TagId",
                table: "MarketItemTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTag_TagId",
                table: "TaskTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTag_TaskModelId",
                table: "TaskTag",
                column: "TaskModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketItemTag");

            migrationBuilder.DropTable(
                name: "TaskTag");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "MarketItem",
                newName: "CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "TagId",
                table: "Task",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Task_TagId",
                table: "Task",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketItem_CategoryId",
                table: "MarketItem",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketItem_MarketItem_CategoryId",
                table: "MarketItem",
                column: "CategoryId",
                principalTable: "MarketItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Tag_TagId",
                table: "Task",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
