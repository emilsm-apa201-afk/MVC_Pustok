using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Pustokkk.Areas.Admin.ViewModels;
using MVC_Pustokkk.DAL;
using MVC_Pustokkk.Models;

namespace MVC_Pustokkk.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new ProductVM
            {
                Categories = await _context.Categories.ToListAsync()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _context.Categories.ToListAsync();
                return View(model);
            }

            if (model.Photo == null || !model.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Düzgün şəkil yükləyin");
                model.Categories = await _context.Categories.ToListAsync();
                return View(model);
            }

            if (model.Photo.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Photo", "Şəkil maksimum 2MB ola bilər");
                model.Categories = await _context.Categories.ToListAsync();
                return View(model);
            }

            string fileName = Guid.NewGuid() + Path.GetExtension(model.Photo.FileName);
            string folderPath = Path.Combine(_env.WebRootPath, "assets/image");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string fullPath = Path.Combine(folderPath, fileName);
            using var stream = new FileStream(fullPath, FileMode.Create);
            await model.Photo.CopyToAsync(stream);

            
            var product = new Product
            {
                Author = model.Author,
                Details = model.Details,
                Image = fileName,
                Price = model.Price,
                PriceOld = model.PriceOld,
                PriceDiscount = model.PriceDiscount,
                CategoryId = model.CategoryId
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            var vm = new ProductVM
            {
                Id = product.Id,
                Author = product.Author,
                Details = product.Details,
                Image = product.Image,
                Price = product.Price,
                PriceOld = product.PriceOld,
                PriceDiscount = product.PriceDiscount,
                CategoryId = product.CategoryId,
                Categories = await _context.Categories.ToListAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, ProductVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _context.Categories.ToListAsync();
                return View(model);
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.Author = model.Author;
            product.Details = model.Details;
            product.Price = model.Price;
            product.PriceOld = model.PriceOld;
            product.PriceDiscount = model.PriceDiscount;
            product.CategoryId = model.CategoryId;

            if (model.Photo != null)
            {
                string fileName = Guid.NewGuid() + model.Photo.FileName;
                string path = Path.Combine(_env.WebRootPath, "assets/image", fileName);
                using var stream = new FileStream(path, FileMode.Create);
                await model.Photo.CopyToAsync(stream);
                product.Image = fileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            if (!product.IsDeleted)
                product.IsDeleted = true;
            else
                _context.Products.Remove(product);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}

