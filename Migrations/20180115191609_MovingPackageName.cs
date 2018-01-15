using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Fyo.Migrations
{
    public partial class MovingPackageName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "SoftwareVersions");

            migrationBuilder.AddColumn<string>(
                name: "Package",
                table: "Software",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Package",
                table: "Software");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "SoftwareVersions",
                nullable: true);
        }
    }
}
