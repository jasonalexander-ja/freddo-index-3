using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FreddoIndex.Migrations
{
    public partial class AddedActiveUntil : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "activeUntil",
                table: "PriceChangePoint",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "activeUntil",
                table: "PriceChangePoint");
        }
    }
}
