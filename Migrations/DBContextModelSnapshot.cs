﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Movies.Data;
using System;

namespace Movies.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Movies.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.HasKey("AuthorId");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("Movies.Models.Genres", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Movies.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AuthorId");

                    b.Property<int>("GenreId");

                    b.Property<string>("MovieDescription");

                    b.Property<DateTime>("ProductionDate");

                    b.Property<string>("Title");

                    b.HasKey("MovieId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("GenreId");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("Movies.Models.RentMovie", b =>
                {
                    b.Property<int>("RentMovieId")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("DayRentPrice");

                    b.Property<bool>("IsRent");

                    b.Property<int>("MovieId");

                    b.Property<DateTime>("RentDate");

                    b.Property<int>("RentDays");

                    b.HasKey("RentMovieId");

                    b.HasIndex("MovieId")
                        .IsUnique();

                    b.ToTable("RentMovie");
                });

            modelBuilder.Entity("Movies.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsReserved");

                    b.Property<int>("MovieId");

                    b.Property<DateTime>("ReservedDate");

                    b.Property<int>("ReservedDays");

                    b.HasKey("ReservationId");

                    b.HasIndex("MovieId");

                    b.ToTable("Reservation");
                });

            modelBuilder.Entity("Movies.Models.Reviews", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MovieId");

                    b.Property<int>("Rate");

                    b.Property<string>("Review");

                    b.HasKey("ReviewId");

                    b.HasIndex("MovieId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Movies.Models.Movie", b =>
                {
                    b.HasOne("Movies.Models.Author", "Author")
                        .WithMany("Movies")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Movies.Models.Genres", "Genres")
                        .WithMany("Movies")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Movies.Models.RentMovie", b =>
                {
                    b.HasOne("Movies.Models.Movie", "Movie")
                        .WithOne("RentMovie")
                        .HasForeignKey("Movies.Models.RentMovie", "MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Movies.Models.Reservation", b =>
                {
                    b.HasOne("Movies.Models.Movie", "Movie")
                        .WithMany("Reservation")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Movies.Models.Reviews", b =>
                {
                    b.HasOne("Movies.Models.Movie", "Movie")
                        .WithMany("Reviews")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
