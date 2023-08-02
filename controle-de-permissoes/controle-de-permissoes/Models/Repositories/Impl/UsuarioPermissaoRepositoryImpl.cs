using controle_de_permissoes.Models.DB;
using controle_de_permissoes.Models.Entities.Orm;
using controle_de_permissoes.Models.Entities.View;
using controle_de_permissoes.Models.Repositories.Interfaces;

namespace controle_de_permissoes.Models.Repositories.Impl
{
    public class UsuarioPermissaoRepositoryImpl : UsuarioPermissaoRepository {

        private readonly BancoContext bancoContext;
        private readonly UsuarioRepository usuarioRepository;

        public UsuarioPermissaoRepositoryImpl(BancoContext bancoContext, UsuarioRepository usuarioRepository) {
            this.bancoContext = bancoContext;
            this.usuarioRepository = usuarioRepository;
        }

        public Usuario Create(ModeloCadastroUsuarioPerfil modeloCadastroUsuarioPerfil) {
            List<Perfil> perfis = modeloCadastroUsuarioPerfil.PerfisUsuario;
            foreach (Perfil perfil in perfis) {
                UsuarioPermissao usuarioPermissao = new UsuarioPermissao();
                usuarioPermissao.Usuario = modeloCadastroUsuarioPerfil.Usuario;
                usuarioPermissao.Usuario.Ativo = true;
                usuarioPermissao.Negacao = false;
                usuarioPermissao.DataHora = DateTime.Now;
                usuarioPermissao.Excluido = false;
                usuarioPermissao.Perfil = perfil;
                bancoContext.tbl_UsuarioPermissao.Add(usuarioPermissao);
            }
            
            bancoContext.SaveChanges();
            return modeloCadastroUsuarioPerfil.Usuario;
        }

        public List<UsuarioPermissao> ReadAll() {
            return bancoContext.tbl_UsuarioPermissao.ToList();
        }

        public UsuarioPermissao ReadById(int id) {
            return bancoContext.tbl_UsuarioPermissao.FirstOrDefault(up => up.Id == id);
        }

        public List<UsuarioPermissao> ReadByUsuario(Usuario usuario) {
            return bancoContext.tbl_UsuarioPermissao.Where(up => up.UsuarioId == usuario.Id).ToList();
        }

        public Usuario Update(ModeloCadastroUsuarioPerfil modeloCadastroUsuarioPerfil) {
            Usuario usuarioMexido = usuarioRepository.ReadById(modeloCadastroUsuarioPerfil.Usuario.Id);

            if (usuarioMexido == null) {
                throw new Exception("Houve um erro ao encontrar o usuário a ser editado");
            }

            usuarioMexido.NomeCompleto = modeloCadastroUsuarioPerfil.Usuario.NomeCompleto;
            usuarioMexido.NomeAmigavel = modeloCadastroUsuarioPerfil.Usuario.NomeAmigavel;
            usuarioMexido.NomeUser = modeloCadastroUsuarioPerfil.Usuario.NomeUser;
            usuarioMexido.SenhaUser = modeloCadastroUsuarioPerfil.Usuario.SenhaUser;
            usuarioMexido.CaixaVirtual = modeloCadastroUsuarioPerfil.Usuario.CaixaVirtual;
            usuarioMexido.Observacao = modeloCadastroUsuarioPerfil.Usuario.Observacao;
            usuarioMexido.Ativo = modeloCadastroUsuarioPerfil.Usuario.Ativo;
            usuarioRepository.Update(usuarioMexido);

            List<UsuarioPermissao> registrosExistentes = ReadByUsuario(usuarioMexido);
            foreach (UsuarioPermissao usuarioPermissao in registrosExistentes) {
                Delete(usuarioPermissao);
            }

            List<Perfil> perfis = modeloCadastroUsuarioPerfil.PerfisUsuario;
            foreach (Perfil perfil in perfis) {
                UsuarioPermissao usuarioPermissao = new UsuarioPermissao();
                usuarioPermissao.Usuario = modeloCadastroUsuarioPerfil.Usuario;
                usuarioPermissao.Negacao = false;
                usuarioPermissao.DataHora = DateTime.Now;
                usuarioPermissao.Excluido = false;
                usuarioPermissao.Perfil = perfil;
                bancoContext.tbl_UsuarioPermissao.Add(usuarioPermissao);
            }

            bancoContext.SaveChanges();
            return modeloCadastroUsuarioPerfil.Usuario;

        }

        public bool Delete(UsuarioPermissao usuarioPermissao) {
            if (usuarioPermissao == null) {
                throw new Exception("Houve um erro ao encontrar o usuário a ser excluído");
            }

            bancoContext.tbl_UsuarioPermissao.Remove(usuarioPermissao);
            bancoContext.SaveChanges();
            return true;
        }
    }
}
