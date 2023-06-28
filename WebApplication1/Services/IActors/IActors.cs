using AutoMapper;
using WebMovieOnline.ModelViews;

namespace WebMovieOnline.Services.IActors
{
    public interface IActors
    {
        Task<IEnumerable<Actor>> GetAllActorsAsync();
        Task<Actor> GetIdActorAsync(int idActors);
        Task<Actor> AddActorsAsync(ActorsModelViews model);
        Task<Actor> EditActorsAsync(ActorsModelViews model, int idActors);
        Task DeleteActorAsync(int idActors);
    }
    public class RepoActors : IActors
    {
        private readonly DbwebsiteMovieOnlineContext _db;
        private readonly IMapper _mapper;
        public RepoActors(DbwebsiteMovieOnlineContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Actor> AddActorsAsync(ActorsModelViews model)
        {
            var mapActors = _mapper.Map<Actor>(model);
            await _db.Actors.AddAsync(mapActors);
            await _db.SaveChangesAsync();
            return mapActors;
        }

        public async Task DeleteActorAsync(int idActors)
        {
            var findIdActors = await _db.Actors.FindAsync(idActors);
            if (findIdActors != null)
            {
                _db.Actors.Remove(findIdActors);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Actor> EditActorsAsync(ActorsModelViews model, int idActors)
        {
            var findIdActor = await _db.Actors.FindAsync(idActors);
            if (findIdActor != null)
            {
                findIdActor.IdPerson = model.IdPerson;
                findIdActor.FirstName = model.FirstName;
                findIdActor.LastName = model.LastName;
                findIdActor.Address = model.Address;
                findIdActor.Birthday = model.Birthday;
                findIdActor.Sex = model.Sex;
                findIdActor.Country = model.Country;
                _db.Actors.Update(findIdActor);
                await _db.SaveChangesAsync();
            }
            return findIdActor;
        }

        public async Task<IEnumerable<Actor>> GetAllActorsAsync()
        {
            return await _db.Actors.Include(x => x.MovieCasts)
                                   .Include(x => x.ActorOfAwards)
                                   .ToListAsync();
        }

        public async Task<Actor> GetIdActorAsync(int idActors)
        {
            return await _db.Actors.FindAsync(idActors);
        }
    }
}
