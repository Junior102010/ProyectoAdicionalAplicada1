using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProyectoAdicionalAplicada1.Models;

namespace ProyectoAdicionalAplicada1.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{

    public DbSet<Producto> Producto { get; set; }

    public DbSet<Entrada> Entrada { get; set; }

    public DbSet<Entrada> EntradaDetalle { get; set; }
}

    
