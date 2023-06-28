using AutoMapper;
using WebMovieOnline.ModelViews;

namespace WebMovieOnline.Services.IActorsOfAward
{
    public interface IActorsOfAward
    {
        Task<IEnumerable<ActorOfAward>> GetAllActorsOfAwardAsync();
        Task<ActorOfAward> AddActorsOfAwardAsync(ActorsOfAwardModelViews model);
        Task<ActorOfAward> EditActorsOfAwardAsync(ActorsOfAwardModelViews model, int idPerson);
        Task DeleteActorsOfAwardAsync(int idPerson);
    }
    public class RepoActorsOfAward : IActorsOfAward
    {
        private readonly DbwebsiteMovieOnlineContext _db;
        private readonly IMapper _mapper;
        public RepoActorsOfAward(DbwebsiteMovieOnlineContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ActorOfAward> AddActorsOfAwardAsync(ActorsOfAwardModelViews model)
        {
            var mapActorsAward = _mapper.Map<ActorOfAward>(model);
            await _db.ActorOfAwards.AddAsync(mapActorsAward);
            await _db.SaveChangesAsync();
            return mapActorsAward;

        }

        public async Task DeleteActorsOfAwardAsync(int idPerson)
        {
            var findIdPerSon = await _db.ActorOfAwards.FindAsync(idPerson);
            if (findIdPerSon != null)
            {
                _db.ActorOfAwards.Remove(findIdPerSon);
                await _db.SaveChangesAsync();
            }

        }

        public async Task<ActorOfAward> EditActorsOfAwardAsync(ActorsOfAwardModelViews model, int idPerson)
        {
            var findIdPerSon = await _db.ActorOfAwards.FindAsync(idPerson);
            if (findIdPerSon != null)
            {
                findIdPerSon.IdPerson = idPerson;
                findIdPerSon.IdAward = model.IdAward;
                findIdPerSon.AwardYear = model.AwardYear;
                _db.ActorOfAwards.Update(findIdPerSon);
                await _db.SaveChangesAsync();
            }
            return findIdPerSon;
        }

        public async Task<IEnumerable<ActorOfAward>> GetAllActorsOfAwardAsync()
        {
            return await _db.ActorOfAwards.ToListAsync();
        }
    }
}
