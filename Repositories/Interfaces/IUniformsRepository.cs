using UniformProjectOmar.Models;

namespace UniformProjectOmar.Repositories.Interfaces
{
    public interface IUniformsRepository
    {
        public Task<List<Movimiento>> GetMovesAsync();

        public Task<bool> CreateMovement(CrearMovimiento movimiento);
    }
}
