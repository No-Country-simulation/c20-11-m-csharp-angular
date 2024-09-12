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
        public async Task<List<RecetaDto>> GetAllRecetas()
        {
            return await _Context.Recetas
                .Include(r => r.Usuario)
                .Include(r => r.RecetaCategorias).ThenInclude(rc => rc.Categoria)
                .Include(r => r.Reviews).ThenInclude(review => review.Usuario)
                .Where(r => !r.IsDeleted)
                .Select(r => new RecetaDto
                {
                    RecetaID = r.RecetaID,
                    Nombre = r.Nombre ?? "Default",
                    Descripcion = r.Descripcion ?? "Default",
                    ImageUrl = r.ImageUrl ?? "Default",
                    Usuario = new UsuarioPublicDto
                    {
                        UsuarioID = r.Usuario.UsuarioID,
                        Nombre = r.Usuario.Nombre ?? "Default"
                    },
                    Reviews = r.Reviews.Select(review => new ReviewDto
                    {
                        ReviewID = review.ReviewID,
                        Comentario = review.Comentario ?? "No Comment",
                        Calificacion = review.Calificacion,
                        Usuario = new UsuarioPublicDto
                        {
                            UsuarioID = review.Usuario.UsuarioID,
                            Nombre = review.Usuario.Nombre ?? "Default"
                        }
                    }).ToList(),  // Convert the IEnumerable<ReviewDto> to List<ReviewDto>
                    Categorias = r.RecetaCategorias.Select((rc) => new CategoriaDto
                    {
                        CategoriaID = rc.Categoria.CategoriaID,
                        Nombre = rc.Categoria.Nombre ?? "Default"
                    }).ToList()  // Convert the IEnumerable<CategoriaDto> to List<CategoriaDto>
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
        public async Task<Receta> CreateReceta(Receta receta, List<string> list_c, int userId)
        {
            try
            {
                Usuario userE = _Context.Usuarios.FirstOrDefault(u => u.UsuarioID == userId);

                Receta newReceta = new Receta
                {
                    Nombre = receta.Nombre,
                    Descripcion = receta.Descripcion,
                    ImageUrl = receta.ImageUrl
                };

                if (userE != null)
                {

                    foreach (var categoria in list_c)
                    {
                        Categoria categoriaE = _Context.Categorias.FirstOrDefault(c => c.Nombre == categoria.ToLower());

                        if (categoriaE == null)
                        {
                            throw new Exception($"{categoria} no existe");
                        }
                        newReceta.Categorias.Add(categoriaE);

                    }

                    newReceta.Usuario = userE;

                }

                _Context.Recetas.Add(newReceta);
                await _Context.SaveChangesAsync();
                Console.WriteLine("RECETA CREADA!");
                return newReceta;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Algo falló al crear la receta", ex);
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
