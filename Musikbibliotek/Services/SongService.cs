using AutoMapper;
using Musikbibliotek.Data;
using Musikbibliotek.Models.DTOs;
using Musikbibliotek.Models.Entities;
using System.Web;

namespace Musikbibliotek.Services
{
    public interface ISongService
    {
        public Task<SongDTOResult> CreateAsync(SongDTORequest request);
        public Task<IEnumerable<SongDTOResult>> GetAllAsync();
        public Task<SongDTOResult> GetByIdAsync(int id);
        public Task<SongDTOResult> UpdateAsync(int id, SongDTORequest request);
        public Task<bool> DeleteAsync(int id);
    }
    public class SongService : ISongService
    {
        private readonly ISongRepository _repo;
        private readonly IMapper _mapper;

        public SongService(ISongRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<SongDTOResult> CreateAsync(SongDTORequest request)
        {
            SongEntity entity = await _repo.CreateSongAsync(request);

            if (entity != null)
            {
                return _mapper.Map<SongDTOResult>(entity);
            }

            throw new ArgumentNullException(nameof(request));
        }

        public async Task<IEnumerable<SongDTOResult>> GetAllAsync()
        {
            var result = new List<SongDTOResult>();
            var list = await _repo.GetAllSongsAsync();
            foreach (var item in list)
            {
                result.Add(_mapper.Map<SongDTOResult>(item));
            }

            return result.ToList();
        }

        public async Task<SongDTOResult> GetByIdAsync(int id)
        {
            SongEntity entity = await _repo.GetSongByIdAsync(id);
            if (entity != null)
            {
                return _mapper.Map<SongDTOResult>(entity);
            }

            throw new ArgumentNullException(nameof(id));
        }

        public async Task<SongDTOResult> UpdateAsync(int id, SongDTORequest request)
        {
            SongEntity entity = await _repo.UpdateSongAsync(id, request);
            if (entity != null)
            {
                return _mapper.Map<SongDTOResult>(entity);
            }

            throw new ArgumentNullException(nameof(id), nameof(request));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _repo.DeleteSongAsync(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return true;
        }
    }
}
