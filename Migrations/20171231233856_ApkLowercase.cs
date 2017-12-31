using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Fyo.Migrations
{
    public partial class ApkLowercase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoftwareVersions_Software_SoftwareID",
                table: "SoftwareVersions");

            migrationBuilder.RenameColumn(
                name: "URL",
                table: "SoftwareVersions",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "SoftwareID",
                table: "SoftwareVersions",
                newName: "SoftwareId");

            migrationBuilder.RenameColumn(
                name: "APK",
                table: "SoftwareVersions",
                newName: "Apk");

            migrationBuilder.RenameIndex(
                name: "IX_SoftwareVersions_SoftwareID",
                table: "SoftwareVersions",
                newName: "IX_SoftwareVersions_SoftwareId");

            migrationBuilder.AlterColumn<long>(
                name: "SoftwareId",
                table: "SoftwareVersions",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SoftwareVersions_Software_SoftwareId",
                table: "SoftwareVersions",
                column: "SoftwareId",
                principalTable: "Software",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoftwareVersions_Software_SoftwareId",
                table: "SoftwareVersions");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "SoftwareVersions",
                newName: "URL");

            migrationBuilder.RenameColumn(
                name: "SoftwareId",
                table: "SoftwareVersions",
                newName: "SoftwareID");

            migrationBuilder.RenameColumn(
                name: "Apk",
                table: "SoftwareVersions",
                newName: "APK");

            migrationBuilder.RenameIndex(
                name: "IX_SoftwareVersions_SoftwareId",
                table: "SoftwareVersions",
                newName: "IX_SoftwareVersions_SoftwareID");

            migrationBuilder.AlterColumn<long>(
                name: "SoftwareID",
                table: "SoftwareVersions",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_SoftwareVersions_Software_SoftwareID",
                table: "SoftwareVersions",
                column: "SoftwareID",
                principalTable: "Software",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
