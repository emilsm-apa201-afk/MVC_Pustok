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

        public async Task<IActionResult> Detail(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            ProductVM vm = new() { Categories = await _context.Categories.ToListAsync() };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM vm)
        {
            if (vm.Photo == null)
            {
                ModelState.AddModelError("Photo", "Şəkil mütləq yüklənməlidir");
                return View(vm);
            }

            string fileName = Guid.NewGuid() + vm.Photo.FileName;
            string path = Path.Combine(_env.WebRootPath, "assets/images/products", fileName);
            using (FileStream stream = new(path, FileMode.Create))
            {
                await vm.Photo.CopyToAsync(stream);
            }

            Product product = new()
            {
                Author = vm.Author,
                Details = vm.Details,
                Price = vm.Price,
                PriceOld = vm.PriceOld,
                PriceDiscount = vm.PriceDiscount,
                CategoryId = vm.CategoryId,
                Image = fileName
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            ProductVM vm = new()
            {
                Id = product.Id,
                Author = product.Author,
                Details = product.Details,
                Price = product.Price,
                PriceOld = product.PriceOld,
                PriceDiscount = product.PriceDiscount,
                CategoryId = product.CategoryId,
                Image = product.Image,
                Categories = await _context.Categories.ToListAsync()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, ProductVM vm)
        {
            vm.Categories = await _context.Categories.ToListAsync();
            var exist = await _context.Products.FindAsync(id);
            if (exist == null) return NotFound();

            if (!ModelState.IsValid) return View(vm);

            if (vm.Photo != null)
            {
                string oldPath = Path.Combine(_env.WebRootPath, "assets/images/products", exist.Image);
                if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);

                string fileName = Guid.NewGuid() + vm.Photo.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "assets/images/products", fileName);
                using (FileStream stream = new(newPath, FileMode.Create))
                {
                    await vm.Photo.CopyToAsync(stream);
                }
                exist.Image = fileName;
            }

            exist.Author = vm.Author;
            exist.Details = vm.Details;
            exist.Price = vm.Price;
            exist.PriceOld = vm.PriceOld;
            exist.PriceDiscount = vm.PriceDiscount;
            exist.CategoryId = vm.CategoryId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "assets/images/products", product.Image);
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}