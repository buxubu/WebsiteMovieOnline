using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMovieOnline.ModelViews;
using WebMovieOnline.Services.IVideo;

namespace WebMovieOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly DbwebsiteMovieOnlineContext _db;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IVideos _videoServices;
        public VideosController(DbwebsiteMovieOnlineContext db, IMapper mapper, IWebHostEnvironment webHostEnvironment, IVideos videos)
        {
            _db = db;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _videoServices = videos;
        }

        [HttpGet("GetAllVideos")]
        public async Task<IActionResult> GetAllVideos()
        {
            try
            {
                return Ok(await _videoServices.GetAllVideosAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

        }

        [HttpPost("addVideo")]
        public async Task<IActionResult> AddVideo([FromForm] VideoModelViews model)
        {
            try
            {
                if (model.UploadVideo != null)
                {
                    return Ok(await _videoServices.AddVideosAsync(model));
                }
                return Ok("Required UploadVideo or UploadTrailerVideo");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut("editVideo")]
        public async Task<IActionResult> EditVideo([FromForm] VideoModelViews model, int idVideo)
        {
            try
            {
                return Ok(await _videoServices.EditVideosAsync(model, idVideo));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete("deleteVideo")]
        public async Task<IActionResult> DeleteVideo(int idVideo)
        {
            try
            {
                await _videoServices.DeleteVideosAsync(idVideo);
                return Ok("Del Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

    }
}
