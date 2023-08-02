using controle_de_permissoes.Models.DB;
using controle_de_permissoes.Models.Entities.Orm;
using controle_de_permissoes.Models.Entities.View;
using controle_de_permissoes.Models.Repositories.Interfaces;

namespace controle_de_permissoes.Models.Repositories.Impl
{
    public class PerfilPermissaoRepositoryImpl : PerfilPermissaoRepository {
        private readonly BancoContext bancoContext;

        public PerfilPermissaoRepositoryImpl(BancoContext bancoContext) {
            this.bancoContext = bancoContext;
        }

        public Perfil Create(ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao) {
            List<Permissao> permissoes = modeloCadastroPerfilPermissao.PermissoesPerfil;
            foreach (Permissao permissao in permissoes) {
                PerfilPermissao perfilPermissao = new PerfilPermissao();
                perfilPermissao.Perfil = modeloCadastroPerfilPermissao.Perfil;
                perfilPermissao.Perfil.Excluido = false;
                perfilPermissao.DataHora = DateTime.Now;
                perfilPermissao.Excluido = false;
                perfilPermissao.Permissao = permissao;
                bancoContext.tbl_PerfilPermissao.Add(perfilPermissao);
            }

            bancoContext.SaveChanges();
            return modeloCadastroPerfilPermissao.Perfil;
        }

        public List<PerfilPermissao> ReadAll() {
            throw new NotImplementedException();
        }

        public PerfilPermissao ReadById(int id) {
            throw new NotImplementedException();
        }

        public PerfilPermissao Update(PerfilPermissao perfilPermissao) {
            throw new NotImplementedException();
        }

        public bool Delete(PerfilPermissao perfilPermissao) {
            throw new NotImplementedException();
        }
    }
}
