using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Tastys.BLL.Interfaces;
using Tastys.Domain;

namespace Tastys.BLL;
public class CategoriaService : ICategoriaService
{
    private readonly IMapper _mapper;
    private readonly ITastysContext _context;

    public CategoriaService(ITastysContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<Categoria> GetCategorias(int pageIndex,int pageSize)
    {
        IQueryable<Categoria> categorias = _context.Categorias.AsQueryable();
        int skip = (pageIndex - 1) * pageSize;
        try
        {
            List<Categoria> allCategorias = categorias.Skip(skip)
            .Take(pageIndex)
            .ToList();

            return allCategorias;
        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
    /// <summary>
    /// Obtener todas las categorias, incluyendo sus recetas.
    /// </summary>
    public async Task<List<CategoriaConRecetasDto>> GetCategorias(CategoriasQuery queryParameters)
    {
        var query = _context.Categorias.AsQueryable();

        switch (queryParameters.OrdenPorCantRecetas)
        {
            case Ordenamiento.Ascendente:
                query = query.OrderBy(cat => cat.Recetas.Count);
                break;
            case Ordenamiento.Descendente:
                query = query.OrderByDescending(cat => cat.Recetas.Count);
                break;
        }

        var categorias = await query.Paginate(queryParameters).ToListAsync();
        var dtos = new List<CategoriaConRecetasDto>();

        // Obtengo las recetas de cada categoria en una query separada
        // Esto es a propósito, porque EF Core no permite limitar la cantidad
        // de resultados en un Include (que en SQL sería un JOIN)
        foreach (var categoria in categorias)
        {
            var dto = _mapper.Map<CategoriaConRecetasDto>(categoria);

            var recetasQuery = _context.Recetas
                .Include(receta => receta.Categorias)
                .Where(receta => receta.Categorias.Contains(categoria));

            var totalRecetas = await recetasQuery.CountAsync();

            if (queryParameters.CantRecetas.HasValue)
            {
                recetasQuery = recetasQuery.Take(queryParameters.CantRecetas.Value);
            }

            dto.Recetas = await recetasQuery.Select(receta => _mapper.Map<RecetaDto>(receta)).ToArrayAsync();
            dto.TotalRecetas = totalRecetas;

            dtos.Add(dto);
        }

        return dtos;
    }

    /// <summary>
    /// Obtiene el detalle de una categoria con una lista de sus recetas posiblemente filtrada u ordenada.
    /// </summary>
    public async Task<CategoriaConRecetasDto> GetCategoriaById(int id, CategoriaByIdQuery queryParameters)
    {
        // No hago Include("Recetas") porque EF Core no permite limitar la cantidad
        // de resultados en un Include (que en SQL sería un JOIN)
        var categoria = await _context.Categorias.FirstAsync(cat => cat.CategoriaID == id);

        var recetasQuery = _context.Recetas
            .Include(receta => receta.Categorias)
            .Where(receta => receta.Categorias.Contains(categoria));

        var totalRecetas = await recetasQuery.CountAsync();

        recetasQuery = recetasQuery.Paginate(queryParameters);

        switch (queryParameters.OrdenPorCantReviews)
        {
            case Ordenamiento.Ascendente:
                recetasQuery = recetasQuery.OrderBy(receta => receta.Reviews.Count);
                break;
            case Ordenamiento.Descendente:
                recetasQuery = recetasQuery.OrderByDescending(receta => receta.Reviews.Count);
                break;
        }

        switch (queryParameters.OrdenPorFecha)
        {
            case Ordenamiento.Ascendente:
                recetasQuery = recetasQuery.OrderBy(receta => receta.create_at);
                break;
            case Ordenamiento.Descendente:
                recetasQuery = recetasQuery.OrderByDescending(receta => receta.create_at);
                break;
        }

        switch (queryParameters.OrdenPorPuntuacion)
        {
            case Ordenamiento.Ascendente:
                recetasQuery = recetasQuery.OrderBy(receta => receta.Reviews.Average(rev => rev.Calificacion));
                break;
            case Ordenamiento.Descendente:
                recetasQuery = recetasQuery.OrderByDescending(receta => receta.Reviews.Average(rev => rev.Calificacion));
                break;
        }

        var categoriaDto = _mapper.Map<CategoriaConRecetasDto>(categoria);
        var recetasDtos = await recetasQuery.Select(receta => _mapper.Map<RecetaDto>(receta)).ToArrayAsync();

        categoriaDto.Recetas = recetasDtos;
        categoriaDto.TotalRecetas = totalRecetas;

        return categoriaDto;
    }
}
