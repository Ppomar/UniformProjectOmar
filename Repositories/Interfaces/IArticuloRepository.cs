using UniformProjectOmar.Models;

namespace UniformProjectOmar.Repositories.Interfaces
{
    public interface IArticuloRepository
    {
        public Task<Articulo?> GetArticuloById(int id);
        public Task<List<Articulo>> GetArticulos();
        public Task<bool?> CreateArticulo(Articulo Articulo);
        public Task<bool?> UpdateArticulo(Articulo Articulo);
        public Task<bool?> DeleteArticulo(int id);
    }
}
