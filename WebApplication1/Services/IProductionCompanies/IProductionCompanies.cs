using AutoMapper;
using WebMovieOnline.ModelViews;

namespace WebMovieOnline.Services.IProductionCompany
{
    public interface IProductionCompanies
    {
        Task<IEnumerable<ProductionCompany>> GetAllProductionCompaniesAsync();
        Task<ProductionCompany> AddProductionCompaniesAsync(ProductionCompaniesModelViews model);
        Task<ProductionCompany> EditProductionCompaniesAsync(ProductionCompaniesModelViews model, int idPC);
        Task DeleteProductionCompaniesAsync(int idPC);
    }
    public class RepoProductionCompanies : IProductionCompanies
    {
        private readonly DbwebsiteMovieOnlineContext _db;
        private readonly IMapper _mapper;
        public RepoProductionCompanies(DbwebsiteMovieOnlineContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ProductionCompany> AddProductionCompaniesAsync(ProductionCompaniesModelViews model)
        {
            var mapPC = _mapper.Map<ProductionCompany>(model);
            await _db.ProductionCompanies.AddAsync(mapPC);
            await _db.SaveChangesAsync();
            return mapPC;
        }

        public async Task DeleteProductionCompaniesAsync(int idPC)
        {
            var findPC = await _db.ProductionCompanies.FindAsync(idPC);
            if (findPC != null)
            {
                _db.ProductionCompanies.Remove(findPC);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<ProductionCompany> EditProductionCompaniesAsync(ProductionCompaniesModelViews model, int idPC)
        {
            var findPC = await _db.ProductionCompanies.FindAsync(idPC);
            if (findPC != null)
            {
                findPC.IdCompany = idPC;
                findPC.NameCompany = model.NameCompany;
                _db.ProductionCompanies.Update(findPC);
                await _db.SaveChangesAsync();
            }
            return findPC;
        }

        public async Task<IEnumerable<ProductionCompany>> GetAllProductionCompaniesAsync()
        {
            return await _db.ProductionCompanies.ToListAsync();
        }
    }
}
