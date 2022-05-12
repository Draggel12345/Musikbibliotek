using AutoMapper;
using Musikbibliotek.Data;
using Musikbibliotek.Models.DTOs;
using Musikbibliotek.Models.Entities;

namespace Musikbibliotek.Services
{
    public interface IAlbumService
    {
        public Task<AlbumDTOResult> CreateAsync(AlbumDTORequest request);
        public Task<IEnumerable<AlbumDTOResult>> GetAllAsync();
        public Task<AlbumDTOResult> GetByIdAsync(int id);
        public Task<AlbumDTOResult> UpdateAsync(int id, AlbumDTORequest request);
        public Task<bool> DeleteAsync(int id);
    }
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _repo;
        private readonly IMapper _mapper;

        public AlbumService(IAlbumRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AlbumDTOResult> CreateAsync(AlbumDTORequest request)
        {
            AlbumEntity result = await _repo.CreateAlbumAsync(request);
            if (result != null)
            {
                return _mapper.Map<AlbumDTOResult>(result);
            }

            throw new ArgumentNullException(nameof(request));
        }

        public async Task<IEnumerable<AlbumDTOResult>> GetAllAsync()
        {
            var result = new List<AlbumDTOResult>();
            var list = await _repo.GetAllAlbumsAsync();
            foreach (var item in list)
            {
                result.Add(_mapper.Map<AlbumDTOResult>(item));
            }

            return result.ToList();
        }

        public async Task<AlbumDTOResult> GetByIdAsync(int id)
        {
            AlbumEntity result = await _repo.GetAlbumByIdAsync(id);
            if (result != null)
            {
                return _mapper.Map<AlbumDTOResult>(result);
            }

            throw new ArgumentNullException(nameof(id));
        }

        public async Task<AlbumDTOResult> UpdateAsync(int id, AlbumDTORequest request)
        {
            AlbumEntity result = await _repo.UpdateAlbumAsync(id, request);
            if (result != null)
            {
                return _mapper.Map<AlbumDTOResult>(result);
            }

            throw new ArgumentNullException(nameof(id), nameof(request));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _repo.DeleteAlbumAsync(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return true;
        }
    }
}
