using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCatalog.Models;


namespace ProjectCatalog.Controllers
{
    public class FrontendController : Controller
    {
        private readonly DataContext _context;

        public FrontendController(DataContext context)
        {
            _context = context;
        }

        // Action untuk menampilkan semua produk di halaman guest
        [HttpGet("Guest")]

        public async Task<IActionResult> Index()
        {
            var products = await _context.Produk
                .Include(p => p.Kategori)
                .Include(p => p.Supplier)
                .ToListAsync();
            return View("Views/Frontend/Guest.cshtml", products);
        }

    }
}
