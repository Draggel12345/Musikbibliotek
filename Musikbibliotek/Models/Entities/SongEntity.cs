using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Musikbibliotek.Models.Entities
{
    public class SongEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public TimeSpan SongLength { get; set; }

        [Required]
        public int AlbumId { get; set; }
        public virtual AlbumEntity Album { get; set; } = null!;

        [Required]
        public int ArtistId { get; set; }
        [Required]
        public string ArtistName { get; set; } = null!;
    }
}