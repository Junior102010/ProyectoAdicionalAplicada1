using Microsoft.EntityFrameworkCore;
using ProyectoAdicionalAplicada1.Data;
using ProyectoAdicionalAplicada1.Models;

namespace ProyectoAdicionalAplicada1.Services;

public class ProductoServices(IDbContextFactory<ApplicationDbContext> DbFactory)
{
    public async Task<bool> Insertar(Producto producto)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Producto.Add(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Producto producto)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Producto.Update(producto);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id) 
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Producto
            .Where(p => p.ProductoId == id)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Producto.AnyAsync(p => p.ProductoId == id);
    }
}
