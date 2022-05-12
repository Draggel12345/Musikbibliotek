using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musikbibliotek.Models.DTOs;
using Musikbibliotek.Services;

namespace Musikbibliotek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _service;
        private readonly LinkGenerator _linkGenerator;

        public AlbumsController(
            IAlbumService service,
            LinkGenerator linkGenerator
            )
        {
            _service = service;
            _linkGenerator = linkGenerator;
        }

        [HttpPost]
        public async Task<IActionResult> PostAlbum(AlbumDTORequest request)
        {
            AlbumDTOResult result = await _service.CreateAsync(request);
            if (result != null)
            {
                var location = _linkGenerator.GetPathByAction(
                    "PostAlbum", "Albums",
                    new { result.AlbumInfo }
                    );
                if (location != null)
                {
                    return Created(location, result);
                }
            }

            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAlbums()
        {
            return new OkObjectResult(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbumById(int id)
        {
            AlbumDTOResult result = await _service.GetByIdAsync(id);
            if (result != null)
            {
                return new OkObjectResult(result);
            }

            return new NotFoundResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlbum(int id, AlbumDTORequest request)
        {
            AlbumDTOResult result = await _service.UpdateAsync(id, request);
            if (result != null)
            {
                return new OkObjectResult(result);
            }

            return new BadRequestResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            if (await _service.DeleteAsync(id))
            {
                return new OkResult();
            }

            return new NotFoundResult();
        }
    }
}
