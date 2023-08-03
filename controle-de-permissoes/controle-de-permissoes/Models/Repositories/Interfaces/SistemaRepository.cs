using controle_de_permissoes.Models.Entities.Orm;

namespace controle_de_permissoes.Models.Repositories.Interfaces
{
    public interface SistemaRepository {
        public List<Sistema> ReadAll();
        public Sistema ReadById(int id);
    }
}
