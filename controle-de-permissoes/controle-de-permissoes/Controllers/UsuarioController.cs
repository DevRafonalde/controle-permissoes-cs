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
            List<Usuario> usuarios = usuarioRepository.ReadAll().Where(u => u.NomeAmigavel != null && u.NomeAmigavel != "" && u.Ativo == true).ToList();
            return View(usuarios);
        }

        // Página para o cadastro de novos usuarios
        public IActionResult Cadastrar() {
            return View();
        }

        public IActionResult GetTodosPerfis() {
            List<Perfil> perfis = perfilRepository.ReadAll();
            return Ok(perfis);
        }

        [HttpPost]
        public IActionResult AdicionarPerfil(ModeloCadastroUsuarioPerfil modeloCadastroUsuarioPerfil) {
            if (modeloCadastroUsuarioPerfil.PerfisSelecionados == null) {
                modeloCadastroUsuarioPerfil.setPerfisSelecionados(new List<Perfil>());
            }
            modeloCadastroUsuarioPerfil.addPerfisSelecionados(new Perfil());
            foreach (Perfil perfil in modeloCadastroUsuarioPerfil.getPerfisSelecionados()) {
                Console.WriteLine(perfil.Nome);
            }
            modeloCadastroUsuarioPerfil.TodosPerfis = perfilRepository.ReadAll();

            return View("Cadastrar", modeloCadastroUsuarioPerfil);
        }

        [HttpPost]
        public IActionResult RemoverPerfil(ModeloCadastroUsuarioPerfil modeloCadastroUsuarioPerfil, int? removePerfil) {
            if (removePerfil.HasValue) {
                Console.WriteLine(removePerfil);
                modeloCadastroUsuarioPerfil.PerfisSelecionados.RemoveAt(removePerfil.Value);
            }
            foreach (Perfil perfil in modeloCadastroUsuarioPerfil.getPerfisSelecionados()) {
                Console.WriteLine(perfil.Nome);
            }
            modeloCadastroUsuarioPerfil.TodosPerfis = perfilRepository.ReadAll();

            return View("Cadastrar", modeloCadastroUsuarioPerfil);
        }

        // Página para a edição de usuarios existentes
        public IActionResult Editar(int id) {
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
            Console.WriteLine("ID DA LISTAGEM ESPECIFICA: " + id);
            Usuario usuario = usuarioRepository.ReadById(id);
            List<int?> perfisId = usuarioPermissaoRepository.ReadByUsuario(usuario).Select(up => up.PerfilId).ToList();
            List<Perfil> perfis = new();
            foreach (int perfilId in perfisId) {
                perfis.Add(perfilRepository.ReadById(perfilId));
            }

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
        public IActionResult Cadastrar([FromBody] ModeloCadastroUsuarioPerfil modeloCadastroUsuarioPerfil) {
            try {
                if (ModelState.IsValid) {
                    List<Perfil> perfisSelecionados = new();
                    foreach (int idPerfil in modeloCadastroUsuarioPerfil.PerfisSelecionadosIds) {
                        perfisSelecionados.Add(perfilRepository.ReadById(idPerfil));
                    }
                    modeloCadastroUsuarioPerfil.setPerfisSelecionados(perfisSelecionados);
                    int id = usuarioPermissaoRepository.Create(modeloCadastroUsuarioPerfil);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    //return View("ListagemEspecifica", modeloCadastroUsuarioPerfil);
                    return Ok(id);
                }
                return View(modeloCadastroUsuarioPerfil);
            } catch (Exception erro) {
                TempData["MensagemErro"] = "Houve um erro no cadastro do usuário, entre em contato com o suporte. " + erro.Message;
                return RedirectToAction("Cadastrar");
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
                return RedirectToAction("Cadastrar");
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
