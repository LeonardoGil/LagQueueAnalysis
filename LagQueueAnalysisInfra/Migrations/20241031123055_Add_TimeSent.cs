using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagQueueAnalysisInfra.Migrations
{
    /// <inheritdoc />
    public partial class Add_TimeSent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeSent",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSent",
                table: "Messages");
        }
    }
}
