using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectCatalog.Models;

namespace ProjectCatalog.Controllers
{
    public class ProdukController : Controller
    {
        private readonly DataContext _context;
        private readonly string _uploadFolder;

        public ProdukController(DataContext context)
        {
            _context = context;

            _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image/produk");

            if(!Directory.Exists(_uploadFolder))
            {
                Directory.CreateDirectory(_uploadFolder);   
            }
        }

        // GET: Produk
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.Produk.Include(p => p.Kategori).Include(p => p.Supplier);
            return View(await dataContext.ToListAsync());
        }

        // GET: Produk/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produk = await _context.Produk
                .Include(p => p.Kategori)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produk == null)
            {
                return NotFound();
            }

            return View(produk);
        }

        // GET: Produk/Create
        public IActionResult Create()
        {
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id", "Id");
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Id");
            return View();
        }

        // POST: Produk/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NamaProduk,Harga,Deskripsi,KategoriId,SupplierId")] Produk produk, IFormFile ImageProduk)
        {
            if (ModelState.IsValid)
            {
                if (ImageProduk != null && ImageProduk.Length > 0)
                {
                    var randomName = $"{new Random().Next(2000, 9999)}_{Path.GetFileName(ImageProduk.FileName)}";
                    var filePath = Path.Combine(_uploadFolder, randomName);

                    // Pastikan folder upload ada
                    if (!Directory.Exists(_uploadFolder))
                    {
                        Directory.CreateDirectory(_uploadFolder);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageProduk.CopyToAsync(stream);
                    }

                    produk.ImageProduk = randomName;
                }
                
                _context.Add(produk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id", "Id", produk.KategoriId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Id", produk.SupplierId);
            return View(produk);
        }

        // GET: Produk/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produk = await _context.Produk.FindAsync(id);
            if (produk == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id", "Id", produk.KategoriId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Id", produk.SupplierId);
            return View(produk);
        }

        // POST: Produk/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NamaProduk,Harga,Deskripsi,KategoriId,SupplierId")] Produk produk, IFormFile ImageProduk)
        {
            if (id != produk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (ImageProduk != null && ImageProduk.Length > 0)
                {
                    // Delete the old image if it exists
                    if (!string.IsNullOrEmpty(produk.ImageProduk))
                    {
                        var oldImagePath = Path.Combine(_uploadFolder, produk.ImageProduk);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Create a new random name for the uploaded image
                    var randomName = $"{new Random().Next(2000, 9999)}_{Path.GetFileName(ImageProduk.FileName)}";
                    var filePath = Path.Combine(_uploadFolder, randomName);

                    // Ensure the upload directory exists
                    if (!Directory.Exists(_uploadFolder))
                    {
                        Directory.CreateDirectory(_uploadFolder);
                    }

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageProduk.CopyToAsync(stream);
                    }

                    // Update the image property in your model
                    produk.ImageProduk = randomName;
                }
                else
                {
                    TempData["ErrorMessage"] = "Image harus Diisi.";
                }

                try
                {
                    _context.Update(produk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdukExists(produk.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategori, "Id", "Id", produk.KategoriId);
            ViewData["SupplierId"] = new SelectList(_context.Supplier, "Id", "Id", produk.SupplierId);
            return View(produk);
        }

        // GET: Produk/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produk = await _context.Produk
                .Include(p => p.Kategori)
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produk == null)
            {
                return NotFound();
            }

            return View(produk);
        }

        // POST: Produk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produk = await _context.Produk.FindAsync(id);
            if (produk != null)
            {
                _context.Produk.Remove(produk);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdukExists(int id)
        {
            return _context.Produk.Any(e => e.Id == id);
        }
    }
}
