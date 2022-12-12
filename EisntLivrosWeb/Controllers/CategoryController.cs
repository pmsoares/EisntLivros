using EisntLivrosWeb.Data;
using EisntLivrosWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace EisntLivrosWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db) => _db = db;

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoriesList = _db.Categories;
            return View(objCategoriesList);
        }
    }
}
