namespace WebMovieOnline.Services.ICategories
{
    public interface ICategories
    {
        Task<IEnumerable<Category>> GetCategoriesAsync();
    }

    public class RepoCategories : ICategories
    {
        private readonly DbwebsiteMovieOnlineContext _db;
        public RepoCategories(DbwebsiteMovieOnlineContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _db.Categories.ToListAsync();
        }
    }
}
