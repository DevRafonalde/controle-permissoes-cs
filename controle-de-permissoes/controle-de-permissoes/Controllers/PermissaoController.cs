using controle_de_permissoes.Models.Entities.Orm;
using controle_de_permissoes.Models.Entities.View;
using controle_de_permissoes.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace controle_de_permissoes.Controllers {
    public class PermissaoController : Controller {
        private readonly PermissaoRepository permissaoRepository;
        private readonly PerfilPermissaoRepository perfilPermissaoRepository;
        private readonly SistemaRepository sistemaRepository;
        private readonly List<Sistema> todosSistemas;

        public PermissaoController(PermissaoRepository permissaoRepository, PerfilPermissaoRepository perfilPermissaoRepository, SistemaRepository sistemaRepository) {
            this.permissaoRepository = permissaoRepository;
            this.perfilPermissaoRepository = perfilPermissaoRepository;
            this.sistemaRepository = sistemaRepository;
        }

        // Página para listar todos as permissoes existentes
        public IActionResult Index() {
            List<Permissao> permissoes = permissaoRepository.ReadAll();

            foreach (Permissao permissao in permissoes) {
                Sistema sistema = sistemaRepository.ReadById(permissao.GetSistemaId());
                permissao.SetSistema(sistema);
            }

            return View(permissoes);
        }

        // Página para o cadastro de novas permissoes
        public IActionResult Cadastrar() {
            ModeloCadastroPermissao modeloCadastroPermissao = new ModeloCadastroPermissao();
            modeloCadastroPermissao.TodosSistemas = sistemaRepository.ReadAll();
            modeloCadastroPermissao.TodasPermissoes = permissaoRepository.ReadAll();
            return View(modeloCadastroPermissao);
        }

        // Página para a edição de permissoes existentes
        public IActionResult Editar(int id) {
            ModeloCadastroPermissao modeloCadastroPermissao = new ModeloCadastroPermissao();
            modeloCadastroPermissao.TodosSistemas = sistemaRepository.ReadAll();
            modeloCadastroPermissao.TodasPermissoes = permissaoRepository.ReadAll();
            modeloCadastroPermissao.Permissao = permissaoRepository.ReadById(id);
            return View(modeloCadastroPermissao);
        }

        // Página para a confirmação de exclusão
        public IActionResult ApagarConfirmacao(int id) {
            Permissao permissao = permissaoRepository.ReadById(id);
            return View(permissao);
        }

        [HttpPost]
        public IActionResult Cadastrar(Permissao permissao) {
            try {
                if (ModelState.IsValid) {
                    permissaoRepository.Create(permissao);
                    TempData["MensagemSucesso"] = "Permissão cadastrada com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(permissao);
            } catch (Exception erro) {
                TempData["MensagemErro"] = "Houve um erro no cadastro da permissao, entre em contato com o suporte." + erro.Message;
                return RedirectToAction("Cadastrar");
            }
        }

        [HttpPost]
        public IActionResult EditarBanco(Permissao permissao) {
            //Permissao permissao = modeloCadastroPermissao.Permissao;
            try {
                if (ModelState.IsValid) {
                    permissaoRepository.Update(permissao);
                    TempData["MensagemSucesso"] = "Permissão editada com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(permissao);
            } catch (Exception erro) {
                TempData["MensagemErro"] = "Houve um erro na edição da permissao, entre em contato com o suporte. " + erro.Message;
                return RedirectToAction("Cadastrar");
            }
        }

        public IActionResult Apagar(Permissao permissao) {
            try {
                List<PerfilPermissao> usosPermissao = perfilPermissaoRepository.ReadByPermissao(permissao);

                foreach (PerfilPermissao perfilPermissao in usosPermissao) {
                    perfilPermissaoRepository.Delete(perfilPermissao);
                }

                permissaoRepository.Delete(permissao);
                TempData["MensagemSucesso"] = "Permissão excluída com sucesso!";
                return RedirectToAction("Index");
            } catch (Exception erro) {
                TempData["MensagemErro"] = "Houve um erro na exclusão da permissao, entre em contato com o suporte. " + erro.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
