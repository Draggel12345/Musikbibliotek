namespace Musikbibliotek.Models.DTOs
{
    public class ArtistDTOResult
    {
        public string ArtistInfo { get; set; } = null!;
        public virtual ICollection<AlbumDTOResult> Albums { get; set; } = null!;
    }
}
