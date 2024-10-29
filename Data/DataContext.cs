using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProjectCatalog.Models;

public class DataContext : IdentityDbContext<ApplicationUser>
{
    public class ApplicationUser : IdentityUser
    {
        // Tambahkan properti tambahan jika diperlukan
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<ProjectCatalog.Models.Kategori> Kategori { get; set; } = default!;

    public DbSet<ProjectCatalog.Models.Supplier> Supplier { get; set; } = default!;

    public DbSet<ProjectCatalog.Models.Produk> Produk { get; set; } = default!;
}
