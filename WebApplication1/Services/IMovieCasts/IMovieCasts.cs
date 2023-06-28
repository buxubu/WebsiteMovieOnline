using AutoMapper;
using WebMovieOnline.ModelViews;

namespace WebMovieOnline.Services.IMovieCasts
{
    public interface IMovieCasts
    {
        Task<IEnumerable<MovieCast>> GetAllMovieCastAsync();
        Task<MovieCast> AddMovieCastAsync(MovieCastModelViews model);
        Task<MovieCast> EditMovieCastAsync(MovieCastModelViews model, int idPerson);
        Task DeleteMovieCastAsync(int idPerson);
    }
    public class RepoMovieCasts : IMovieCasts
    {
        private readonly DbwebsiteMovieOnlineContext _db;
        private readonly IMapper _mapper;
        public RepoMovieCasts(DbwebsiteMovieOnlineContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<MovieCast> AddMovieCastAsync(MovieCastModelViews model)
        {
            var mapMovieCast = _mapper.Map<MovieCast>(model);
            await _db.MovieCasts.AddAsync(mapMovieCast);
            await _db.SaveChangesAsync();
            return mapMovieCast;
        }

        public async Task DeleteMovieCastAsync(int idPerson)
        {
            var findIdPerson = await _db.MovieCasts.FindAsync(idPerson);
            _db.MovieCasts.Remove(findIdPerson);
            await _db.SaveChangesAsync();
        }

        public async Task<MovieCast> EditMovieCastAsync(MovieCastModelViews model, int idPerson)
        {
            var findIdPerson = await _db.MovieCasts.FindAsync(idPerson);
            if (findIdPerson != null)
            {
                findIdPerson.IdMovie = model.IdMovie;
                findIdPerson.IdPerson = model.IdPerson;
                findIdPerson.CharacterName = model.CharacterName;
                findIdPerson.Position = model.Position;
                _db.MovieCasts.Update(findIdPerson);
                await _db.SaveChangesAsync();
            }
            return findIdPerson;
        }

        public async Task<IEnumerable<MovieCast>> GetAllMovieCastAsync()
        {
            return await _db.MovieCasts.Include(x => x.IdPersonNavigation).ToListAsync();
        }
    }
}
