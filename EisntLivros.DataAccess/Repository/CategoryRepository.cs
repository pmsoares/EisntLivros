using EisntLivros.DataAccess.Data;
using EisntLivros.DataAccess.Repository.IRepository;
using EisntLivros.Models;

namespace EisntLivros.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
