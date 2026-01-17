using Microsoft.EntityFrameworkCore;
using ProyectoAdicionalAplicada1.Data;
using ProyectoAdicionalAplicada1.Models;
using System.Linq.Expressions;

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

    public async Task<bool> Guardar(Producto producto)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        if (await Existe(producto.ProductoId) != true && producto != null) {
         
                return await Insertar(producto);
        }
        else 
        {
                return await Modificar(producto);
        }
       
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

    public async Task<Producto?> Buscar(int Id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Producto.FirstOrDefaultAsync(e => e.ProductoId == Id);
    }
    public async Task<List<Producto>> Listar(Expression<Func<Producto, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Producto
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

}
