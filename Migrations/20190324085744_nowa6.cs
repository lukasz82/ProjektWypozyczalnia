using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Movies.Migrations
{
    public partial class nowa6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RentMovie",
                columns: table => new
                {
                    RentMovieId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayRentPrice = table.Column<float>(nullable: false),
                    IsRent = table.Column<bool>(nullable: false),
                    IsReserved = table.Column<bool>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    RentDate = table.Column<DateTime>(nullable: false),
                    RentDays = table.Column<int>(nullable: false),
                    ReservedDays = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentMovie", x => x.RentMovieId);
                    table.ForeignKey(
                        name: "FK_RentMovie_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    ReservationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsReserved = table.Column<bool>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    ReservedDate = table.Column<DateTime>(nullable: false),
                    ReservedDays = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservation_Movie_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movie",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RentMovie_MovieId",
                table: "RentMovie",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_MovieId",
                table: "Reservation",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentMovie");

            migrationBuilder.DropTable(
                name: "Reservation");
        }
    }
}
