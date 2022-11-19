using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VTBlockBackend.Migrations
{
    public partial class add_marketItem_image_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarketItemImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageId = table.Column<int>(type: "integer", nullable: false),
                    MarketItemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketItemImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketItemImage_ImageStorage_MarketItemId",
                        column: x => x.MarketItemId,
                        principalTable: "ImageStorage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketItemImage_MarketItem_ImageId",
                        column: x => x.ImageId,
                        principalTable: "MarketItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketItemImage_ImageId",
                table: "MarketItemImage",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketItemImage_MarketItemId",
                table: "MarketItemImage",
                column: "MarketItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketItemImage");
        }
    }
}
