using Microsoft.EntityFrameworkCore;
using UniformProjectOmar.Data;
using UniformProjectOmar.Models;
using UniformProjectOmar.Repositories.Interfaces;

namespace UniformProjectOmar.Repositories
{
    public class GrupoRepository : IGrupoRepository
    {
        private readonly AppDbContext _context;

        public GrupoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Grupo>> GetGrupos()
        {
            try
            {
                return await _context.Grupo.ToListAsync();
            }
            catch (Exception)
            {

                return new List<Grupo>();
            }
        }

        public async Task<Grupo?> GetGrupoById(string id)
        {
            try
            {
                return await _context.Grupo.FirstOrDefaultAsync(a => a.Id == id);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool?> CreateGrupo(Grupo grupo)
        {
            try
            {
                var existingGrupo = await _context.Grupo.FirstOrDefaultAsync(g => g.Id.ToLower() == grupo.Id.ToLower() || g.Grupo1.ToLower() == grupo.Grupo1.ToLower());

                if (existingGrupo != null) return null;               

                await _context.Grupo.AddAsync(grupo);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool?> UpdateGrupo(Grupo grupo)
        {
            try
            {
                var existingGrupo = await _context.Grupo.FirstOrDefaultAsync(g => g.Id != grupo.Id && g.Grupo1.ToLower() == grupo.Grupo1.ToLower());

                if (existingGrupo != null) return null;

                var entity = await _context.Grupo.FirstOrDefaultAsync(ta => ta.Id == grupo.Id);

                if (entity != null)
                {
                    
                    entity.Grupo1 = grupo.Grupo1;
                    entity.IdTipoEmpleado = grupo.IdTipoEmpleado;

                    _context.Grupo.Update(entity);
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

        public async Task<bool?> DeleteGrupo(string id)
        {
            try
            {
                var existingGrupo = await _context.Grupo.FirstOrDefaultAsync(g => g.Id.ToLower() == id.ToLower());

                if (existingGrupo == null) return null;

                _context.Grupo.Remove(existingGrupo);
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
