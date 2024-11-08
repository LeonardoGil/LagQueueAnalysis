using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagQueueAnalysisInfra.Migrations
{
    /// <inheritdoc />
    public partial class ExceptionMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExceptionId",
                table: "Messages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Exceptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeOfFailure = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exceptions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ExceptionId",
                table: "Messages",
                column: "ExceptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Exceptions_ExceptionId",
                table: "Messages",
                column: "ExceptionId",
                principalTable: "Exceptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Exceptions_ExceptionId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "Exceptions");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ExceptionId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "ExceptionId",
                table: "Messages");
        }
    }
}
