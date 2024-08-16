using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagQueueApplication.Migrations
{
    /// <inheritdoc />
    public partial class Added_Host_Field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Host",
                table: "Queues",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Host",
                table: "Queues");
        }
    }
}
