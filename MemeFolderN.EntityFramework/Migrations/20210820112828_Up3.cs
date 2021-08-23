using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeFolderN.EntityFramework.Migrations
{
    public partial class Up3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memes_Folders_ParentFolderId",
                table: "Memes");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentFolderId",
                table: "Memes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentFolderId",
                table: "Folders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Memes_Folders_ParentFolderId",
                table: "Memes",
                column: "ParentFolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memes_Folders_ParentFolderId",
                table: "Memes");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentFolderId",
                table: "Memes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentFolderId",
                table: "Folders",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Memes_Folders_ParentFolderId",
                table: "Memes",
                column: "ParentFolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
