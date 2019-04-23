using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Movies.Migrations
{
    public partial class nowa7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RentMovie_MovieId",
                table: "RentMovie");

            migrationBuilder.CreateIndex(
                name: "IX_RentMovie_MovieId",
                table: "RentMovie",
                column: "MovieId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RentMovie_MovieId",
                table: "RentMovie");

            migrationBuilder.CreateIndex(
                name: "IX_RentMovie_MovieId",
                table: "RentMovie",
                column: "MovieId");
        }
    }
}
