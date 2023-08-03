using controle_de_permissoes.Models.Entities.Orm;
using controle_de_permissoes.Models.Entities.View;

namespace controle_de_permissoes.Models.Repositories.Interfaces
{
    public interface PerfilPermissaoRepository {
        List<PerfilPermissao> Create(ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao);
        PerfilPermissao ReadById(int id);
        List<PerfilPermissao> ReadByPerfil(Perfil perfil);
        List<PerfilPermissao> ReadAll();
        List<PerfilPermissao> Update(ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao);
        bool Delete(PerfilPermissao perfilPermissao);
    }
}
