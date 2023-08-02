using controle_de_permissoes.Models.Entities.Orm;

namespace controle_de_permissoes.Models.Repositories.Interfaces
{
    public interface UsuarioRepository {
        Usuario Create(Usuario usuario);
        Usuario ReadById(int id);
        List<Usuario> ReadAll();
        Usuario Update(Usuario usuario);
        bool Delete(Usuario usuario);
    }
}
