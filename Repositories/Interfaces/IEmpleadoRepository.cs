using UniformProjectOmar.Models;

namespace UniformProjectOmar.Repositories.Interfaces
{
    public interface IEmpleadoRepository
    {
        public Task<Empleado?> GetEmpleadoById(int id);
        public Task<List<Empleado>> GetEmpleados();
        public Task<bool?> CreateEmpleado(Empleado empleado);
        public Task<bool?> UpdateEmpleado(Empleado empleado);
        public Task<bool?> DeleteEmpleado(int id);
    }
}
