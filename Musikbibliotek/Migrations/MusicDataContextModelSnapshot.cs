// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Musikbibliotek.Data;

#nullable disable

namespace Musikbibliotek.Migrations
{
    [DbContext(typeof(MusicDataContext))]
    partial class MusicDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Musikbibliotek.Models.Entities.AlbumEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Albums");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ArtistId = 1,
                            Name = "Power Up"
                        },
                        new
                        {
                            Id = 2,
                            ArtistId = 2,
                            Name = "Requiem"
                        },
                        new
                        {
                            Id = 3,
                            ArtistId = 3,
                            Name = "Mezmerize"
                        },
                        new
                        {
                            Id = 4,
                            ArtistId = 4,
                            Name = "Meteora"
                        },
                        new
                        {
                            Id = 5,
                            ArtistId = 5,
                            Name = "Powerslave"
                        });
                });

            modelBuilder.Entity("Musikbibliotek.Models.Entities.ArtistEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Artists");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "AC/DC"
                        },
                        new
                        {
                            Id = 2,
                            Name = "KORN"
                        },
                        new
                        {
                            Id = 3,
                            Name = "System Of A Down"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Linkin Park"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Iron Maiden"
                        });
                });

            modelBuilder.Entity("Musikbibliotek.Models.Entities.SongEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<string>("ArtistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("SongLength")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Songs");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AlbumId = 1,
                            ArtistId = 1,
                            ArtistName = "AC/DC",
                            Name = "Shot In The Dark",
                            SongLength = new TimeSpan(0, 0, 3, 3, 0)
                        },
                        new
                        {
                            Id = 2,
                            AlbumId = 2,
                            ArtistId = 2,
                            ArtistName = "KORN",
                            Name = "Forgotten",
                            SongLength = new TimeSpan(0, 0, 3, 10, 0)
                        },
                        new
                        {
                            Id = 3,
                            AlbumId = 3,
                            ArtistId = 3,
                            ArtistName = "System Of A Down",
                            Name = "B.Y.O.B",
                            SongLength = new TimeSpan(0, 0, 4, 9, 0)
                        },
                        new
                        {
                            Id = 4,
                            AlbumId = 4,
                            ArtistId = 4,
                            ArtistName = "Linkin Park",
                            Name = "Faint",
                            SongLength = new TimeSpan(0, 0, 2, 25, 0)
                        },
                        new
                        {
                            Id = 5,
                            AlbumId = 5,
                            ArtistId = 5,
                            ArtistName = "Iron Maiden",
                            Name = "Aces High",
                            SongLength = new TimeSpan(0, 0, 4, 19, 0)
                        });
                });

            modelBuilder.Entity("Musikbibliotek.Models.Entities.AlbumEntity", b =>
                {
                    b.HasOne("Musikbibliotek.Models.Entities.ArtistEntity", "Artist")
                        .WithMany("Albums")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("Musikbibliotek.Models.Entities.SongEntity", b =>
                {
                    b.HasOne("Musikbibliotek.Models.Entities.AlbumEntity", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");
                });

            modelBuilder.Entity("Musikbibliotek.Models.Entities.AlbumEntity", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("Musikbibliotek.Models.Entities.ArtistEntity", b =>
                {
                    b.Navigation("Albums");
                });
#pragma warning restore 612, 618
        }
    }
}
