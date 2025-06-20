using Microsoft.EntityFrameworkCore;
using UniformProjectOmar.Data;
using UniformProjectOmar.Models;
using UniformProjectOmar.Repositories.Interfaces;

namespace UniformProjectOmar.Repositories
{
    public class TipoEmpleadoRepository : ITipoEmpleadoRepository
    {
        private readonly AppDbContext _context;

        public TipoEmpleadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TipoEmpleado>> GetTipoEmpleados()
        {
            try
            {
                return await _context.TipoEmpleado.ToListAsync();
            }
            catch (Exception)
            {

                return new List<TipoEmpleado>();
            }
        }

        public async Task<TipoEmpleado?> GetTipoEmpleadoById(int id)
        {
            try
            {
                return await _context.TipoEmpleado.FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool?> CreateTipoEmpleado(TipoEmpleado tipoEmpleado)
        {
            try
            {
                var existingTipoEmpleado = await _context.Empleado.FirstOrDefaultAsync(te => te.Id == tipoEmpleado.Id);

                if (existingTipoEmpleado != null) return null;

                var maxId = await _context.Empleado.MaxAsync(t => (int?)t.Id) ?? 0;
                tipoEmpleado.Id = maxId + 1;

                await _context.TipoEmpleado.AddAsync(tipoEmpleado);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool?> UpdateTipoEmpleado(TipoEmpleado tipoEmpleado)
        {
            try
            {
                var existingTipoEmpleado = await _context.TipoEmpleado.FirstOrDefaultAsync(te => te.Id != tipoEmpleado.Id && te.Tipo.ToLower() == tipoEmpleado.Tipo.ToLower());

                if (existingTipoEmpleado != null) return null;

                var entity = await _context.TipoEmpleado.FirstOrDefaultAsync(te => te.Id == tipoEmpleado.Id);

                if (entity != null)
                {
                    entity.Tipo = tipoEmpleado.Tipo;                    

                    _context.TipoEmpleado.Update(entity);
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

        public async Task<bool?> DeleteTipoEmpleado(int id)
        {
            try
            {
                var existingEmpleado = await _context.TipoEmpleado.FirstOrDefaultAsync(te => te.Id == id);

                if (existingEmpleado == null) return null;

                _context.TipoEmpleado.Remove(existingEmpleado);
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
