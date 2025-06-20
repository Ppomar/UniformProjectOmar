using UniformProjectOmar.Models;

namespace UniformProjectOmar.Repositories.Interfaces
{
    public interface IUniformsRepository
    {
        public Task<List<MovimientoVw>> GetMoves();

        public Task<bool?> CreateMovimiento(CrearMovimiento movimiento);

        public Task<Movimiento?> GetMovimientoById(int id);

        public Task<bool?> UpdateMovimiento(CrearMovimiento movimiento);

        public Task<bool?> DeleteMovimiento(int id);
    }
}
