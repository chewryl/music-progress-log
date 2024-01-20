using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicProgressLogAPI.Migrations
{
    /// <inheritdoc />
    public partial class OnetomanybetweenPieceandProgressLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProgressLogs_PieceId",
                table: "ProgressLogs");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressLogs_PieceId",
                table: "ProgressLogs",
                column: "PieceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProgressLogs_PieceId",
                table: "ProgressLogs");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressLogs_PieceId",
                table: "ProgressLogs",
                column: "PieceId",
                unique: true);
        }
    }
}
