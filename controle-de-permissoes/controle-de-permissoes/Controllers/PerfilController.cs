using controle_de_permissoes.Models.Entities.Orm;
using controle_de_permissoes.Models.Entities.View;
using controle_de_permissoes.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace controle_de_permissoes.Controllers {
    public class PerfilController : Controller {
        private readonly PerfilRepository perfilRepository;
        private readonly PerfilPermissaoRepository perfilPermissaoRepository;
        private readonly UsuarioPermissaoRepository usuarioPermissaoRepository;
        private readonly SistemaRepository sistemaRepository;
        private readonly PermissaoRepository permissaoRepository;

        public PerfilController(PerfilRepository perfilRepository, PerfilPermissaoRepository perfilPermissaoRepository, UsuarioPermissaoRepository usuarioPermissaoRepository, SistemaRepository sistemaRepository, PermissaoRepository permissaoRepository) {
            this.perfilRepository = perfilRepository;
            this.perfilPermissaoRepository = perfilPermissaoRepository;
            this.usuarioPermissaoRepository = usuarioPermissaoRepository;
            this.sistemaRepository = sistemaRepository;
            this.permissaoRepository = permissaoRepository;
        }

        // Página para listar todos os perfis existentes
        public IActionResult Index() {
            List<Perfil> perfis = perfilRepository.ReadAll();
            return View(perfis);
        }

        // Página para o cadastro de novos perfis
        public IActionResult Cadastro() {
            ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao = new ModeloCadastroPerfilPermissao();
            modeloCadastroPerfilPermissao.TodosSistemas = sistemaRepository.ReadAll();
            modeloCadastroPerfilPermissao.TodasPermissoes = permissaoRepository.ReadAll();
            return View(modeloCadastroPerfilPermissao);
        }

        // Página para a edição de perfis existentes
        public IActionResult Edicao(int id) {
            Perfil perfil = perfilRepository.ReadById(id);
            List<Permissao> permissoes = perfilPermissaoRepository.ReadByPerfil(perfil).Select(pp => pp.Permissao).ToList();

            ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao = new ModeloCadastroPerfilPermissao();
            modeloCadastroPerfilPermissao.TodosSistemas = sistemaRepository.ReadAll();
            modeloCadastroPerfilPermissao.Perfil = perfil;
            modeloCadastroPerfilPermissao.PermissoesSelecionadas = permissoes;
            modeloCadastroPerfilPermissao.TodasPermissoes = permissaoRepository.ReadAll();

            return View(modeloCadastroPerfilPermissao);
        }

        // Página para a listagem de permissões específica de cada perfil
        public IActionResult ListagemEspecifica(int id) {
            Perfil perfil = perfilRepository.ReadById(id);
            List<Permissao> permissoes = perfilPermissaoRepository.ReadByPerfil(perfil).Select(pp => pp.Permissao).ToList();

            ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao = new ModeloCadastroPerfilPermissao();
            modeloCadastroPerfilPermissao.Perfil = perfil;
            modeloCadastroPerfilPermissao.PermissoesSelecionadas = permissoes;

            return View(modeloCadastroPerfilPermissao);
        }

        // Página para a confirmação da exclusão
        public IActionResult ApagarConfirmacao(int id) {
            Perfil perfil = perfilRepository.ReadById(id);
            return View(perfil);
        }

        [HttpPost]
        public IActionResult Cadastro(ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao) {
            try {
                if (ModelState.IsValid) {
                    perfilPermissaoRepository.Create(modeloCadastroPerfilPermissao);
                    TempData["MensagemSucesso"] = "Perfil cadastrado com sucesso!";
                    return RedirectToAction("ListagemEspecifica", modeloCadastroPerfilPermissao.Perfil.Id);
                }
                return View(modeloCadastroPerfilPermissao);
            } catch (Exception erro) {
                TempData["MensagemErro"] = "Houve um erro no cadastro do perfil, entre em contato com o suporte." + erro.Message;
                return RedirectToAction("Cadastro");
            }
        }

        [HttpPost]
        public IActionResult EditarBanco(ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao) {
            try {
                if (ModelState.IsValid) {
                    perfilPermissaoRepository.Update(modeloCadastroPerfilPermissao);
                    TempData["MensagemSucesso"] = "Perfil editado com sucesso!";
                    return RedirectToAction("ListagemEspecifica", modeloCadastroPerfilPermissao.Perfil.Id);
                }
                return View(modeloCadastroPerfilPermissao);
            } catch (Exception erro) {
                TempData["MensagemErro"] = "Houve um erro na edição do perfil, entre em contato com o suporte. " + erro.Message;
                return RedirectToAction("Cadastro");
            }
        }

        public IActionResult Apagar(Perfil perfil) {
            try {
                List<PerfilPermissao> perfisPermissao = perfilPermissaoRepository.ReadByPerfil(perfil);
                List<UsuarioPermissao> usuariosPerfil = usuarioPermissaoRepository.ReadByPerfil(perfil);

                foreach (PerfilPermissao perfilPermissao in perfisPermissao) {
                    perfilPermissaoRepository.Delete(perfilPermissao);
                }

                foreach (UsuarioPermissao usuarioPermissao in usuariosPerfil) {
                    usuarioPermissaoRepository.Delete(usuarioPermissao);
                }

                perfilRepository.Delete(perfil);
                TempData["MensagemSucesso"] = "Perfil excluído com sucesso!";
                return RedirectToAction("Index");
            } catch (Exception erro) {
                TempData["MensagemErro"] = "Houve um erro na exclusão do perfil, entre em contato com o suporte. " + erro.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
