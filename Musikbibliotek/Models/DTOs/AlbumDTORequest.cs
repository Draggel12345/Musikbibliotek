namespace Musikbibliotek.Models.DTOs
{
    public class AlbumDTORequest
    {
        public string Name { get; set; } = null!;
        public int ArtistId { get; set; }
    }
}
