using Microsoft.EntityFrameworkCore;
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
        }
    }
}
