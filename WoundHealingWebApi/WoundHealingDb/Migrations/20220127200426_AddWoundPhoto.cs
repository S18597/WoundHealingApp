using Microsoft.EntityFrameworkCore.Migrations;

namespace WoundHealingDb.Migrations
{
    public partial class AddWoundPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WoundPhoto",
                columns: table => new
                {
                    WoundPhotoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WoundId = table.Column<int>(nullable: false),
                    Filename = table.Column<string>(maxLength: 500, nullable: false),
                    FileData = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WoundPhoto_WoundPhotoId", x => x.WoundPhotoId);
                    table.ForeignKey(
                        name: "FK_WoundPhoto_Wound_WoundId",
                        column: x => x.WoundId,
                        principalTable: "Wound",
                        principalColumn: "WoundId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WoundPhoto_WoundId",
                table: "WoundPhoto",
                column: "WoundId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WoundPhoto");
        }
    }
}