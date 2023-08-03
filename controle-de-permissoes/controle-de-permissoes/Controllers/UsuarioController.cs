using controle_de_permissoes.Models.Entities.Orm;
using controle_de_permissoes.Models.Entities.View;
using controle_de_permissoes.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace controle_de_permissoes.Controllers {
    public class UsuarioController : Controller {
        private readonly UsuarioRepository usuarioRepository;
        private readonly PerfilRepository perfilRepository;
        private readonly UsuarioPermissaoRepository usuarioPermissaoRepository;

        public UsuarioController(UsuarioRepository usuarioRepository, PerfilRepository perfilRepository, UsuarioPermissaoRepository usuarioPermissaoRepository) {
            this.usuarioRepository = usuarioRepository;
            this.perfilRepository = perfilRepository;
            this.usuarioPermissaoRepository = usuarioPermissaoRepository;
        }

        // Página para listar todos os usuarios existentes
        public IActionResult Index() {
            List<Usuario> usuarios = usuarioRepository.ReadAll();
            return View(usuarios);
        }

        // Página para o cadastro de novos usuarios
        public IActionResult Cadastro() {
            ModeloCadastroUsuarioPerfil modeloCadastroUsuarioPerfil = new ModeloCadastroUsuarioPerfil();
            modeloCadastroUsuarioPerfil.TodosPerfis = perfilRepository.ReadAll();
            return View(modeloCadastroUsuarioPerfil);
        }

        // Página para a edição de usuarios existentes
        public IActionResult Edicao(int id) {
            Usuario usuario = usuarioRepository.ReadById(id);
            List<Perfil> perfis = usuarioPermissaoRepository.ReadByUsuario(usuario).Select(up => up.Perfil).ToList();

            ModeloCadastroUsuarioPerfil modeloCadastroUsuarioPerfil= new ModeloCadastroUsuarioPerfil();
            modeloCadastroUsuarioPerfil.TodosPerfis = perfilRepository.ReadAll();
            modeloCadastroUsuarioPerfil.Usuario = usuario;
            modeloCadastroUsuarioPerfil.PerfisSelecionados = perfis;

            return View(modeloCadastroUsuarioPerfil);
        }

        // Página para a listagem de perfis específica de cada usuario
        public IActionResult ListagemEspecifica(int id) {
            Usuario usuario = usuarioRepository.ReadById(id);
            List<Perfil> perfis = usuarioPermissaoRepository.ReadByUsuario(usuario).Select(up => up.Perfil).ToList();

            ModeloCadastroUsuarioPerfil modeloCadastroUsuarioPerfil = new ModeloCadastroUsuarioPerfil();
            modeloCadastroUsuarioPerfil.Usuario = usuario;
            modeloCadastroUsuarioPerfil.PerfisSelecionados = perfis;

            return View(modeloCadastroUsuarioPerfil);
        }

        // Página para a confirmação da exclusão
        public IActionResult ApagarConfirmacao(int id) {
            Usuario usuario = usuarioRepository.ReadById(id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Cadastro(ModeloCadastroUsuarioPerfil modeloCadastroUsuarioPerfil) {
            try {
                if (ModelState.IsValid) {
                    usuarioPermissaoRepository.Create(modeloCadastroUsuarioPerfil);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("ListagemEspecifica", modeloCadastroUsuarioPerfil.Usuario.Id);
                }
                return View(modeloCadastroUsuarioPerfil);
            } catch (Exception erro) {
                TempData["MensagemErro"] = "Houve um erro no cadastro do usuário, entre em contato com o suporte." + erro.Message;
                return RedirectToAction("Cadastro");
            }
        }

        [HttpPost]
        public IActionResult EditarBanco(ModeloCadastroUsuarioPerfil modeloCadastroUsuarioPerfil) {
            try {
                if (ModelState.IsValid) {
                    usuarioPermissaoRepository.Update(modeloCadastroUsuarioPerfil);
                    TempData["MensagemSucesso"] = "Usuário editado com sucesso!";
                    return RedirectToAction("ListagemEspecifica", modeloCadastroUsuarioPerfil.Usuario.Id);
                }
                return View(modeloCadastroUsuarioPerfil);
            } catch (Exception erro) {
                TempData["MensagemErro"] = "Houve um erro na edição do usuário, entre em contato com o suporte. " + erro.Message;
                return RedirectToAction("Cadastro");
            }
        }

        public IActionResult Apagar(Usuario usuario) {
            try {
                List<UsuarioPermissao> vinculosUsuario = usuarioPermissaoRepository.ReadByUsuario(usuario);

                foreach (UsuarioPermissao usuarioPermissao in vinculosUsuario) {
                    usuarioPermissaoRepository.Delete(usuarioPermissao);
                }

                usuarioRepository.Delete(usuario);
                TempData["MensagemSucesso"] = "Usuário excluído com sucesso!";
                return RedirectToAction("Index");
            } catch (Exception erro) {
                TempData["MensagemErro"] = "Houve um erro na exclusão do usuário, entre em contato com o suporte. " + erro.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
