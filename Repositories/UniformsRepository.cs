using Microsoft.EntityFrameworkCore;
using UniformProjectOmar.Data;
using UniformProjectOmar.Models;
using UniformProjectOmar.Repositories.Interfaces;

namespace UniformProjectOmar.Repositories
{
    public class UniformsRepository : IUniformsRepository
    {
        private readonly AppDbContext _context;

        public UniformsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MovimientoVw>> GetMoves()
        {
            try
            {
                return await _context.MovimientosEmpleado
                                      .OrderByDescending(m => m.Fecha)
                                      .ToListAsync();
            }
            catch (Exception)
            {

                return new List<MovimientoVw>();
            }
            
        }

        public async Task<Movimiento?> GetMovimientoById(int id)
        {
            try
            {
                return await _context.Movimiento.FirstOrDefaultAsync(m => m.Id == id);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<bool?> CreateMovimiento(CrearMovimiento movimiento) 
        {
            try
            {
                await _context.RecordDeliveryAsync(movimiento.IdEmpleado,movimiento.IdArticulo,
                    movimiento.Talla,movimiento.Cantidad);

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool?> UpdateMovimiento(CrearMovimiento movimiento)
        {
            try
            {
                var existingMovimiento = await _context.Movimiento.FirstOrDefaultAsync(m => m.Id == movimiento.Id);

                if(existingMovimiento == null) return null;
                
                existingMovimiento.IdEmpleado = movimiento.IdEmpleado;
                existingMovimiento.IdArticulo = movimiento.IdArticulo;
                existingMovimiento.Talla = movimiento.Talla;

                _context.Update(existingMovimiento);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool?> DeleteMovimiento(int id)
        {
            try
            {
                var existingMovimiento = await _context.Movimiento.FirstOrDefaultAsync(m => m.Id == id);

                if (existingMovimiento == null) return null;

                _context.Movimiento.Remove(existingMovimiento);
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
