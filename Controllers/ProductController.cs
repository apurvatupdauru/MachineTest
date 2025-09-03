using MachineTest.Data;
using MachineTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MachineTest.Controllers
{
    public class ProductController : Controller
    {
        private readonly MachineTestDbContext db;

        public ProductController(MachineTestDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var products = db.Products.Include(p => p.Category).OrderBy(p => p.ProductId).Skip((page - 1) * pageSize)
        .Take(pageSize).ToList();

            int totalRecords = db.Products.Count();
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            ViewBag.CurrentPage = page;

            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product p)
        {

            db.Products.Add(p);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));


        }

        public IActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product p)
        {

            db.Products.Update(p);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));


        }

        public IActionResult Delete(int id)
        {
            var product = db.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Product p)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}