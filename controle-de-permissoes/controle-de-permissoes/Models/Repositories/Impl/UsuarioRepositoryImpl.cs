using controle_de_permissoes.Models.DB;
using controle_de_permissoes.Models.Entities.Orm;
using controle_de_permissoes.Models.Repositories.Interfaces;

namespace controle_de_permissoes.Models.Repositories.Impl
{
    public class UsuarioRepositoryImpl : UsuarioRepository {
        private readonly BancoContext bancoContext;

        public UsuarioRepositoryImpl(BancoContext bancoContext) {
            this.bancoContext = bancoContext;
        }

        public Usuario Create(Usuario usuario) {
            usuario.Ativo = true;
            bancoContext.tbl_Usuario.Add(usuario);
            bancoContext.SaveChanges();
            return usuario;
        }

        public List<Usuario> ReadAll() {
            return bancoContext.tbl_Usuario.ToList();
        }

        public Usuario ReadById(int id) {
            return bancoContext.tbl_Usuario.FirstOrDefault(x => x.Id == id);
        }

        public Usuario Update(Usuario usuario) {
            Usuario usuarioBanco = ReadById(usuario.Id);

            if (usuarioBanco == null) {
                throw new Exception("Houve um erro ao encontrar o usuário a ser editado");
            }

            usuarioBanco.NomeCompleto = usuario.NomeCompleto;
            usuarioBanco.NomeAmigavel= usuario.NomeAmigavel;
            usuarioBanco.NomeUser = usuario.NomeUser;
            usuarioBanco.SenhaUser = usuario.SenhaUser;
            usuarioBanco.CaixaVirtual = usuario.CaixaVirtual;
            usuarioBanco.Observacao = usuario.Observacao;
            usuarioBanco.Ativo = usuario.Ativo;

            bancoContext.tbl_Usuario.Update(usuarioBanco);
            bancoContext.SaveChanges();
            return usuarioBanco;
        }

        public bool Delete(Usuario usuario) {
            if (usuario == null) {
                throw new Exception("Houve um erro ao encontrar o usuário a ser excluído");
            }

            bancoContext.tbl_Usuario.Remove(usuario);
            bancoContext.SaveChanges();
            return true;
        }
    }
}
