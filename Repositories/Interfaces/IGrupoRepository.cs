using UniformProjectOmar.Models;

namespace UniformProjectOmar.Repositories.Interfaces
{
    public interface IGrupoRepository
    {
        public Task<List<Grupo>> GetGrupos();
        public Task<Grupo?> GetGrupoById(string id);
        public Task<bool?> CreateGrupo(Grupo grupo);
        public Task<bool?> DeleteGrupo(string id);
        public Task<bool?> UpdateGrupo(Grupo grupo);
    }
}
