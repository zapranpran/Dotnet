namespace ProjectCatalog.Models
{
    public class Produk
    {
        
        public int Id { get; set; }

        public string NamaProduk { get; set; } = string.Empty;
       
        public decimal Harga { get; set; }

        public string Deskripsi { get; set; } = string.Empty;

        // Relasi dengan Kategori
        public int KategoriId { get; set; }
        public Kategori? Kategori { get; set; }

        // Relasi dengan Supplier
        public int SupplierId { get; set; }
        public Supplier? Supplier { get; set; }

        public string? ImageProduk { get; set;}
    }
}
