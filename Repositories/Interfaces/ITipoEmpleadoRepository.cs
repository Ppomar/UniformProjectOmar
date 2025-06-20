using UniformProjectOmar.Models;

namespace UniformProjectOmar.Repositories.Interfaces
{
    public interface ITipoEmpleadoRepository
    {
        public Task<List<TipoEmpleado>> GetTipoEmpleados();
        public Task<TipoEmpleado?> GetTipoEmpleadoById(int id);
        public Task<bool?> CreateTipoEmpleado(TipoEmpleado tipoEmpleado);
        public Task<bool?> UpdateTipoEmpleado(TipoEmpleado tipoEmpleado);
        public Task<bool?> DeleteTipoEmpleado(int id);
    }
}
