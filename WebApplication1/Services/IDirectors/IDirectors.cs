using AutoMapper;
using WebMovieOnline.ModelViews;

namespace WebMovieOnline.Services.IDirectors
{
    public interface IDirectors
    {
        Task<IEnumerable<Director>> GetAllDirectorsAsync();
        Task<Director> AddDirectorsAsync(DirectorsModelViews model);
        Task<Director> EditDirectorsAsync(DirectorsModelViews model, int idDirectors);
        Task DeleteDirectors(int idDirector);
    }
    public class RepoDirectors : IDirectors
    {
        private readonly DbwebsiteMovieOnlineContext _db;
        private readonly IMapper _mapper;
        public RepoDirectors(DbwebsiteMovieOnlineContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<Director> AddDirectorsAsync(DirectorsModelViews model)
        {
            var mapDirectors = _mapper.Map<Director>(model);
            await _db.Directors.AddAsync(mapDirectors);
            await _db.SaveChangesAsync();
            return mapDirectors;
        }

        public async Task DeleteDirectors(int idDirectors)
        {
            var findIdDirectors = await _db.Directors.FindAsync(idDirectors);
            if (findIdDirectors != null)
            {
                _db.Directors.Remove(findIdDirectors);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<Director> EditDirectorsAsync(DirectorsModelViews model, int idDirectors)
        {
            var findIdDirectors = await _db.Directors.FindAsync(idDirectors);
            if (findIdDirectors != null)
            {
                findIdDirectors.IdDirector = idDirectors;
                findIdDirectors.FirstName = model.FirstName;
                findIdDirectors.LastName = model.LastName;
                findIdDirectors.Birhday = model.Birhday;
                findIdDirectors.Country = model.Country;
                _db.Directors.Update(findIdDirectors);
                await _db.SaveChangesAsync();
            }
            return findIdDirectors;
        }

        public async Task<IEnumerable<Director>> GetAllDirectorsAsync()
        {
            return await _db.Directors.ToListAsync();
        }
    }
}
