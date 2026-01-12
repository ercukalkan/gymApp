using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymApp.GymTrackingService.Data.Migrations
{
    /// <inheritdoc />
    public partial class StudentTrainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TrainerUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Students_Trainers_TrainerUserId",
                        column: x => x.TrainerUserId,
                        principalTable: "Trainers",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_TrainerUserId",
                table: "Students",
                column: "TrainerUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Trainers");
        }
    }
}
