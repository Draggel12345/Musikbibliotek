using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Musikbibliotek.Models.DTOs;
using Musikbibliotek.Models.Entities;

namespace Musikbibliotek.Data
{
    public interface IAlbumRepository
    {
        public Task<AlbumEntity> CreateAlbumAsync(AlbumDTORequest request);
        public Task<IEnumerable<AlbumEntity>> GetAllAlbumsAsync();
        public Task<AlbumEntity> GetAlbumByIdAsync(int id);
        public Task<AlbumEntity> UpdateAlbumAsync(int id, AlbumDTORequest request);
        public Task<bool> DeleteAlbumAsync(int id);
    }

    public class AlbumRepositoryImpl : IAlbumRepository
    {
        private readonly MusicDataContext _context;
        private readonly IMapper _mapper;

        public AlbumRepositoryImpl(MusicDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AlbumEntity> CreateAlbumAsync(AlbumDTORequest request)
        {
            if (!await _context.Albums.AnyAsync(a => a.Name == request.Name))
            {
                AlbumEntity entity = _mapper.Map<AlbumEntity>(request);
                ArtistEntity? artist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == entity.ArtistId);
                if (artist != null)
                {
                    entity.Artist = artist;
                    //SongEntity? song = await _context.Songs.FirstOrDefaultAsync(a => a.ArtistId == artist.Id);

                    //if (song != null)
                    //    entity.Songs.Add(song);

                    /*
                     Fixa så albums sång-lista visar sångerna!
                     */

                    _context.Albums.Add(entity);
                    await _context.SaveChangesAsync();

                    return entity;
                }
            }

            return null!;
        }

        public async Task<IEnumerable<AlbumEntity>> GetAllAlbumsAsync()
        {
            return await _context.Albums.Include(a => a.Songs).ToListAsync();
        }

        public async Task<AlbumEntity> GetAlbumByIdAsync(int id)
        {
            AlbumEntity? entity = await _context.Albums.Include(a => a.Songs).FirstOrDefaultAsync(a => a.Id == id);
            if (entity != null)
            {
                return entity;
            }

            return null!;
        }

        public async Task<AlbumEntity> UpdateAlbumAsync(int id, AlbumDTORequest request)
        {
            AlbumEntity old = await GetAlbumByIdAsync(id);
            if (old != null)
            {
                ArtistEntity? artist = await _context.Artists.FirstOrDefaultAsync(a => a.Id == old.ArtistId);
                if (artist != null)
                {
                    if (!string.IsNullOrEmpty(request.Name) && !string.IsNullOrWhiteSpace(request.Name))
                        old.Name = request.Name;

                    if (old.ArtistId != 0 && old.ArtistId != request.ArtistId)
                        old.ArtistId = request.ArtistId;

                    _context.Entry(old).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    old.Artist = artist;

                    return old;
                }
            }

            return null!;
        }

        public async Task<bool> DeleteAlbumAsync(int id)
        {
            AlbumEntity toRemove = await GetAlbumByIdAsync(id);
            if (toRemove != null)
            {
                _context.Albums.Remove(toRemove);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
