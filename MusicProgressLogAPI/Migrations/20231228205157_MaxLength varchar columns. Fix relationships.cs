using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicProgressLogAPI.Migrations
{
    /// <inheritdoc />
    public partial class MaxLengthvarcharcolumnsFixrelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgressLogs_AudioFiles_AudioFileId",
                table: "ProgressLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressLogs_UserRelationships_UserRelationshipId",
                table: "ProgressLogs");

            migrationBuilder.DropIndex(
                name: "IX_ProgressLogs_AudioFileId",
                table: "ProgressLogs");

            migrationBuilder.DropColumn(
                name: "AudioFileId",
                table: "ProgressLogs");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ProgressLogs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProgressLogs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pieces",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Instrument",
                table: "Pieces",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Composer",
                table: "Pieces",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MIMEType",
                table: "AudioFiles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "AudioFiles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FileLocation",
                table: "AudioFiles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProgressLogId",
                table: "AudioFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AudioFiles_ProgressLogId",
                table: "AudioFiles",
                column: "ProgressLogId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AudioFiles_ProgressLogs_ProgressLogId",
                table: "AudioFiles",
                column: "ProgressLogId",
                principalTable: "ProgressLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressLogs_UserRelationships_UserRelationshipId",
                table: "ProgressLogs",
                column: "UserRelationshipId",
                principalTable: "UserRelationships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AudioFiles_ProgressLogs_ProgressLogId",
                table: "AudioFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgressLogs_UserRelationships_UserRelationshipId",
                table: "ProgressLogs");

            migrationBuilder.DropIndex(
                name: "IX_AudioFiles_ProgressLogId",
                table: "AudioFiles");

            migrationBuilder.DropColumn(
                name: "ProgressLogId",
                table: "AudioFiles");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "ProgressLogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProgressLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AudioFileId",
                table: "ProgressLogs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pieces",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Instrument",
                table: "Pieces",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Composer",
                table: "Pieces",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "MIMEType",
                table: "AudioFiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                table: "AudioFiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "FileLocation",
                table: "AudioFiles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgressLogs_AudioFileId",
                table: "ProgressLogs",
                column: "AudioFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressLogs_AudioFiles_AudioFileId",
                table: "ProgressLogs",
                column: "AudioFileId",
                principalTable: "AudioFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgressLogs_UserRelationships_UserRelationshipId",
                table: "ProgressLogs",
                column: "UserRelationshipId",
                principalTable: "UserRelationships",
                principalColumn: "Id");
        }
    }
}
