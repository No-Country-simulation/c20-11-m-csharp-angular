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
            try
            {
                return await _Context.Recetas
                .Include(receta => receta.Usuario)
                .Include(receta => receta.Categorias)
                .Include(receta => receta.Reviews)
                .Select(receta => _Mapper.Map<RecetaDto>(receta))
                .Where(receta => !receta.IsDeleted)
                .ToListAsync();
            }
            catch
            {
                throw new ApplicationException("Algo falló");
            }

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
