using controle_de_permissoes.Models.Entities.Orm;
using controle_de_permissoes.Models.Entities.View;

namespace controle_de_permissoes.Models.Repositories.Interfaces
{
    public interface UsuarioPermissaoRepository {
        List<UsuarioPermissao> Create(ModeloCadastroUsuarioPerfil modeloCadastroUsuarioPerfil);
        UsuarioPermissao ReadById(int id);
        List<UsuarioPermissao> ReadByUsuario(Usuario usuario);
        List<UsuarioPermissao> ReadByPerfil(Perfil perfil);
        List<UsuarioPermissao> ReadAll();
        List<UsuarioPermissao> Update(ModeloCadastroUsuarioPerfil modeloCadastroUsuarioPerfil);
        bool Delete(UsuarioPermissao usuarioPermissao);
    }
}
