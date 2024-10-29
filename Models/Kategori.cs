namespace ProjectCatalog.Models
{
   public class Kategori
{
    public int Id { get; set; }
    public string? Name { get; set; }

     public ICollection<Produk> Produks { get; set; } = new List<Produk>(); // Inisialisasi koleksi
   
}

}
