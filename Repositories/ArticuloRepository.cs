using Microsoft.EntityFrameworkCore;
using UniformProjectOmar.Data;
using UniformProjectOmar.Models;
using UniformProjectOmar.Repositories.Interfaces;

namespace UniformProjectOmar.Repositories
{
    public class ArticuloRepository : IArticuloRepository
    {
        private readonly AppDbContext _context;

        public ArticuloRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Articulo>> GetArticulos()
        {
            try
            {
                return await _context.Articulo.ToListAsync();
            }
            catch (Exception)
            {

                return new List<Articulo>();
            }
        }

        public async Task<Articulo?> GetArticuloById(int id)
        {
            try
            {
                return await _context.Articulo.FirstOrDefaultAsync(a => a.Id == id);                                
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool?> CreateArticulo(Articulo articulo)
        {
            try
            {
                var existingArticulo = await _context.Articulo.FirstOrDefaultAsync(ta => ta.Descripcion.ToLower() == articulo.Descripcion.ToLower());

                if (existingArticulo != null) return null;

                var maxId = await _context.Articulo.MaxAsync(t => (int?)t.Id) ?? 0;
                articulo.Id = maxId + 1;

                await _context.Articulo.AddAsync(articulo);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool?> UpdateArticulo(Articulo articulo)
        {
            try
            {
                var existingArticulo = await _context.Articulo.FirstOrDefaultAsync(ta => ta.Id != articulo.Id && ta.Descripcion.ToLower() == articulo.Descripcion.ToLower());

                if (existingArticulo != null) return null;

                var entity = await _context.Articulo.FirstOrDefaultAsync(ta => ta.Id == articulo.Id);

                if (entity != null)
                {
                    entity.Descripcion = articulo.Descripcion;
                    entity.IdTipoArticulo = articulo.IdTipoArticulo;
                    entity.UnidadTalla = articulo.UnidadTalla;                    

                    _context.Articulo.Update(entity);
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

        public async Task<bool?> DeleteArticulo(int id)
        {
            try
            {
                var existingArticulo = await _context.Articulo.FirstOrDefaultAsync(ta => ta.Id == id);

                if (existingArticulo == null) return null;

                _context.Articulo.Remove(existingArticulo);
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
