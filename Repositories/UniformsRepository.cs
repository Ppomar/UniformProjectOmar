using Microsoft.EntityFrameworkCore;
using UniformProjectOmar.Data;
using UniformProjectOmar.Models;
using UniformProjectOmar.Repositories.Interfaces;

namespace UniformProjectOmar.Repositories
{
    public class UniformsRepository : IUniformsRepository
    {
        private readonly AppDbContext _appDbContext;

        public UniformsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Movimiento>> GetMovesAsync()
        {
            try
            {
                return await _appDbContext.MovimientosEmpleado
                                      .OrderByDescending(m => m.Fecha)
                                      .ToListAsync();
            }
            catch (Exception)
            {

                return new List<Movimiento>();
            }
            
        }

        public async Task<bool> CreateMovement(CrearMovimiento movimiento) 
        {
            try
            {
                await _appDbContext.RecordDeliveryAsync(movimiento.IdEmpleado,movimiento.IdArticulo,
                    movimiento.Talla,movimiento.Cantidad);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }        
    }
}
