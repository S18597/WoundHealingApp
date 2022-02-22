using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WoundHealingDb.Migrations
{
    public partial class TreatmentAndAppointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Treatment",
                columns: table => new
                {
                    TreatmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WoundId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    PatientId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatment_TreatmentId", x => x.TreatmentId);
                    table.ForeignKey(
                        name: "FK_Treatment_User_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Treatment_User_PatientId",
                        column: x => x.PatientId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Treatment_Wound_WoundId",
                        column: x => x.WoundId,
                        principalTable: "Wound",
                        principalColumn: "WoundId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TreatmentId = table.Column<int>(nullable: false),
                    AppointmentNotes = table.Column<string>(maxLength: 1000, nullable: true),
                    AppointmentDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment_AppointmentId", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointment_Treatment_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatment",
                        principalColumn: "TreatmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_TreatmentId",
                table: "Appointment",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_DoctorId",
                table: "Treatment",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_PatientId",
                table: "Treatment",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatment_WoundId",
                table: "Treatment",
                column: "WoundId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Treatment");
        }
    }
}