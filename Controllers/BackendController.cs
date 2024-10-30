
using Microsoft.AspNetCore.Mvc; 
using ProjectCatalog.Models;

namespace ProjectCatalog.Controllers
{
    public class BackendController : Controller
    {
        private readonly DataContext _context;

        public BackendController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var KategoriCount = _context.Kategori.Count();
            var SupplierCount = _context.Supplier.Count();
            var ProdukCount = _context.Produk.Count();

            ViewBag.KategoriCount = KategoriCount;
            ViewBag.supplierCount = SupplierCount;
            ViewBag.ProdukCount = ProdukCount;

            return View("Views/Dashboard.cshtml");
        }
    }
}