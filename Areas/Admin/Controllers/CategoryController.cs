using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Pustokkk.Areas.Admin.ViewModels;
using MVC_Pustokkk.DAL;
using MVC_Pustokkk.Models;

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
            var categories = await _context.Categories.ToListAsync();
            var vms = categories.Select(c => new CategoryVM
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

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

            bool exists = await _context.Categories.AnyAsync(p => p.Name == category.Name);
            if (exists)
            {
                ModelState.AddModelError("Name", "Bu name sistemde var");
                return View(category);
            }

            if (!category.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Siz uygun formatda file elave etmirsiz.");
                return View(category);
            }

            if (category.Photo.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Photo", "Siz duzgun hecmde file elave etmirsiz.");
                return View(category);
            }

            string fileName = Guid.NewGuid().ToString() + category.Photo.FileName;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/image", fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await category.Photo.CopyToAsync(fileStream);
            }

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
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/image", fileName);
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

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || id < 1)
                return BadRequest();

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            var categoryVM = new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
            };

            return View(categoryVM);
        }
    }
}

