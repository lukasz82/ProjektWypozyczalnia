using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Movies.Migrations
{
    public partial class nowa8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReserved",
                table: "RentMovie");

            migrationBuilder.DropColumn(
                name: "ReservedDays",
                table: "RentMovie");

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "Reviews",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Reviews");

            migrationBuilder.AddColumn<bool>(
                name: "IsReserved",
                table: "RentMovie",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReservedDays",
                table: "RentMovie",
                nullable: false,
                defaultValue: 0);
        }
    }
}
