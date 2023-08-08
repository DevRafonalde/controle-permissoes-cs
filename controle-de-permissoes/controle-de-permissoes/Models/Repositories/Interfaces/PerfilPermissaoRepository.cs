using controle_de_permissoes.Models.Entities.Orm;
using controle_de_permissoes.Models.Entities.View;

namespace controle_de_permissoes.Models.Repositories.Interfaces
{
    public interface PerfilPermissaoRepository {
        int Create(ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao);
        PerfilPermissao ReadById(int id);
        List<PerfilPermissao> ReadByPerfil(Perfil perfil);
        List<PerfilPermissao> ReadByPermissao(Permissao permissao);
        List<PerfilPermissao> ReadAll();
        int Update(ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao);
        bool Delete(PerfilPermissao perfilPermissao);
    }
}
