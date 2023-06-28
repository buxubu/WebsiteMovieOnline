using AutoMapper;
using WebMovieOnline.ModelViews;

namespace WebMovieOnline.Services.IAward
{
    public interface IAward
    {
        Task<IEnumerable<Award>> GetAllAwardAsync();
        Task<Award> AddAwardAsync(AwardModelViews model);
        Task<Award> EditAwardAsync(AwardModelViews model, int idAward);
        Task DeleteAwardAsync(int idAward);
    }
    public class RepoAward : IAward
    {
        private readonly DbwebsiteMovieOnlineContext _db;
        private readonly IMapper _mapper;
        public RepoAward(DbwebsiteMovieOnlineContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<Award> AddAwardAsync(AwardModelViews model)
        {
            var mapAward = _mapper.Map<Award>(model);
            await _db.Awards.AddAsync(mapAward);
            await _db.SaveChangesAsync();
            return mapAward;
        }

        public async Task DeleteAwardAsync(int idAward)
        {
            var findIdAward = await _db.Awards.FindAsync(idAward);
            if (findIdAward != null)
            {
                _db.Awards.Remove(findIdAward);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Award> EditAwardAsync(AwardModelViews model, int idAward)
        {
            var findIdAward = await _db.Awards.FindAsync(idAward);
            if (findIdAward != null)
            {
                findIdAward.IdAward = idAward;
                findIdAward.NameAward = model.NameAward;
                _db.Awards.Update(findIdAward);
                await _db.SaveChangesAsync();
            }
            return findIdAward;
        }

        public async Task<IEnumerable<Award>> GetAllAwardAsync()
        {
            return await _db.Awards.ToListAsync();
        }
    }
}
