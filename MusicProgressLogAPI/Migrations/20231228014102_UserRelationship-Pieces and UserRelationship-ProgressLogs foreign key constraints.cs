﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicProgressLogAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserRelationshipPiecesandUserRelationshipProgressLogsforeignkeyconstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pieces_UserRelationships_UserRelationshipId",
                table: "Pieces");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserRelationshipId",
                table: "ProgressLogs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pieces_UserRelationships_UserRelationshipId",
                table: "Pieces",
                column: "UserRelationshipId",
                principalTable: "UserRelationships",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pieces_UserRelationships_UserRelationshipId",
                table: "Pieces");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserRelationshipId",
                table: "ProgressLogs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Pieces_UserRelationships_UserRelationshipId",
                table: "Pieces",
                column: "UserRelationshipId",
                principalTable: "UserRelationships",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
