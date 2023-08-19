using AutoMapper;
using WebMovieOnline.ModelViews;

namespace WebMovieOnline.Services.IGenres
{
    public interface IGenres
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync();
        Task<Genre> AddGenresAsync(GenresModelViews model);
        Task<Genre> EditGenresAsync(GenresModelViews model, int idGenres);
        Task DeleteGenresAsync(int idGenres);
    }
    public class RepoGenres : IGenres
    {
        private readonly DbwebsiteMovieOnlineContext _db;
        private readonly IMapper _mapper;
        public RepoGenres(DbwebsiteMovieOnlineContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Genre> AddGenresAsync(GenresModelViews model)
        {
            var mapGenres = _mapper.Map<Genre>(model);
            await _db.Genres.AddAsync(mapGenres);
            await _db.SaveChangesAsync();
            return mapGenres;
        }

        public async Task DeleteGenresAsync(int idGenres)
        {
            var findIdGenres = await _db.Genres.FindAsync(idGenres);
            _db.Genres.Remove(findIdGenres);
            await _db.SaveChangesAsync();

        }

        public async Task<Genre> EditGenresAsync(GenresModelViews model, int idGenres)
        {
            var findIdGenres = await _db.Genres.FindAsync(idGenres);
            if (findIdGenres != null)
            {
                findIdGenres.IdGenre = idGenres;
                findIdGenres.NameGenre = model.NameGenre;
                _db.Genres.Update(findIdGenres);
                await _db.SaveChangesAsync();
            }
            return findIdGenres;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            var getAll = await _db.Genres.Take(3).ToListAsync();
            return getAll;
        }
    }
}
