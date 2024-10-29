namespace ProjectCatalog.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Nama { get; set; } = string.Empty;
        public string nomorkontak { get; set; } = string.Empty;

         public ICollection<Produk> Produks { get; set; } = new List<Produk>(); // Inisialisasi koleksi
       
    }
}
