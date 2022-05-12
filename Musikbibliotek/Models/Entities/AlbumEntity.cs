using Musikbibliotek.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Musikbibliotek.Models.Entities
{
    public class AlbumEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int ArtistId { get; set; }
        public virtual ArtistEntity Artist { get; set; } = null!;
        public virtual ICollection<SongEntity> Songs { get; set; } = null!;
    }
}