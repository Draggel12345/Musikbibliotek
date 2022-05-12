using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musikbibliotek.Models.DTOs;
using Musikbibliotek.Services;

namespace Musikbibliotek.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongService _service;
        private readonly LinkGenerator _linkGenerator;

        public SongsController(
            ISongService service,
            LinkGenerator linkGenerator
            )
        {
            _service = service;
            _linkGenerator = linkGenerator;
        }

        [HttpPost]
        public async Task<IActionResult> PostSong(SongDTORequest request)
        {
            SongDTOResult result = await _service.CreateAsync(request);
            if (result != null)
            {
                var location = _linkGenerator.GetPathByAction(
                    "PostSong", "Songs",
                    new { result.SongInfo }
                    );
                if (location != null)
                {
                    return Created(location, result);
                }
            }

            return new BadRequestResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSongs()
        {
            return new OkObjectResult(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSongById(int id)
        {
            SongDTOResult result = await _service.GetByIdAsync(id);
            if (result != null)
            {
                return new OkObjectResult(result);
            }

            return new NotFoundResult();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSong(int id, SongDTORequest request)
        {
            SongDTOResult result = await _service.UpdateAsync(id, request);
            if (result != null)
            {
                return new OkObjectResult(result);
            }

            return new BadRequestResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            if (await _service.DeleteAsync(id))
            {
                return new OkResult();
            }

            return new NotFoundResult();
        }
    }
}
