using AutoMapper;
using Musikbibliotek.Models.DTOs;
using Musikbibliotek.Models.Entities;

namespace Musikbibliotek.AutoMapper
{
    public class MusicMapper : Profile
    {
        public MusicMapper()
        {
            CreateMap<ArtistEntity, ArtistDTOResult>()
                .ForMember(s => s.ArtistInfo, opt => opt.MapFrom(o => $"{o.Id} {o.Name}"))
                .ReverseMap();
            CreateMap<ArtistDTORequest, ArtistEntity>();

            CreateMap<AlbumEntity, AlbumDTOResult>()
                .ForMember(s => s.AlbumInfo, opt => opt.MapFrom(o => $"{o.Id} {o.Name}"))
                .ForMember(s => s.ArtistInfo, opt => opt.MapFrom(o => $"{o.ArtistId} {o.Artist.Name}"))
                .ReverseMap();

            CreateMap<AlbumDTORequest, AlbumEntity>();

            CreateMap<SongDTORequest, SongEntity>();
        }
    }
}
