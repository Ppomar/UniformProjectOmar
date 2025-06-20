using Microsoft.EntityFrameworkCore;
using UniformProjectOmar.Data;
using UniformProjectOmar.Models;
using UniformProjectOmar.Repositories.Interfaces;

namespace UniformProjectOmar.Repositories
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly AppDbContext _context;

        public EmpleadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Empleado>> GetEmpleados()
        {
            try
            {
                return await _context.Empleado.Include(e => e.Grupo).ToListAsync();
            }
            catch (Exception)
            {

                return new List<Empleado>();
            }
        }

        public async Task<Empleado?> GetEmpleadoById(int id)
        {
            try
            {
                return await _context.Empleado.Include(e => e.Grupo).FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool?> CreateEmpleado(Empleado empleado)
        {
            try
            {
                var existingEmpleado = await _context.Empleado.FirstOrDefaultAsync(ta => ta.NombreEmpleado.ToLower() == empleado.NombreEmpleado.ToLower());

                if (existingEmpleado != null) return null;

                var maxId = await _context.Empleado.MaxAsync(t => (int?)t.Id) ?? 0;
                empleado.Id = maxId + 1;

                await _context.Empleado.AddAsync(empleado);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool?> UpdateEmpleado(Empleado empleado)
        {
            try
            {
                var existingEmpleado = await _context.Empleado.FirstOrDefaultAsync(ta => ta.Id != empleado.Id && ta.NombreEmpleado.ToLower() == empleado.NombreEmpleado.ToLower());

                if (existingEmpleado != null) return null;

                var entity = await _context.Empleado.FirstOrDefaultAsync(ta => ta.Id == empleado.Id);

                if (entity != null)
                {
                    entity.NombreEmpleado = empleado.NombreEmpleado;
                    entity.IdGrupo = empleado.IdGrupo;                    

                    _context.Empleado.Update(entity);
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

        public async Task<bool?> DeleteEmpleado(int id)
        {
            try
            {
                var existingEmpleado = await _context.Empleado.FirstOrDefaultAsync(ta => ta.Id == id);

                if (existingEmpleado == null) return null;

                _context.Empleado.Remove(existingEmpleado);
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
