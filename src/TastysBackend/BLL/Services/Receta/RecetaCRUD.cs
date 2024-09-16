using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tastys.Domain;

namespace Tastys.BLL.Services.RecetaCRUD
{
    public class RecetaCRUD : IRecetaService
    {
        private readonly ITastysContext _Context;
        private readonly IMapper _Mapper;

        public RecetaCRUD(ITastysContext _context, IMapper _mapper)
        {
            _Context = _context;
            _Mapper = _mapper;
        }
        public async Task<List<RecetaDto>> GetOrderRecetas(int pageIndex, int pageSize, QueryOrdersRecetas order, int day = -7)
        {
            var desdeFecha = DateTime.UtcNow.AddDays(day);

            try
            {
                var recetasQuery = _Context.Recetas
                    .Include(r => r.Reviews)
                    .Include(r => r.RecetaCategorias).ThenInclude(rc => rc.Categoria)
                    .Include(r => r.Usuario)
                    .AsQueryable();

                if (order == QueryOrdersRecetas.Fav)
                {
                    recetasQuery = recetasQuery
                        .Where(r => r.Reviews.Any(review => review.create_at >= desdeFecha))
                        .OrderByDescending(r => r.Reviews.Count(review => review.create_at >= desdeFecha));
                }
                else if (order == QueryOrdersRecetas.CreateDate)
                {
                    recetasQuery = recetasQuery.OrderByDescending(r => r.RecetaID);
                }

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
                            UsuarioID = r.Usuario.UsuarioID,
                            Nombre = r.Usuario.Nombre
                        },
                        Reviews = r.Reviews.Select(review => new ReviewDto
                        {
                            ReviewID = review.ReviewID,
                            Comentario = review.Comentario,
                            Calificacion = review.Calificacion,
                            Usuario = new UsuarioPublicDto
                            {
                                UsuarioID = review.Usuario.UsuarioID,
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




        public async Task<List<RecetaDto>> GetAllRecetas()
        {
            return await _Context.Recetas
                .Include(r => r.Usuario)
                .Include(r => r.RecetaCategorias)
                .ThenInclude(rc => rc.Categoria)
                .Include(r => r.Reviews)
                .ThenInclude(review => review.Usuario)
                .Where(r => !r.IsDeleted)
                .Select(r => new RecetaDto
                {
                    RecetaID = r.RecetaID,
                    Nombre = r.Nombre,
                    Descripcion = r.Descripcion,
                    ImageUrl = r.ImageUrl,
                    Usuario = new UsuarioPublicDto
                    {
                        UsuarioID = r.Usuario.UsuarioID,
                        Nombre = r.Usuario.Nombre
                    },
                    Reviews = r.Reviews.Select(review => new ReviewDto
                    {
                        ReviewID = review.ReviewID,
                        Comentario = review.Comentario,
                        Calificacion = review.Calificacion,
                        Usuario = new UsuarioPublicDto
                        {
                            UsuarioID = review.Usuario.UsuarioID,
                            Nombre = review.Usuario.Nombre
                        }
                    }).ToList(),
                    Categorias = r.RecetaCategorias.Select((rc) => new CategoriaDto
                    {
                        CategoriaID = rc.Categoria.CategoriaID,
                        Nombre = rc.Categoria.Nombre
                    }).ToList()
                })
                .ToListAsync();
        }



        public async Task<RecetaDto> GetRecetaByID(int ID)
        {
            try
            {
                var receta = await _Context.Recetas.Where(receta => !receta.IsDeleted).FirstAsync(r => r.RecetaID == ID);
                if (receta == null)
                {
                    throw new KeyNotFoundException($"Receta con ID {ID} no fue encontrada");
                }
                return _Mapper.Map<RecetaDto>(receta);
            }
            catch
            {
                throw new ApplicationException("Algo falló");
            }
        }
        public async Task<bool> UpdateReceta(RecetaDto recetaDto, int ID)
        {

            try
            {
                var receta = await _Context.Recetas.FindAsync(ID);
                if (receta == null)
                {
                    throw new KeyNotFoundException($"Receta con ID {ID} no fue encontrada");
                }
                _Mapper.Map(recetaDto, receta);
                await _Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Algo falló al actualizar la receta, {ex}");
            }
        }
        public async Task<Receta> CreateReceta(Receta receta, List<string> list_c, string auth_id)
        {
            try
            {
                Usuario userE = await _Context.Usuarios.FirstOrDefaultAsync(u => u.Auth0Id == auth_id);

                if (userE == null)
                {
                    int.TryParse(auth_id,out int usuarioId);

                    userE = await _Context.Usuarios.FirstOrDefaultAsync(u => u.UsuarioID == usuarioId);

                    if (userE == null || userE.IsDeleted != null)
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
                    Categoria categoriaE = await _Context.Categorias.FirstOrDefaultAsync(c => c.Nombre == categoria.ToLower());

                    if (categoriaE == null)
                    {
                        throw new Exception($"La categoría '{categoria}' no existe.");
                    }
                    newReceta.Categorias.Add(categoriaE);
                }

                _Context.Recetas.Add(newReceta);

                await _Context.SaveChangesAsync();

                Console.WriteLine("RECETA CREADA!");

                return newReceta;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Algo falló al crear la receta" + ex.Message);
            }
        }

        public async Task<bool> DeleteReceta(int ID)
        {
            try
            {
                var receta = await _Context.Recetas.FindAsync(ID);
                if (receta == null)
                {
                    throw new KeyNotFoundException($"Receta con ID {ID} no fue encontrada");
                }
                _Context.Recetas.Remove(receta);
                await _Context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Algo falló al actualizar la receta, {ex}");
            }
        }
    }
}
