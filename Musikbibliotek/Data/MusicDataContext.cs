using Microsoft.EntityFrameworkCore;
using Musikbibliotek.Models.DTOs;
using Musikbibliotek.Models.Entities;

namespace Musikbibliotek.Data
{
    public class MusicDataContext : DbContext
    {
        private readonly DbContextOptions? _options;
        private readonly IConfiguration? _config;

        public MusicDataContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _options = options;
            _config = config;
        }

        protected MusicDataContext()
        {
        }

        public virtual DbSet<ArtistEntity> Artists { get; set; } = null!;
        public virtual DbSet<AlbumEntity> Albums { get; set; } = null!;
        public virtual DbSet<SongEntity> Songs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("LocalSql"));
        }

        protected override void OnModelCreating(ModelBuilder bldr)
        {
            //Connection against DB
            base.OnModelCreating(bldr);

            bldr.Entity<ArtistEntity>()
                .HasData(new
                {
                    Id = 1,
                    Name = "AC/DC"
                });
            bldr.Entity<AlbumEntity>()
                .HasData(new
                {
                    Id = 1,
                    Name = "Power Up",
                    ArtistId = 1
                });
            bldr.Entity<SongEntity>()
                .HasData(new
                {
                    Id = 1,
                    Name = "Shot In The Dark",
                    SongLength = TimeSpan.FromSeconds(183),
                    AlbumId = 1,
                    ArtistId = 1,
                    ArtistName = "AC/DC"
                });

            bldr.Entity<ArtistEntity>()
                .HasData(new
                {
                    Id = 2,
                    Name = "KORN"
                });
            bldr.Entity<AlbumEntity>()
                .HasData(new
                {
                    Id = 2,
                    Name = "Requiem",
                    ArtistId = 2
                });
            bldr.Entity<SongEntity>()
                .HasData(new
                {
                    Id = 2,
                    Name = "Forgotten",
                    SongLength = TimeSpan.FromSeconds(190),
                    AlbumId = 2,
                    ArtistId = 2,
                    ArtistName = "KORN"
                });

            bldr.Entity<ArtistEntity>()
                .HasData(new
                {
                    Id = 3,
                    Name = "System Of A Down"
                });
            bldr.Entity<AlbumEntity>()
                .HasData(new
                {
                    Id = 3,
                    Name = "Mezmerize",
                    ArtistId = 3
                });
            bldr.Entity<SongEntity>()
                .HasData(new
                {
                    Id = 3,
                    Name = "B.Y.O.B",
                    SongLength = TimeSpan.FromSeconds(249),
                    AlbumId = 3,
                    ArtistId = 3,
                    ArtistName = "System Of A Down"
                });

            bldr.Entity<ArtistEntity>()
                .HasData(new
                {
                    Id = 4,
                    Name = "Linkin Park"
                });
            bldr.Entity<AlbumEntity>()
                .HasData(new
                {
                    Id = 4,
                    Name = "Meteora",
                    ArtistId = 4
                });
            bldr.Entity<SongEntity>()
                .HasData(new
                {
                    Id = 4,
                    Name = "Faint",
                    SongLength = TimeSpan.FromSeconds(145),
                    AlbumId = 4,
                    ArtistId = 4,
                    ArtistName = "Linkin Park"
                });

            bldr.Entity<ArtistEntity>()
                .HasData(new
                {
                    Id = 5,
                    Name = "Iron Maiden"
                });
            bldr.Entity<AlbumEntity>()
                .HasData(new
                {
                    Id = 5,
                    Name = "Powerslave",
                    ArtistId = 5
                });
            bldr.Entity<SongEntity>()
                .HasData(new
                {
                    Id = 5,
                    Name = "Aces High",
                    SongLength = TimeSpan.FromSeconds(259),
                    AlbumId = 5,
                    ArtistId = 5,
                    ArtistName = "Iron Maiden"
                });
        }
    }
}
