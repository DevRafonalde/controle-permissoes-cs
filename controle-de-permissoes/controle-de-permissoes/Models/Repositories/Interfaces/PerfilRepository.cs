using controle_de_permissoes.Models.Entities.Orm;

namespace controle_de_permissoes.Models.Repositories.Interfaces
{
    public interface PerfilRepository {
        Perfil Create(Perfil permissao);
        Perfil ReadById(int id);
        List<Perfil> ReadAll();
        Perfil Update(Perfil permissao);
        bool Delete(Perfil permissao);
    }
}
