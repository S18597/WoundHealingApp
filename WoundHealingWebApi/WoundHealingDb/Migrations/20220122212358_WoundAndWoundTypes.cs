using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WoundHealingDb.Migrations
{
    public partial class WoundAndWoundTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PainLevel",
                columns: table => new
                {
                    PainLevelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PainLevelName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PainLevel_PainLevelId", x => x.PainLevelId);
                });

            migrationBuilder.CreateTable(
                name: "PainType",
                columns: table => new
                {
                    PainTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PainTypeName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PainType_PainTypeId", x => x.PainTypeId);
                });

            migrationBuilder.CreateTable(
                name: "SurroundingSkin",
                columns: table => new
                {
                    SurroundingSkinId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurroundingSkinName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurroundingSkin_SurroundingSkinId", x => x.SurroundingSkinId);
                });

            migrationBuilder.CreateTable(
                name: "WoundBleeding",
                columns: table => new
                {
                    WoundBleedingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WoundBleedingName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WoundBleeding_WoundBleedingId", x => x.WoundBleedingId);
                });

            migrationBuilder.CreateTable(
                name: "WoundColor",
                columns: table => new
                {
                    WoundColorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WoundColorName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WoundColor_WoundColorId", x => x.WoundColorId);
                });

            migrationBuilder.CreateTable(
                name: "WoundExudate",
                columns: table => new
                {
                    WoundExudateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WoundExudateName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WoundExudate_WoundExudateId", x => x.WoundExudateId);
                });

            migrationBuilder.CreateTable(
                name: "WoundLocation",
                columns: table => new
                {
                    WoundLocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WoundLocationName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WoundLocation_WoundLocationId", x => x.WoundLocationId);
                });

            migrationBuilder.CreateTable(
                name: "WoundOdor",
                columns: table => new
                {
                    WoundOdorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WoundOdorName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WoundOdor_WoundOdorId", x => x.WoundOdorId);
                });

            migrationBuilder.CreateTable(
                name: "WoundSize",
                columns: table => new
                {
                    WoundSizeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WoundSizeName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WoundSize_WoundSizeId", x => x.WoundSizeId);
                });

            migrationBuilder.CreateTable(
                name: "WoundType",
                columns: table => new
                {
                    WoundTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WoundTypeName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WoundType_WoundTypeId", x => x.WoundTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Wound",
                columns: table => new
                {
                    WoundId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    WoundTypeId = table.Column<int>(nullable: false),
                    WoundLocationId = table.Column<int>(nullable: false),
                    WoundSizeId = table.Column<int>(nullable: false),
                    WoundColorId = table.Column<int>(nullable: false),
                    WoundOdorId = table.Column<int>(nullable: false),
                    WoundExudateId = table.Column<int>(nullable: false),
                    WoundBleedingId = table.Column<int>(nullable: false),
                    SurroundingSkinId = table.Column<int>(nullable: false),
                    PainTypeId = table.Column<int>(nullable: false),
                    PainLevelId = table.Column<int>(nullable: false),
                    WoundRegisterDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wound_WoundId", x => x.WoundId);
                    table.ForeignKey(
                        name: "FK_Wound_PainLevel_PainLevelId",
                        column: x => x.PainLevelId,
                        principalTable: "PainLevel",
                        principalColumn: "PainLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wound_PainType_PainTypeId",
                        column: x => x.PainTypeId,
                        principalTable: "PainType",
                        principalColumn: "PainTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wound_SurroundingSkin_SurroundingSkinId",
                        column: x => x.SurroundingSkinId,
                        principalTable: "SurroundingSkin",
                        principalColumn: "SurroundingSkinId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wound_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wound_WoundBleeding_WoundBleedingId",
                        column: x => x.WoundBleedingId,
                        principalTable: "WoundBleeding",
                        principalColumn: "WoundBleedingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wound_WoundColor_WoundColorId",
                        column: x => x.WoundColorId,
                        principalTable: "WoundColor",
                        principalColumn: "WoundColorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wound_WoundExudate_WoundExudateId",
                        column: x => x.WoundExudateId,
                        principalTable: "WoundExudate",
                        principalColumn: "WoundExudateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wound_WoundLocation_WoundLocationId",
                        column: x => x.WoundLocationId,
                        principalTable: "WoundLocation",
                        principalColumn: "WoundLocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wound_WoundOdor_WoundOdorId",
                        column: x => x.WoundOdorId,
                        principalTable: "WoundOdor",
                        principalColumn: "WoundOdorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wound_WoundSize_WoundSizeId",
                        column: x => x.WoundSizeId,
                        principalTable: "WoundSize",
                        principalColumn: "WoundSizeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Wound_WoundType_WoundTypeId",
                        column: x => x.WoundTypeId,
                        principalTable: "WoundType",
                        principalColumn: "WoundTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wound_PainLevelId",
                table: "Wound",
                column: "PainLevelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_PainTypeId",
                table: "Wound",
                column: "PainTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_SurroundingSkinId",
                table: "Wound",
                column: "SurroundingSkinId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_UserId",
                table: "Wound",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundBleedingId",
                table: "Wound",
                column: "WoundBleedingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundColorId",
                table: "Wound",
                column: "WoundColorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundExudateId",
                table: "Wound",
                column: "WoundExudateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundLocationId",
                table: "Wound",
                column: "WoundLocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundOdorId",
                table: "Wound",
                column: "WoundOdorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundSizeId",
                table: "Wound",
                column: "WoundSizeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wound_WoundTypeId",
                table: "Wound",
                column: "WoundTypeId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wound");

            migrationBuilder.DropTable(
                name: "PainLevel");

            migrationBuilder.DropTable(
                name: "PainType");

            migrationBuilder.DropTable(
                name: "SurroundingSkin");

            migrationBuilder.DropTable(
                name: "WoundBleeding");

            migrationBuilder.DropTable(
                name: "WoundColor");

            migrationBuilder.DropTable(
                name: "WoundExudate");

            migrationBuilder.DropTable(
                name: "WoundLocation");

            migrationBuilder.DropTable(
                name: "WoundOdor");

            migrationBuilder.DropTable(
                name: "WoundSize");

            migrationBuilder.DropTable(
                name: "WoundType");
        }
    }
}