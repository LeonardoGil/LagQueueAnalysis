using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagQueueAnalysisInfra.Migrations
{
    /// <inheritdoc />
    public partial class Add_Start_and_End_Dates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessingEnded",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessingStarted",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessingEnded",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ProcessingStarted",
                table: "Messages");
        }
    }
}
