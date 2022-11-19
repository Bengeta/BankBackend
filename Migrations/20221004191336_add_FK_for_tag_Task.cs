using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VTBlockBackend.Migrations
{
    public partial class add_FK_for_tag_Task : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Tag_TagId",
                table: "Task",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Tag_TagId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_TagId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Task");
        }
    }
}
