using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BattleCalculator.Data.Migrations
{
    public partial class UpdateGameChronoLongAndEndedNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndedAt",
                table: "Games",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<long>(
                name: "Chrono",
                table: "Games",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "EndedAt",
                table: "Games",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Chrono",
                table: "Games",
                type: "text",
                nullable: true,
                oldClrType: typeof(long));
        }
    }
}
