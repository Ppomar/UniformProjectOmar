using UniformProjectOmar.Models;

namespace UniformProjectOmar.Repositories.Interfaces
{
    public interface ITipoArticuloRepository
    {
        public Task<TipoArticulo?> GetTipoArticuloById(int id);
        public Task<List<TipoArticulo>> GetTipoArticulos();
        public Task<bool?> CreateTipoArticulo(TipoArticulo tipoArticulo);
        public Task<bool?> DeleteTipoArticulo(int id);
        public Task<bool?> UpdateTipoArticulo(TipoArticulo tipoArticulo);
    }
}
