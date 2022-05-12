using System.ComponentModel.DataAnnotations;

namespace Musikbibliotek.Models.Entities
{
    public class ArtistEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public virtual ICollection<AlbumEntity> Albums { get; set; } = null!;
    }
}
