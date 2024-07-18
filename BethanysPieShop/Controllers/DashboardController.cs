using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class DashboardController : Controller
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;
        public DashboardController(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

        public IActionResult Index()
        {



            List<Category> Categories = _bethanysPieShopDbContext.Categories.ToList();

            return View(Categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {

            if (ModelState.IsValid)
            {
                _bethanysPieShopDbContext.Categories.Add(obj);
                _bethanysPieShopDbContext.SaveChanges();
                TempData["success"] = "Has been Created successfly";

                return RedirectToAction("Index");
            }

            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category category = _bethanysPieShopDbContext.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _bethanysPieShopDbContext.Categories.Update(obj);
                _bethanysPieShopDbContext.SaveChanges();
                TempData["success"] = "Has been Edit successfly";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category category = _bethanysPieShopDbContext.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _bethanysPieShopDbContext.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (obj == null)
            {
                return NotFound();

            }
            _bethanysPieShopDbContext.Categories.Remove(obj);
            _bethanysPieShopDbContext.SaveChanges();
            TempData["success"] = "Has been Deleted successfly";

            return RedirectToAction("Index");
        }


    }
}
