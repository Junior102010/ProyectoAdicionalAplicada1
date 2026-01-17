using Microsoft.EntityFrameworkCore;
using ProyectoAdicionalAplicada1.Data;
using ProyectoAdicionalAplicada1.Models;
using System;
using System.Linq.Expressions;

namespace ProyectoAdicionalAplicada1.Services;


public class EntradasServices(IDbContextFactory<ApplicationDbContext> DbFactory)
{
    public async Task<bool> Insertar(Entrada Entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Entrada.Add(Entrada);

        await AfectarExistencia(contexto, Entrada.Detalles.ToArray(), true);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Entrada entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var entradaAnterior = await contexto.Entrada
                .Include(e => e.Detalles)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EntradaId == entrada.EntradaId);

        if (entradaAnterior == null) return false;

        await AfectarExistencia(contexto, entradaAnterior.Detalles.ToArray(), false);

        contexto.Entrada.Update(entrada);

        await AfectarExistencia(contexto, entrada.Detalles.ToArray(), true);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var entrada = await contexto.Entrada
        .Include(p => p.Detalles)
        .FirstOrDefaultAsync(p => p.EntradaId == id);

        if (entrada == null) return false;
        await AfectarExistencia(contexto, entrada.Detalles.ToArray(), false);
        contexto.Entrada.Remove(entrada);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<Entrada?> Buscar(int Id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Entrada.Include(d=> d.Detalles).FirstOrDefaultAsync(e => e.EntradaId == Id);
    }

    public async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Entrada.AnyAsync(p => p.EntradaId == id);
    }

    public async Task<List<Entrada>> Listar(Expression<Func<Entrada, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Entrada
            .Include(e => e.Detalles)
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Guardar(Entrada entrada)
    {
        if (!await Existe(entrada.EntradaId))
        {
            return await Insertar(entrada);
        }
        else
        {
            return await Modificar(entrada);
        }
    }

    private async Task AfectarExistencia(ApplicationDbContext contexto, EntradaDetalle[] detalles, bool sumar)
    {
        foreach (var item in detalles)
        {
            var producto = await contexto.Producto.FindAsync(item.ProductoId);
            if (producto != null)
            {
                if (sumar)
                    producto.Existencia += item.Cantidad;
                else
                    producto.Existencia -= item.Cantidad;

                contexto.Producto.Update(producto);
            }
        }
    }
}
