using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tastys.Domain;

namespace Tastys.BLL;

public class RecetaService : IRecetaService
{
    private readonly ITastysContext _context;
    private readonly IMapper _mapper;

    public RecetaService(ITastysContext _context, IMapper _mapper)
    {
        this._context = _context;
        this._mapper = _mapper;
    }

    public async Task<List<RecetaDto>> GetOrderRecetas(int pageIndex, int pageSize, QueryOrdersRecetas order, int day = -7)
    {
        var desdeFecha = DateTime.UtcNow.AddDays(day);

        try
        {
            var recetasQuery = _context.Recetas
                .Include(r => r.Reviews)
                .Include(r => r.RecetaCategorias).ThenInclude(rc => rc.Categoria)
                .Include(r => r.Usuario)
                .AsQueryable();

            if (order == QueryOrdersRecetas.Fav)
                recetasQuery = recetasQuery
                    .Where(r => r.Reviews!.Any(review => review.create_at >= desdeFecha))
                    .OrderByDescending(r => r.Reviews!.Count(review => review.create_at >= desdeFecha));
            else if (order == QueryOrdersRecetas.CreateDate)
                recetasQuery = recetasQuery.OrderByDescending(r => r.RecetaID);

            var recetasPaged = await recetasQuery
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(r => new RecetaDto
                {
                    RecetaID = r.RecetaID,
                    Nombre = r.Nombre,
                    Descripcion = r.Descripcion,
                    ImageUrl = r.ImageUrl,
                    Usuario = new UsuarioPublicDto
                    {
                        UsuarioID = r.Usuario!.UsuarioID,
                        Nombre = r.Usuario.Nombre
                    },
                    Reviews = r.Reviews!.Select(review => new ReviewDto
                    {
                        ReviewID = review.ReviewID,
                        Comentario = review.Comentario,
                        Calificacion = review.Calificacion,
                        Usuario = new UsuarioPublicDto
                        {
                            UsuarioID = review.Usuario!.UsuarioID,
                            Nombre = review.Usuario.Nombre
                        }
                    }).ToList(),
                    Categorias = r.RecetaCategorias.Select(rc => new CategoriaDto
                    {
                        CategoriaID = rc.Categoria.CategoriaID,
                        Nombre = rc.Categoria.Nombre
                    }).ToList()
                })
                .ToListAsync();

            return recetasPaged;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Ocurrió un error al obtener las recetas.", ex);
        }
    }

    public async Task<List<RecetaDto>> GetAll()
    {
        return await _context.Recetas
            .Include(receta => receta.Usuario)
            .Include(receta => receta.Categorias)
            .Include(receta => receta.Reviews)
            //.ThenInclude(review => review.Usuario) // No lo incluyo porque es sería demasiada información innecesaria.
            .Where(r => !r.IsDeleted)
            .Select(receta => _mapper.Map<RecetaDto>(receta))
            .ToListAsync();
    }

    public async Task<List<RecetaDto>> GetAll(RecetasQuery queryParameters)
    {
        var query = _context.Recetas
            .Include(receta => receta.Usuario)
            .Include(receta => receta.Categorias)
            .Include(receta => receta.Reviews!.Take(queryParameters.CantReviews))
            .Where(r => !r.IsDeleted)
            .AsQueryable();

        // TODO: Full-text search en nombre + descripción, pero probablemente requiera
        // hacer cambios en la DB para incluir un índice, porque si no es muy ineficiente
        if (!string.IsNullOrWhiteSpace(queryParameters.S))
        {
            query = query.Where(receta =>
                receta.Nombre.Contains(queryParameters.S, StringComparison.CurrentCultureIgnoreCase) ||
                receta.Descripcion.Contains(queryParameters.S, StringComparison.CurrentCultureIgnoreCase));
        }

        return await query
            .Paginate(queryParameters)
            .Select(receta => _mapper.Map<RecetaDto>(receta))
            .ToListAsync();
    }

    public async Task<RecetaDto> GetByID(int ID)
    {
        var receta = await _context.Recetas
            .Include(receta => receta.Usuario)
            .Include(receta => receta.Categorias)
            .Include(receta => receta.Reviews!)
            .ThenInclude(review => review.Usuario)
            .Where(receta => !receta.IsDeleted)
            .FirstAsync(r => r.RecetaID == ID);

        if (receta == null)
            throw new NotFoundException(ID, "No se encontró una receta con esta ID");

        return _mapper.Map<RecetaDto>(receta);

        // Si salta una excepción se hace cargo el middleware
    }

    public async Task<bool> UpdateById(RecetaDto recetaDto, int ID)
    {
        var receta = await _context.Recetas.FindAsync(ID);

        if (receta == null)
            throw new NotFoundException(ID, "No se encontró una receta con esta ID");

        _mapper.Map(recetaDto, receta);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Receta> Create(Receta receta, List<string> list_c, string auth_id)
    {
        var userE = await _context.Usuarios.FirstOrDefaultAsync(u => u.Auth0Id == auth_id);

        if (userE == null)
        {
            int.TryParse(auth_id, out int usuarioId);

            userE = await _context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioID == usuarioId);

            if (userE == null || userE.IsDeleted)
            {
                throw new Exception("Usuario no encontrado o eliminado.");
            }
        }

        Receta newReceta = new Receta
        {
            Nombre = receta.Nombre,
            Descripcion = receta.Descripcion,
            ImageUrl = receta.ImageUrl,
            Usuario = userE
        };

        foreach (var categoria in list_c)
        {
            var categoriaE = await _context.Categorias.FirstOrDefaultAsync(c => c.Nombre.Equals(categoria, StringComparison.CurrentCultureIgnoreCase));

            if (categoriaE == null)
                throw new NotFoundException($"No hay una categoría con el nombre \"{categoria}\"");

            newReceta.Categorias?.Add(categoriaE);
        }

        _context.Recetas.Add(newReceta);
        await _context.SaveChangesAsync();

        return newReceta;
    }

    public async Task<bool> DeleteById(int ID)
    {
        var receta = await _context.Recetas.FindAsync(ID);

        if (receta == null)
            throw new NotFoundException(ID, "No se encontró una receta con esta ID");

        _context.Recetas.Remove(receta);
        await _context.SaveChangesAsync();

        return true;
    }
}
