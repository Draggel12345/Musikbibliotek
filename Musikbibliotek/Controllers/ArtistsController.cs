using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musikbibliotek.Models.DTOs;
using Musikbibliotek.Services;
using System.Web;

namespace Musikbibliotek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _service;
        private readonly LinkGenerator _linkGenerator;

        public ArtistsController(
            IArtistService service,
            LinkGenerator linkGenerator
            )
        {
            _service = service;
            _linkGenerator = linkGenerator;
        }

        [HttpPost]
        public async Task<IActionResult> PostArtist(ArtistDTORequest request)
        {
            ArtistDTOResult result = await _service.CreateAsync(request);
            if (result != null)
            {
                var location = _linkGenerator.GetPathByAction(
                    "PostArtist", "Artists",
                    new { result.ArtistInfo }
                    );
                if (location != null)
                {
                    return Created(location, result);
                }
            }

            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArtists()
        {
            return new OkObjectResult(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistById(int id)
        {
            ArtistDTOResult result = await _service.GetByIdAsync(id);
            if (result != null)
            {
                return new OkObjectResult(result);
            }

            return new NotFoundResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtist(int id, ArtistDTORequest request)
        {
            ArtistDTOResult result = await _service.UpdateAsync(id, request);
            if (result != null)
            {
                return new OkObjectResult(result);
            }

            return new BadRequestResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            if (await _service.DeleteAsync(id))
            {
                return new OkResult();
            }

            return new NotFoundResult();
        }
    }
}
