using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebMovieOnline.ModelViews;

namespace WebMovieOnline.Services.IMovie
{
    public interface IMovies
    {
        Task<IEnumerable<MapMovie>> GetMoviesAsync();
        Task<IEnumerable<Movie>> GetMovieByName(string name);
        Task<IEnumerable<MapMovie>> GetAllMoviesPagingAsync(int? page, int pageSize);
        Task<IEnumerable<MapMovie>> SearchMoviesPagingAsync(SearchMovie model);
        Task<IEnumerable<MapMovie>> SearchMovieWithGenresAsync(int idGenres);
        Task<IEnumerable<Movie>> SearchMovieWithCategories(int idCategories);
        Task<IEnumerable<Movie>> SearchMovieWithCountries(int idCountries);
        Task<IEnumerable<Movie>> FilteringMovie(FilteringMovie request);
        Task<MapMovie> GetMovieByIDAsync(int idMovie);
        Task<int> CountMovieAsync();
        Task<int> CountMovieSearchAsync(SearchMovie model);
        Task<Movie> AddMovieAsync([FromForm] MovieModelViews model);
        Task<Movie> UpdateMovieAsync([FromForm] MovieModelViews model, int? idMovie);
        Task DeleteMovieAsync(int idMovie);
        Task<IEnumerable<MapMovie>> FindTrenMovie();
        Task<IEnumerable<MapMovie>> FindSeriesMovie();
        Task<IEnumerable<MapMovie>> GetMoviesCategoryAsync(int idCategories);
        Task<Category> GetNameCate(int idCate);
        Task<IEnumerable<MapMovie>> GetMovieCountryAsync(int idCount);
        Task<IEnumerable<Video>> GetMovieVideoAsync(int idMovie);

    }
    public class RepoMovies : IMovies
    {
        private readonly DbwebsiteMovieOnlineContext _db;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public RepoMovies(DbwebsiteMovieOnlineContext db, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _mapper = mapper;
            _webHostEnviroment = webHostEnvironment;
        }

        public async Task<IEnumerable<MapMovie>> GetMoviesAsync()
        {
            var getMovies = await _db.Movies.Take(5).ToListAsync();
            var mapMovies =_mapper.Map<IEnumerable<MapMovie>>(getMovies);
            return mapMovies;
        }


        public async Task<Movie> AddMovieAsync([FromForm] MovieModelViews model)
        {

            string directoryPathImages = Path.Combine(_webHostEnviroment.ContentRootPath, "UploadImages");
            string directoryPathVideoTrailer = Path.Combine(_webHostEnviroment.ContentRootPath, "UploadVideoTrailer");

            string filePathVT = Path.Combine(directoryPathVideoTrailer, model.UploadVideoTrailer.FileName);
            string filePathI = Path.Combine(directoryPathImages, model.UploadImages.FileName);

            await model.UploadVideoTrailer.CopyToAsync(new FileStream(filePathVT, FileMode.Create));
            await model.UploadImages.CopyToAsync(new FileStream(filePathI, FileMode.Create));
            string hostUrl = "https://localhost:7012/";
            string imagesUrl = hostUrl + "UploadImages/" + model.UploadImages.FileName;
            string videoTrailerUrl = hostUrl + "UploadVideoTrailer/" + model.UploadVideoTrailer.FileName;
           

            //var mapMovie = _mapper.Map<Movie>(model);
            // thay mapping
            Movie addMovies = new Movie()
            {
                IdMovie = 0,
                NameMovie = model.NameMovie,
                Showtimes = model.Showtimes,
                Description = model.Description,
                Trending = model.Trending,
                ReleaseDate = DateTime.Now,
                MovieStatus = model.MovieStatus,
                Images = imagesUrl,
                TrailerMovie = videoTrailerUrl,
                IdCategory = model.IdCategory,
                Tagline = model.Tagline

            };
            await _db.Movies.AddAsync(addMovies);
            await _db.SaveChangesAsync();


            int getIdMovies = addMovies.IdMovie;

            var movie = await _db.Movies.Where(x => x.IdMovie == getIdMovies)
                                        /*.Include(x => x.IdLanguages)
                                        .Include(c => c.IdGenres)
                                        .Include(g => g.IdCountries)
                                        .Include(d => d.IdDirectors)
                                        .Include(p => p.IdCompanies)*/
                                        .FirstOrDefaultAsync();
            if (movie == null) return null;
            //add relationship Movie Language
            foreach (int item in model.idLanguages)
            {
                var language = await _db.Languages.FindAsync(item);
                if (language == null) return null;
                movie.IdLanguages.Add(language);
            }

            //add realationship Movie Genres
            foreach (int item in model.idGenres)
            {
                var genres = await _db.Genres.FindAsync(item);
                if (genres == null) return null;
                movie.IdGenres.Add(genres);
            }

            //add realationship Movie Countries
            foreach (int item in model.idCountries)
            {
                var countries = await _db.Countries.FindAsync(item);
                if (countries == null) return null;
                movie.IdCountries.Add(countries);
            }

            //add realtionship Movie Directors
            foreach (int item in model.idDirectors)
            {
                var directors = await _db.Directors.FindAsync(item);
                if (directors == null) return null;
                movie.IdDirectors.Add(directors);
            }

            //add realationship Movie PC
            foreach (int item in model.idProductionCompanies)
            {
                var PC = await _db.ProductionCompanies.FindAsync(item);
                if (PC == null) return null;
                movie.IdCompanies.Add(PC);
            }

            await _db.SaveChangesAsync();
            return addMovies;
        }



        public async Task<Movie> UpdateMovieAsync([FromForm] MovieModelViews model, int? idMovie)
        {

            var findMovieToUpdate = await _db.Movies/*.Include(x => x.IdLanguages)
                                                    .Include(c => c.IdGenres)
                                                    .Include(z => z.IdCountries)
                                                    .Include(p => p.IdCompanies)
                                                    .Include(d => d.IdDirectors)*/
                                                    .Where(x => x.IdMovie == idMovie)
                                                    .FirstOrDefaultAsync();
            if (findMovieToUpdate == null) return null;

            string directoryPath = Path.Combine(_webHostEnviroment.ContentRootPath, "UploadImages");
            string directoryPathVideoTraielr = Path.Combine(_webHostEnviroment.ContentRootPath, "UploadVideoTrailer");

            var cutFileNameImages = findMovieToUpdate.Images.Split("/").Last();
            string cutTrailerNameVideo = findMovieToUpdate.TrailerMovie.Split("/").Last();

            if (model.UploadImages == null && model.UploadVideoTrailer == null) 
            { 
                findMovieToUpdate.IdMovie = idMovie.Value;
                findMovieToUpdate.NameMovie = model.NameMovie;
                findMovieToUpdate.Showtimes = model.Showtimes;
                findMovieToUpdate.Description = model.Description;
                findMovieToUpdate.Trending = model.Trending;
                findMovieToUpdate.ReleaseDate = model.ReleaseDate;
                findMovieToUpdate.Tagline = model.Tagline;
                findMovieToUpdate.MovieStatus = model.MovieStatus;
                findMovieToUpdate.Images = findMovieToUpdate.Images;
                findMovieToUpdate.TrailerMovie = findMovieToUpdate.TrailerMovie;
                findMovieToUpdate.IdCategory = model.IdCategory;
                _db.Movies.Update(findMovieToUpdate);
                await _db.SaveChangesAsync();


                //edit relationship Movie and Language
                findMovieToUpdate.IdLanguages.Clear();
                foreach (int item in model.idLanguages)
                {
                    var getLanguages = await _db.Languages.FindAsync(item);
                    if (getLanguages == null) return null;
                    findMovieToUpdate.IdLanguages.Add(getLanguages);
                }

                //edit realtionship Movie and Genres
                findMovieToUpdate.IdGenres.Clear();
                foreach (int item in model.idGenres)
                {
                    var getGenres = await _db.Genres.FindAsync(item);
                    if (getGenres == null) return null;
                    findMovieToUpdate.IdGenres.Add(getGenres);
                }

                //edit relationship Movie Countries
                findMovieToUpdate.IdCountries.Clear();
                foreach (int item in model.idCountries)
                {
                    var getCountries = await _db.Countries.FindAsync(item);
                    if (getCountries == null) return null;
                    findMovieToUpdate.IdCountries.Add(getCountries);
                }

                //add realtionship Movie Directors
                foreach (int item in model.idDirectors)
                {
                    var directors = await _db.Directors.FindAsync(item);
                    if (directors == null) return null;
                    findMovieToUpdate.IdDirectors.Add(directors);
                }

                //add realationship Movie PC
                foreach (int item in model.idProductionCompanies)
                {
                    var PC = await _db.ProductionCompanies.FindAsync(item);
                    if (PC == null) return null;
                    findMovieToUpdate.IdCompanies.Add(PC);
                }

                await _db.SaveChangesAsync();
                return findMovieToUpdate;
            }
            else if (cutFileNameImages != model.UploadImages.FileName || cutTrailerNameVideo != model.UploadVideoTrailer.FileName)
            {
                string delFilePathImages = Path.Combine(directoryPath, cutFileNameImages);
                string delFilPathVideoTrailer = Path.Combine(directoryPathVideoTraielr, cutTrailerNameVideo);
                System.IO.File.Delete(delFilePathImages);
                System.IO.File.Delete(delFilPathVideoTrailer);

                var filePathI = Path.Combine(directoryPath, model.UploadImages.FileName);
                var filePathVT = Path.Combine(directoryPathVideoTraielr, model.UploadVideoTrailer.FileName);

                await model.UploadVideoTrailer.CopyToAsync(new FileStream(filePathVT, FileMode.Create));
                await model.UploadImages.CopyToAsync(new FileStream(filePathI, FileMode.Create));

                string hostUrl = "https://localhost:7012/";
                string imagesUrl = hostUrl + "UploadImages/" + model.UploadImages.FileName;
                string videotrailerUrl = hostUrl + "UploadVideoTrailer/" + model.UploadVideoTrailer.FileName;
                model.Images = imagesUrl;
                model.TrailerMovie = videotrailerUrl;

                findMovieToUpdate.IdMovie = idMovie.Value;
                findMovieToUpdate.NameMovie = model.NameMovie;
                findMovieToUpdate.Showtimes = model.Showtimes;
                findMovieToUpdate.Description = model.Description;
                findMovieToUpdate.Trending = model.Trending;
                findMovieToUpdate.ReleaseDate = model.ReleaseDate;
                findMovieToUpdate.Tagline = model.Tagline;
                findMovieToUpdate.MovieStatus = model.MovieStatus;
                findMovieToUpdate.Images = imagesUrl;
                findMovieToUpdate.IdCategory = model.IdCategory;
                findMovieToUpdate.TrailerMovie = videotrailerUrl;
                _db.Movies.Update(findMovieToUpdate);
                await _db.SaveChangesAsync();

                //edit relationship Movie and Language
                findMovieToUpdate.IdLanguages.Clear();
                foreach (int item in model.idLanguages)
                {
                    var getLanguages = await _db.Languages.FindAsync(item);
                    if (getLanguages == null) return null;
                    findMovieToUpdate.IdLanguages.Add(getLanguages);
                }

                //edit realtionship Movie and Genres
                findMovieToUpdate.IdGenres.Clear();
                foreach (int item in model.idGenres)
                {
                    var getGenres = await _db.Genres.FindAsync(item);
                    if (getGenres == null) return null;
                    findMovieToUpdate.IdGenres.Add(getGenres);
                }

                //edit relationship Movie Countries
                findMovieToUpdate.IdCountries.Clear();
                foreach (int item in model.idCountries)
                {
                    var getCountries = await _db.Countries.FindAsync(item);
                    if (getCountries == null) return null;
                    findMovieToUpdate.IdCountries.Add(getCountries);
                }

                //add realtionship Movie Directors
                foreach (int item in model.idDirectors)
                {
                    var directors = await _db.Directors.FindAsync(item);
                    if (directors == null) return null;
                    findMovieToUpdate.IdDirectors.Add(directors);
                }

                //add realationship Movie PC
                foreach (int item in model.idProductionCompanies)
                {
                    var PC = await _db.ProductionCompanies.FindAsync(item);
                    if (PC == null) return null;
                    findMovieToUpdate.IdCompanies.Add(PC);
                }

                await _db.SaveChangesAsync();
                return findMovieToUpdate;
            }
            else
            {
                if(model == null && cutFileNameImages != model.UploadImages.FileName || cutTrailerNameVideo != model.UploadVideoTrailer.FileName) 
                {
                    string delFilePathImages = Path.Combine(directoryPath, cutFileNameImages);
                    string delFilPathVideoTrailer = Path.Combine(directoryPathVideoTraielr, cutTrailerNameVideo);
                    System.IO.File.Delete(delFilePathImages);
                    System.IO.File.Delete(delFilPathVideoTrailer);

                    var filePathI = Path.Combine(directoryPath, model.UploadImages.FileName);
                    var filePathVT = Path.Combine(directoryPathVideoTraielr, model.UploadVideoTrailer.FileName);

                    await model.UploadVideoTrailer.CopyToAsync(new FileStream(filePathVT, FileMode.Create));
                    await model.UploadImages.CopyToAsync(new FileStream(filePathI, FileMode.Create));


                    string hostUrl = "https://localhost:7012/";
                    string imagesUrl = hostUrl + "UploadImages/" + model.UploadImages.FileName;
                    string videotrailerUrl = hostUrl + "UploadVideoTrailer/" + model.UploadVideoTrailer.FileName;

                    model.Images = imagesUrl;
                    model.TrailerMovie = videotrailerUrl;

                    findMovieToUpdate.IdMovie = findMovieToUpdate.IdMovie;
                    findMovieToUpdate.NameMovie = findMovieToUpdate.NameMovie;
                    findMovieToUpdate.Showtimes = findMovieToUpdate.Showtimes;
                    findMovieToUpdate.Description = findMovieToUpdate.Description;
                    findMovieToUpdate.Trending = findMovieToUpdate.Trending;
                    findMovieToUpdate.ReleaseDate = findMovieToUpdate.ReleaseDate;
                    findMovieToUpdate.Tagline = findMovieToUpdate.Tagline;
                    findMovieToUpdate.MovieStatus = findMovieToUpdate.MovieStatus;
                    findMovieToUpdate.Images = imagesUrl;
                    findMovieToUpdate.TrailerMovie = videotrailerUrl;
                    findMovieToUpdate.IdCategory = findMovieToUpdate.IdCategory;
                    _db.Movies.Update(findMovieToUpdate);
                    await _db.SaveChangesAsync();


                    //edit relationship Movie and Language
                    findMovieToUpdate.IdLanguages.Clear();
                    foreach (int item in model.idLanguages)
                    {
                        var getLanguages = await _db.Languages.FindAsync(item);
                        if (getLanguages == null) return null;
                        findMovieToUpdate.IdLanguages.Add(getLanguages);
                    }

                    //edit realtionship Movie and Genres
                    findMovieToUpdate.IdGenres.Clear();
                    foreach (int item in model.idGenres)
                    {
                        var getGenres = await _db.Genres.FindAsync(item);
                        if (getGenres == null) return null;
                        findMovieToUpdate.IdGenres.Add(getGenres);
                    }

                    //edit relationship Movie Countries
                    findMovieToUpdate.IdCountries.Clear();
                    foreach (int item in model.idCountries)
                    {
                        var getCountries = await _db.Countries.FindAsync(item);
                        if (getCountries == null) return null;
                        findMovieToUpdate.IdCountries.Add(getCountries);
                    }

                    //add realtionship Movie Directors
                    foreach (int item in model.idDirectors)
                    {
                        var directors = await _db.Directors.FindAsync(item);
                        if (directors == null) return null;
                        findMovieToUpdate.IdDirectors.Add(directors);
                    }

                    //add realationship Movie PC
                    foreach (int item in model.idProductionCompanies)
                    {
                        var PC = await _db.ProductionCompanies.FindAsync(item);
                        if (PC == null) return null;
                        findMovieToUpdate.IdCompanies.Add(PC);
                    }

                    await _db.SaveChangesAsync();
                    return findMovieToUpdate;
                }
                else
                {
                    return findMovieToUpdate;
                }
                
            }

        }

        public async Task DeleteMovieAsync(int idMovie)
        {
            var findidMovie = await _db.Movies.Include(x => x.IdLanguages)
                                              .Include(c => c.IdGenres)
                                              .Include(p => p.IdCompanies)
                                              .Include(d => d.IdDirectors)
                                              .Where(x => x.IdMovie == idMovie)
                                              .FirstOrDefaultAsync();
            if (findidMovie == null) return;
            var directorPathI = Path.Combine(_webHostEnviroment.ContentRootPath, "UploadImages");
            var directoryPathVT = Path.Combine(_webHostEnviroment.ContentRootPath, "UploadVideoTrailer");
            var cutNameImages = findidMovie.Images.Split("/").Last();
            var cutNameVideoTrailer = findidMovie.TrailerMovie.Split("/").Last();
            var filePathI = Path.Combine(directorPathI, cutNameImages);
            var filePathVT = Path.Combine(directoryPathVT, cutNameVideoTrailer);
            System.IO.File.Delete(filePathI);
            System.IO.File.Delete(filePathVT);
            //delete all
            findidMovie.IdLanguages.Clear();
            findidMovie.IdGenres.Clear();
            findidMovie.IdCountries.Clear();
            findidMovie.IdDirectors.Clear();
            findidMovie.IdCompanies.Clear();

            _db.Movies.Remove(findidMovie);
            await _db.SaveChangesAsync();
        }

        public async Task<MapMovie> GetMovieByIDAsync(int idMovie)
        {

            var findIdMovie = await _db.Movies.Where(x => x.IdMovie == idMovie).FirstOrDefaultAsync();
            var getMapmovie = _mapper.Map<MapMovie>(findIdMovie);
            
            return getMapmovie;

        }

        public async Task<IEnumerable<Movie>> GetMovieByName(string name)
        {
            var findNameMovie = await _db.Movies.Where(x => x.NameMovie.ToLower().Contains(name.ToLower())).ToListAsync();
            return findNameMovie;
        }

        public async Task<IEnumerable<MapMovie>> GetAllMoviesPagingAsync(int? page, int pageSize)
        {
            var getAllMoviePaging = await _db.Movies.OrderBy(x => x.IdMovie)
                                                    .Skip((int)(page - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToListAsync();
            var mapAllMoviePaging = _mapper.Map<IEnumerable<MapMovie>>(getAllMoviePaging);
            return mapAllMoviePaging;
        }

        public async Task<int> CountMovieAsync()
        {
            var countMovie = await _db.Movies.CountAsync();
            return countMovie;
        }

        public async Task<IEnumerable<MapMovie>> SearchMoviesPagingAsync(SearchMovie model)
        {

            if (!string.IsNullOrEmpty(model.nameMovie))
            {
                var findNameMovie = await _db.Movies
                                       .Where(z => z.NameMovie.ToLower().Contains(model.nameMovie.ToLower()))
                                       .OrderBy(c => c.ReleaseDate)
                                       .Skip((int)(model.page - 1) * model.pageSize)
                                       .Take(model.pageSize)
                                       .ToListAsync();
                var mapSearchMoviePaging = _mapper.Map<IEnumerable<MapMovie>>(findNameMovie);
                return mapSearchMoviePaging;
            }
            else
            {
                var allMovies = await _db.Movies
                                       .OrderBy(c => c.ReleaseDate)
                                       .Skip((int)(model.page - 1) * model.pageSize)
                                       .Take(model.pageSize)
                                       .ToListAsync();
                var mapAllMovie = _mapper.Map<IEnumerable<MapMovie>>(allMovies);
                return mapAllMovie;
            }
            
        }

        public async Task<int> CountMovieSearchAsync(SearchMovie model)
        {
            if (!string.IsNullOrEmpty(model.nameMovie))
            {
                int countMovieSearch = await _db.Movies.Where(z => z.NameMovie.ToLower().Contains(model.nameMovie.ToLower())).CountAsync();
                return countMovieSearch;
            }

            var countMovie = await _db.Movies.CountAsync();
            return countMovie;

        }


        public async Task<IEnumerable<Movie>> SearchMovieWithCategories(int idCategories)
        {
            var find = await _db.Movies.Where(x => x.IdCategory == idCategories)
                                              .Include(x => x.IdLanguages)
                                              .Include(v => v.Videos)
                                              .Include(x => x.MovieCasts)
                                              .Include(g => g.IdGenres)
                                              .Include(z => z.IdCountries)
                                              .Include(c => c.IdCategoryNavigation)
                                              .Include(p => p.IdCompanies)
                                              .Include(d => d.IdDirectors).ToListAsync();
            return find;
        }

        public async Task<IEnumerable<Movie>> SearchMovieWithCountries(int idCountries)
        {
            var find = await _db.Movies.Where(x => x.IdCountries.Single().IdCountry == idCountries)
                                              .Include(x => x.IdLanguages)
                                              .Include(v => v.Videos)
                                              .Include(x => x.MovieCasts)
                                              .Include(g => g.IdGenres)
                                              .Include(z => z.IdCountries)
                                              .Include(c => c.IdCategoryNavigation)
                                              .Include(p => p.IdCompanies)
                                              .Include(d => d.IdDirectors).ToListAsync();
            return find;
        }

        public async Task<IEnumerable<Movie>> FilteringMovie(FilteringMovie request)
        {
            if (request.idGenres != null && request.idCountries != null && request.idLanguages != null && request.idCategories != null)
            {
                var find = await _db.Movies.Where(x => x.IdGenres.Single().IdGenre == request.idGenres
                                               && x.IdCountries.Single().IdCountry == request.idCountries
                                               && x.IdLanguages.Single().IdLanguage == request.idLanguages
                                               && x.IdCategory == request.idCategories)
                                              .Include(x => x.IdLanguages)
                                              .Include(v => v.Videos)
                                              .Include(x => x.MovieCasts)
                                              .Include(g => g.IdGenres)
                                              .Include(z => z.IdCountries)
                                              .Include(c => c.IdCategoryNavigation)
                                              .Include(p => p.IdCompanies)
                                              .Include(d => d.IdDirectors).ToListAsync();
                return find;
            }
            else
            {
                return await _db.Movies.Include(x => x.IdLanguages)
                                       .Include(v => v.Videos)
                                       .Include(x => x.MovieCasts)
                                       .Include(g => g.IdGenres)
                                       .Include(z => z.IdCountries)
                                       .Include(c => c.IdCategoryNavigation)
                                       .Include(p => p.IdCompanies)
                                       .Include(d => d.IdDirectors).ToListAsync();
            }

        }

        public async Task<IEnumerable<MapMovie>> FindTrenMovie()
        {
            var getTrenMovie = await _db.Movies.Where(x => x.Trending == true).Take(12).ToListAsync();
            var mapTrenMovie = _mapper.Map<IEnumerable<MapMovie>>(getTrenMovie);
            return mapTrenMovie;
        }

        public async Task<IEnumerable<MapMovie>> FindSeriesMovie()
        {
            var getSeriesMovie = await _db.Movies.Where(x => x.IdCategory == 2).Take(4).OrderByDescending(y=>y.ReleaseDate).ToListAsync();
            var mapSeriesMovie = _mapper.Map<IEnumerable<MapMovie>>(getSeriesMovie);
            return mapSeriesMovie;
        }

        public async Task<IEnumerable<MapMovie>> GetMoviesCategoryAsync(int idCategories)
        {
            var getMoviesCategory = await _db.Movies.Where(x => x.IdCategory == idCategories).ToListAsync();
            var maptMoviesCategory = _mapper.Map<IEnumerable<MapMovie>>(getMoviesCategory);
            return maptMoviesCategory;
        }

        public async Task<Category> GetNameCate(int idCate)
        {
            var getNameCate = await _db.Categories.Where(x=>x.IdCategory == idCate).FirstOrDefaultAsync();
            return getNameCate;
        }


        public async Task<IEnumerable<MapMovie>> GetMovieCountryAsync(int idCount)
        {
            var getMovieCountry = await _db.Movies.Where(x=>x.IdCountries.Single().IdCountry == idCount).ToListAsync();
            var mapMovieCountry = _mapper.Map<IEnumerable<MapMovie>>(getMovieCountry);
            return mapMovieCountry;
        }

        public async Task<IEnumerable<MapMovie>> SearchMovieWithGenresAsync(int idGenres)
        {
            var find = await _db.Movies.Where(x => x.IdGenres.Single().IdGenre == idGenres).ToListAsync();
            var mapMovieGenre = _mapper.Map<IEnumerable<MapMovie>>(find);
            return mapMovieGenre;
        }

        public async Task<IEnumerable<Video>> GetMovieVideoAsync(int idMovie)
        {
            var getMovieVideo = await _db.Videos.Where(x=>x.IdMovie == idMovie).ToListAsync();
            return getMovieVideo;
        }
    }
}
