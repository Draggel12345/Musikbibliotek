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
                .ForMember(s => s.ArtistInfo, opt => opt.MapFrom(o => $"Id: {o.Id} Band: {o.Name}"))
                .ReverseMap();
            CreateMap<ArtistDTORequest, ArtistEntity>();

            CreateMap<AlbumEntity, AlbumDTOResult>()
                .ForMember(s => s.AlbumInfo, opt => opt.MapFrom(o => $"Id: {o.Id} Album Name: {o.Name}"))
                .ForMember(s => s.ArtistInfo, opt => opt.MapFrom(o => $"Id: {o.ArtistId} Band: {o.Artist.Name}"))
                .ReverseMap();

            CreateMap<AlbumDTORequest, AlbumEntity>();

            CreateMap<SongEntity, SongDTOResult>()
                .ForMember(s => s.SongInfo, opt => opt.MapFrom(o => $"Id: {o.Id} Song Name: {o.Name} Length: {o.SongLength:mm\\:ss}"))
                .ForMember(s => s.AlbumInfo, opt => opt.MapFrom(o => $"Id: {o.AlbumId} Album Name: {o.Album.Name}"))
                .ForMember(s => s.ArtistInfo, opt => opt.MapFrom(o => $"Id: {o.ArtistId} Band: {o.ArtistName}"))
                .ReverseMap();
            CreateMap<SongDTORequest, SongEntity>();
        }
    }
}
