using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tastys.BLL.Services.Receta;
using Tastys.Domain;

namespace Tastys.BLL.Services.Receta.RecetaCRUD
{
    public class RecetaCRUD:IRecetaService
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
                var receta = await _Context.Recetas.FindAsync(ID);
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
                _Mapper.Map(recetaDto,receta);
                await _Context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw new ApplicationException($"Algo falló al actualizar la receta, {ex}");
            }
        }
        public async Task<RecetaDto> CreateReceta(RecetaDto receta)
        {
            try {
                var mappedReceta = _Mapper.Map<Tastys.Domain.Receta>(receta);
                _Context.Recetas.Add(mappedReceta);
                await _Context.SaveChangesAsync();
                var createdRecetaDto = _Mapper.Map<RecetaDto>(receta);
                return createdRecetaDto;
            } 
            catch(Exception ex)
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
                _Mapper.Map<RecetaDto>(receta);
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
