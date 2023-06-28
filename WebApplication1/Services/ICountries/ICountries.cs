using AutoMapper;
using WebMovieOnline.ModelViews;

namespace WebMovieOnline.Services.ICountry
{
    public interface ICountries
    {
        Task<IEnumerable<Country>> GetAllCountryAsync();
        Task<Country> AddCountryAsync(CountriesModelViews model);
        Task<Country> EditCountryAsync(CountriesModelViews model, int idCountry);
        Task DeleteCountryAsync(int idCountry);
    }
    public class RepoCoutries : ICountries
    {
        private readonly DbwebsiteMovieOnlineContext _db;
        private readonly IMapper _mapper;

        public RepoCoutries(DbwebsiteMovieOnlineContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<Country> AddCountryAsync(CountriesModelViews model)
        {
            var mapCountry = _mapper.Map<Country>(model);
            await _db.Countries.AddAsync(mapCountry);
            await _db.SaveChangesAsync();
            return mapCountry;
        }

        public async Task DeleteCountryAsync(int idCountry)
        {
            var findIdCountry = await _db.Countries.FindAsync(idCountry);
            _db.Countries.Remove(findIdCountry);
            await _db.SaveChangesAsync();
        }

        public async Task<Country> EditCountryAsync(CountriesModelViews model, int idCountry)
        {
            var findIdCountry = await _db.Countries.FindAsync(idCountry);
            if (findIdCountry != null)
            {
                findIdCountry.IdCountry = idCountry;
                findIdCountry.NameCountry = model.NameCountry;
                _db.Countries.Update(findIdCountry);
                await _db.SaveChangesAsync();
            }
            return findIdCountry;
        }

        public async Task<IEnumerable<Country>> GetAllCountryAsync()
        {
            return await _db.Countries.ToListAsync();
        }
    }
}
