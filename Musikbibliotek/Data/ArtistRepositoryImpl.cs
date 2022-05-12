using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Musikbibliotek.Models.DTOs;
using Musikbibliotek.Models.Entities;

namespace Musikbibliotek.Data
{
    public interface IArtistRepository
    {
        public Task<ArtistEntity> CreateArtistAsync(ArtistDTORequest request);
        public Task<IEnumerable<ArtistEntity>> GetAllArtistsAsync();
        public Task<ArtistEntity> GetArtistByIdAsync(int id);
        public Task<ArtistEntity> UpdateArtistAsync(int id, ArtistDTORequest request);
        public Task<bool> DeleteArtistAsync(int id);
    }

    public class ArtistRepositoryImpl : IArtistRepository
    {
        private readonly MusicDataContext _context;
        private readonly IMapper _mapper;

        public ArtistRepositoryImpl(MusicDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ArtistEntity> CreateArtistAsync(ArtistDTORequest request)
        {
            if (!await _context.Artists.AnyAsync(a => a.Name == request.Name))
            {
                ArtistEntity entity = _mapper.Map<ArtistEntity>(request);

                _context.Artists.Add(entity);
                await _context.SaveChangesAsync();

                return entity;
            }

            return null!;
        }

        public async Task<IEnumerable<ArtistEntity>> GetAllArtistsAsync()
        {
            return await _context.Artists.Include(a => a.Albums).ToListAsync();
        }

        public async Task<ArtistEntity> GetArtistByIdAsync(int id)
        {
            ArtistEntity? entity = await _context.Artists.Include(a => a.Albums).FirstOrDefaultAsync(a => a.Id == id);
            if (entity != null)
            {
                return entity;
            }

            return null!;
        }

        public async Task<ArtistEntity> UpdateArtistAsync(int id, ArtistDTORequest request)
        {
            ArtistEntity old = await GetArtistByIdAsync(id);
            if (old != null)
            {
                AlbumEntity? album = await _context.Albums.FirstOrDefaultAsync(a => a.Id == old.Id);

                if (album != null)
                {
                    SongEntity? song = await _context.Songs.FirstOrDefaultAsync(s => s.ArtistId == album.Id);
                    if (song != null)
                    {
                        if (!string.IsNullOrEmpty(request.Name) && !string.IsNullOrWhiteSpace(request.Name))
                            old.Name = request.Name;

                        song.ArtistId = old.Id;
                        song.ArtistName = old.Name;

                        _context.Entry(old).State = EntityState.Modified;
                        await _context.SaveChangesAsync();

                        return old;
                    }
                }
            }

            return null!;
        }

        public async Task<bool> DeleteArtistAsync(int id)
        {
            ArtistEntity toRemove = await GetArtistByIdAsync(id);
            if (toRemove != null)
            {
                _context.Artists.Remove(toRemove);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
