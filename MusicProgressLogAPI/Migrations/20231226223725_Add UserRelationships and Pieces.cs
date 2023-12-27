using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicProgressLogAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRelationshipsandPieces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgressLogs_AudioFiles_AudioFileId",
                table: "ProgressLogs");

            migrationBuilder.AlterColumn<Guid>(
                name: "AudioFileId",
                table: "ProgressLogs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "PieceId",
                table: "ProgressLogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserRelationshipId",
                table: "ProgressLogs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserRelationships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRelationships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pieces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Composer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instrument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRelationshipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pieces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pieces_UserRelationships_UserRelationshipId",
                        column: x => x.UserRelationshipId,
                        principalTable: "UserRelationships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgressLogs_PieceId",
                table: "ProgressLogs",
                column: "PieceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgressLogs_UserRelationshipId",
                table: "ProgressLogs",
                column: "UserRelationshipId");

            migrationBuilder.CreateIndex(
                name: "IX_Pieces_UserRelationshipId",
                table: "Pieces",
                column: "UserRelationshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressLogs_AudioFiles_AudioFileId",
                table: "ProgressLogs",
                column: "AudioFileId",
                principalTable: "AudioFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressLogs_Pieces_PieceId",
                table: "ProgressLogs",
                column: "PieceId",
                principalTable: "Pieces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressLogs_UserRelationships_UserRelationshipId",
                table: "ProgressLogs",
                column: "UserRelationshipId",
                principalTable: "UserRelationships",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgressLogs_AudioFiles_AudioFileId",
                table: "ProgressLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressLogs_Pieces_PieceId",
                table: "ProgressLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressLogs_UserRelationships_UserRelationshipId",
                table: "ProgressLogs");

            migrationBuilder.DropTable(
                name: "Pieces");

            migrationBuilder.DropTable(
                name: "UserRelationships");

            migrationBuilder.DropIndex(
                name: "IX_ProgressLogs_PieceId",
                table: "ProgressLogs");

            migrationBuilder.DropIndex(
                name: "IX_ProgressLogs_UserRelationshipId",
                table: "ProgressLogs");

            migrationBuilder.DropColumn(
                name: "PieceId",
                table: "ProgressLogs");

            migrationBuilder.DropColumn(
                name: "UserRelationshipId",
                table: "ProgressLogs");

            migrationBuilder.AlterColumn<Guid>(
                name: "AudioFileId",
                table: "ProgressLogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressLogs_AudioFiles_AudioFileId",
                table: "ProgressLogs",
                column: "AudioFileId",
                principalTable: "AudioFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
