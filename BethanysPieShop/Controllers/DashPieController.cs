using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
        public class DashPieController : Controller
        {
            private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;
            public DashPieController(BethanysPieShopDbContext bethanysPieShopDbContext)
            {
            _bethanysPieShopDbContext= bethanysPieShopDbContext;

            }
            public IActionResult Index()
            {
                List<Pie> Pies = _bethanysPieShopDbContext.Pies.ToList();
                return View(Pies);
            }


            public IActionResult Create()
            {
            var model = new PieWithCategories
            {
                categories = _bethanysPieShopDbContext.Categories.ToList(),
            };

                return View(model);
            }

            [HttpPost]
            public IActionResult Create(PieWithCategories obj)
            {

                if (ModelState.IsValid)
                {
                _bethanysPieShopDbContext.Pies.Add(obj.pie);
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

                Pie pie = _bethanysPieShopDbContext.Pies.FirstOrDefault(c => c.PieId == id);
                if (pie == null)
                {
                    return NotFound();
                }

                return View(pie);
            }

            [HttpPost]
            public IActionResult Edit(Pie obj)
            {
                if (ModelState.IsValid)
                {
                    _bethanysPieShopDbContext.Pies.Update(obj);
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
                Pie pie = _bethanysPieShopDbContext.Pies.FirstOrDefault(c => c.PieId == id);
                if (pie == null)
                {
                    return NotFound();
                }
                return View(pie);
            }

            [HttpPost, ActionName("Delete")]
            public IActionResult DeletePost(int? id)
            {
                Pie? obj = _bethanysPieShopDbContext.Pies.FirstOrDefault(c => c.PieId == id);
                if (obj == null)
                {
                    return NotFound();

                }
                _bethanysPieShopDbContext.Pies.Remove(obj);
                _bethanysPieShopDbContext.SaveChanges();
                TempData["success"] = "Has been Deleted successfly";

                return RedirectToAction("Index");
            }


        }
    }
