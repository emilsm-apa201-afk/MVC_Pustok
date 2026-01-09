using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Pustokkk.Areas.Admin.ViewModels;
using MVC_Pustokkk.DAL;
using MVC_Pustokkk.Models;
using NuGet.ContentModel;
using static System.Net.Mime.MediaTypeNames;

namespace MVC_Pustokkk.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vms = await _context.Categories
                .Select(c => new CategoryVM
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductCount = c.Products.Count 
                })
                .ToListAsync();

            return View(vms);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            bool exists = await _context.Categories.AnyAsync(c => c.Name == category.Name);
            if (exists)
            {
                ModelState.AddModelError("Name", "Bu category artıq mövcuddur");
                return View(category);
            }

            if (category.Photo == null)
            {
                ModelState.AddModelError("Photo", "Şəkil seçilməlidir");
                return View(category);
            }

            if (!category.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Yalnız şəkil faylı yükləyə bilərsiniz");
                return View(category);
            }

            if (category.Photo.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Photo", "Şəkil maksimum 2MB ola bilər");
                return View(category);
            }

            string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/products");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + category.Photo.FileName;
            string path = Path.Combine(folder, fileName);

            using var fs = new FileStream(path, FileMode.Create);
            await category.Photo.CopyToAsync(fs);

            category.Image = fileName;

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1)
                return BadRequest();

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (id == null || id < 1)
                return BadRequest();

            var exists = await _context.Categories.FindAsync(id);
            if (exists == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(category);

            bool nameExists = await _context.Categories.AnyAsync(c => c.Name == category.Name && c.Id != id);
            if (nameExists)
            {
                ModelState.AddModelError("Name", "Bu category artiq movcuddur");
                return View(category);
            }

            exists.Name = category.Name;
            if (category.Photo != null)
            {
                string fileName = Guid.NewGuid().ToString() + category.Photo.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images/products", fileName);
                using var fs = new FileStream(path, FileMode.Create);
                await category.Photo.CopyToAsync(fs);
                exists.Image = fileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            if (!category.IsDeleted)
                category.IsDeleted = true; 
            else
                _context.Categories.Remove(category); 

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products) 
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound();

            var vm = new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                ProductCount = category.Products.Count,
                //Photo = category.Products.

                Products = category.Products.Select(p => new ProductVM
                {
                    Id = p.Id,
                    Author = p.Author,
                    Details = p.Details,
                    Price = p.Price,
                    PriceOld = p.PriceOld,
                    PriceDiscount = p.PriceDiscount,
                    Image = p.Image,
                    Photo = p.Photo,
                }).ToList()
            };

            return View(vm);
        }
    }
}

