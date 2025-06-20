using Microsoft.EntityFrameworkCore;
using UniformProjectOmar.Data;
using UniformProjectOmar.Models;
using UniformProjectOmar.Repositories.Interfaces;

namespace UniformProjectOmar.Repositories
{
    public class TipoArticuloRepository : ITipoArticuloRepository
    {
        private readonly AppDbContext _context;

        public TipoArticuloRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TipoArticulo>> GetTipoArticulos() 
        {
            try
            {
                return await _context.TipoArticulo.ToListAsync();
            }
            catch (Exception)
            {

                return new List<TipoArticulo>();
            }             
        }

        public async Task<TipoArticulo?> GetTipoArticuloById(int id)
        {
            try
            {
                return await _context.TipoArticulo.FirstOrDefaultAsync(ta => ta.Id == id);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool?> CreateTipoArticulo(TipoArticulo tipoArticulo) 
        {
            try
            {
                var existingTipoArticulo = await _context.TipoArticulo.FirstOrDefaultAsync(ta => ta.Descripcion.ToLower() == tipoArticulo.Descripcion.ToLower());

                if (existingTipoArticulo != null) return null;

                var maxId = await _context.TipoArticulo.MaxAsync(t => (int?)t.Id) ?? 0;
                tipoArticulo.Id = maxId + 1;

                await _context.TipoArticulo.AddAsync(tipoArticulo);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool?> UpdateTipoArticulo(TipoArticulo tipoArticulo)
        {
            try
            {
                var existingTipoArticulo = await _context.TipoArticulo.FirstOrDefaultAsync(ta => ta.Id != tipoArticulo.Id && ta.Descripcion.ToLower() == tipoArticulo.Descripcion.ToLower());

                if (existingTipoArticulo != null) return null;

                var entity = await _context.TipoArticulo.FirstOrDefaultAsync(ta => ta.Id == tipoArticulo.Id);

                if (entity != null)
                {
                    entity.Descripcion = tipoArticulo.Descripcion;
                    entity.Aplicacion = tipoArticulo.Aplicacion;

                    _context.TipoArticulo.Update(entity);
                    await _context.SaveChangesAsync();

                    return true;
                }
                else 
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool?> DeleteTipoArticulo(int id)
        {
            try
            {
                var existingTipoArticulo = await _context.TipoArticulo.FirstOrDefaultAsync(ta => ta.Id == id);

                if (existingTipoArticulo == null) return null;

                _context.TipoArticulo.Remove(existingTipoArticulo);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
