using controle_de_permissoes.Models.Entities.Orm;

namespace controle_de_permissoes.Models.Repositories.Interfaces
{
    public interface SistemaRepository {
        List<Sistema> ReadAll();
    }
}
