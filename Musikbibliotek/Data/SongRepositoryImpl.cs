using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Musikbibliotek.Models.DTOs;
using Musikbibliotek.Models.Entities;

namespace Musikbibliotek.Data
{
    public interface ISongRepository
    {
        public Task<SongEntity> CreateSongAsync(SongDTORequest request);
        public Task<IEnumerable<SongEntity>> GetAllSongsAsync();
        public Task<SongEntity> GetSongByIdAsync(int id);
        public Task<SongEntity> UpdateSongAsync(int id, SongDTORequest request);
        public Task<bool> DeleteSongAsync(int id);
    }

    public class SongRepositoryImpl : ISongRepository
    {
        private readonly MusicDataContext _context;
        private readonly IMapper _mapper;

        public SongRepositoryImpl(MusicDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SongEntity> CreateSongAsync(SongDTORequest request)
        {
            if (!await _context.Songs.AnyAsync(s => s.Name == request.Name))
            {
                SongEntity entity = _mapper.Map<SongEntity>(request);

                entity.SongLength = TimeSpan.FromSeconds(request.SongLength);

                AlbumEntity? album = await _context.Albums.FirstOrDefaultAsync(a => a.Id == entity.AlbumId);
                if (album != null)
                {
                    entity.Album = album;

                    ArtistEntity? artist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == album.ArtistId);
                    if (artist != null)
                    {
                        entity.ArtistId = artist.Id;
                        entity.ArtistName = artist.Name;

                        _context.Songs.Add(entity);
                        await _context.SaveChangesAsync();

                        return entity;
                    }
                }
            }

            return null!;
        }

        public async Task<IEnumerable<SongEntity>> GetAllSongsAsync()
        {
            return await _context.Songs
                .Include(a => a.Album)
                .ToListAsync();
        }

        public async Task<SongEntity> GetSongByIdAsync(int id)
        {
            SongEntity? entity = await _context.Songs
                .Include(a => a.Album)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (entity != null)
            {
                return entity;
            }

            return null!;
        }

        public async Task<SongEntity> UpdateSongAsync(int id, SongDTORequest request)
        {
            ArtistEntity? artist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == id);

            if (artist != null)
            {
                SongEntity? old = await _context.Songs.FirstOrDefaultAsync(s => s.ArtistId == artist.Id);

                if (old != null)
                {
                    if (!string.IsNullOrEmpty(request.Name) && !string.IsNullOrWhiteSpace(request.Name))
                        old.Name = request.Name;

                    if (request.SongLength != 0)
                        old.SongLength = TimeSpan.FromSeconds(request.SongLength);

                    if (request.AlbumId != 0 && !await _context.Albums.AnyAsync(a => a.Id == request.AlbumId))
                        old.AlbumId = request.AlbumId;

                    //if (request.ArtistId != 0 && old.ArtistId != request.ArtistId)
                    //    old.ArtistId = request.ArtistId;

                    //if (!string.IsNullOrEmpty(request.ArtistName) && !string.IsNullOrWhiteSpace(request.ArtistName))
                    //    old.ArtistName = request.Name;

                    _context.Entry(old).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return old;
                }
            }


            return null!;
        }

        public async Task<bool> DeleteSongAsync(int id)
        {
            SongEntity toRemove = await GetSongByIdAsync(id);
            if (toRemove != null)
            {
                _context.Songs.Remove(toRemove);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}
