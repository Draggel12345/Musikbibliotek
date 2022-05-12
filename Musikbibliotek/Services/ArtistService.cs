using AutoMapper;
using Musikbibliotek.Data;
using Musikbibliotek.Models.DTOs;
using Musikbibliotek.Models.Entities;

namespace Musikbibliotek.Services
{
    public interface IArtistService
    {
        public Task<ArtistDTOResult> CreateAsync(ArtistDTORequest request);
        public Task<IEnumerable<ArtistDTOResult>> GetAllAsync();
        public Task<ArtistDTOResult> GetByIdAsync(int id);
        public Task<ArtistDTOResult> UpdateAsync(int id, ArtistDTORequest request);
        public Task<bool> DeleteAsync(int id);
    }
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _repo;
        private readonly IMapper _mapper;

        public ArtistService(IArtistRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ArtistDTOResult> CreateAsync(ArtistDTORequest request)
        {
            ArtistEntity result = await _repo.CreateArtistAsync(request);
            if (result != null)
            {
                return _mapper.Map<ArtistDTOResult>(result);
            }

            throw new ArgumentNullException(nameof(request));
        }

        public async Task<IEnumerable<ArtistDTOResult>> GetAllAsync()
        {
            var result = new List<ArtistDTOResult>();
            var list = await _repo.GetAllArtistsAsync();
            foreach (var item in list)
            {
                result.Add(_mapper.Map<ArtistDTOResult>(item));
            }

            return result.ToList();
        }

        public async Task<ArtistDTOResult> GetByIdAsync(int id)
        {
            ArtistEntity result = await _repo.GetArtistByIdAsync(id);
            if (result != null)
            {
                return _mapper.Map<ArtistDTOResult>(result);
            }

            throw new ArgumentNullException(nameof(id));
        }

        public async Task<ArtistDTOResult> UpdateAsync(int id, ArtistDTORequest request)
        {
            ArtistEntity result = await _repo.UpdateArtistAsync(id, request);
            if (result != null)
            {
                return _mapper.Map<ArtistDTOResult>(result);
            }

            throw new ArgumentNullException(nameof(id), nameof(request));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await _repo.DeleteArtistAsync(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return true;
        }
    }
}
