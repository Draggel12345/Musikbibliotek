namespace Musikbibliotek.Models.DTOs
{
    public class AlbumDTOResult
    {
        public string AlbumInfo { get; set; } = null!;
        public string ArtistInfo { get; set; } = null!;
        public virtual ICollection<SongDTOResult> Songs { get; set; } = null!;
    }
}