namespace Musikbibliotek.Models.DTOs
{
    public class SongDTORequest
    {
        public string Name { get; set; } = null!;
        public int SongLength { get; set; }
        public int AlbumId { get; set; }
    }
}
