using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMovieOnline.Models;
using WebMovieOnline.ModelViews;
using WebMovieOnline.Services.IMovie;


namespace WebMovieOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly DbwebsiteMovieOnlineContext _db;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly IMovies _moviesServices;
        public MoviesController(DbwebsiteMovieOnlineContext db, IMapper mapper, IWebHostEnvironment webHostEnvironment, IMovies moviesServices)
        {
            _db = db;
            _mapper = mapper;
            _webHostEnviroment = webHostEnvironment;
            _moviesServices = moviesServices;
        }

        [HttpGet("getAllMovies")]
        public async Task<IActionResult> GetAllMovie()
        {
            return Ok(await _moviesServices.GetMoviesAsync());
        }

        [HttpGet("seachPaging")]
        public async Task<IActionResult> SearchPaging([FromQuery] SearchMovie model)
        {
            try
            {
                model.page = model.page.HasValue ? model.page.Value > 0 ? model.page.Value : 1 : 1;
                model.pageSize = model.pageSize == 0 ? 10 : model.pageSize;
                int totalItems = await _moviesServices.CountMovieSearchAsync(model);
                var listSearchMoviePaging = await _moviesServices.SearchMoviesPagingAsync(model);
                return Ok(new MoviesReponse
                {
                    ListMovie = listSearchMoviePaging,
                    Pagings = new Paging(totalItems, model.page, model.pageSize)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("MoviePaging")]
        public async Task<IActionResult> MoviePaging(int? page, int pageSize)
        {
            try
            {
                page = page.HasValue ? page.Value > 0 ? page.Value : 1 : 1;
                pageSize = pageSize == 0 ? 10 : pageSize;
                int totalItems = await _moviesServices.CountMovieAsync();
                var listMoviePaging = await _moviesServices.GetAllMoviesPagingAsync(page, pageSize);
                return Ok(new MoviesReponse
                {
                    ListMovie = listMoviePaging,
                    Pagings = new Paging(totalItems, page, pageSize)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("getIdMovie")]
        public async Task<IActionResult> GetIdMovie(int idMovie)
        {
            try
            {
                return Ok(await _moviesServices.GetMovieByIDAsync(idMovie));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
        [HttpGet("getNameMovie")]
        public async Task<IActionResult> GetNameMovie(string nameMovie)
        {
            try
            {
                return Ok(await _moviesServices.GetMovieByName(nameMovie));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("addMovies")]
        public async Task<IActionResult> AddMovie([FromForm] MovieModelViews model)
        {
            try
            {
                return Ok(await _moviesServices.AddMovieAsync(model));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

        }

        [HttpPut("updateMovies")]
        public async Task<IActionResult> UpdateMovie([FromForm] MovieModelViews model, int? idMovie)
        {
            try
            {
                return Ok(await _moviesServices.UpdateMovieAsync(model, idMovie));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete("deleteMovie")]
        public async Task<IActionResult> DeleteMovie(int idMovie)
        {
            try
            {
                await _moviesServices.DeleteMovieAsync(idMovie);
                return Ok("success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("getMovieGenres")]
        public async Task<IActionResult> SearMovieWithGenres(int idGenres)
        {
            try
            {
                return Ok(await _moviesServices.SearchMovieWithGenresAsync(idGenres));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
        [HttpGet("getMovieCategories")]
        public async Task<IActionResult> SearchMovieWithCategories(int idCategories)
        {
            try
            {
                return Ok(await _moviesServices.SearchMovieWithCategories(idCategories));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("getMovieCountries")]
        public async Task<IActionResult> SearchMovieWithCountries(int idCountries)
        {
            try
            {
                return Ok(await _moviesServices.SearchMovieWithCountries(idCountries));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("filteringMovie")]
        public async Task<IActionResult> FilteringMovie([FromQuery] FilteringMovie request)
        {
            try
            {
                return Ok(await _moviesServices.FilteringMovie(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("getTrenMovie")]
        public async Task<IActionResult> GetTrenMovie()
        {
            try
            {
                return Ok(await _moviesServices.FindTrenMovie());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

    }
}
