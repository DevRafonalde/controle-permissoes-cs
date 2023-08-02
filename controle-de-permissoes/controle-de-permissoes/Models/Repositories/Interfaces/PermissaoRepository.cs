using controle_de_permissoes.Models.Entities.Orm;

namespace controle_de_permissoes.Models.Repositories.Interfaces
{
    public interface PermissaoRepository {
        Permissao Create(Permissao permissao);
        Permissao ReadById(int id);
        List<Permissao> ReadAll();
        Permissao Update(Permissao permissao);
        bool Delete(Permissao permissao);
    }
}
