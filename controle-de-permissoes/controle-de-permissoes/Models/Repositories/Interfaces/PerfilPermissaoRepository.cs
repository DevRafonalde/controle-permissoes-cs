using controle_de_permissoes.Models.Entities.Orm;

namespace controle_de_permissoes.Models.Repositories.Interfaces
{
    public interface PerfilPermissaoRepository {
        PerfilPermissao Create(PerfilPermissao perfilPermissao);
        PerfilPermissao ReadById(int id);
        List<PerfilPermissao> ReadAll();
        PerfilPermissao Update(PerfilPermissao perfilPermissao);
        bool Delete(PerfilPermissao perfilPermissao);
    }
}
