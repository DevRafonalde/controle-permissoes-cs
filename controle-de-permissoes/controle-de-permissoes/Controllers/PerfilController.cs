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

        // Página para listar todos os permissoes existentes
        public IActionResult Index() {
            List<Perfil> perfis = perfilRepository.ReadAll();
            return View(perfis);
        }

        // Página para o cadastro de novos permissoes
        public IActionResult Cadastrar() {
            Console.WriteLine("teste inicio cadastro get");
            ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao = new ModeloCadastroPerfilPermissao();
            modeloCadastroPerfilPermissao.TodosSistemas = sistemaRepository.ReadAll();
            Console.WriteLine("teste fim cadastro get");
            return View(modeloCadastroPerfilPermissao);
        }

        public IActionResult GetTodasPermissoes() {
            List<Permissao> permissoes = permissaoRepository.ReadAll();
            return Ok(permissoes);
        }

        // Página para a edição de permissoes existentes
        public IActionResult Editar(int id) {
            Perfil perfil = perfilRepository.ReadById(id);
            List<int> permissoesId = perfilPermissaoRepository.ReadByPerfil(perfil).Select(up => up.PermissaoId).ToList();
            List<Permissao> permissoes = new();
            foreach (int permissaoId in permissoesId) {
                Permissao permissao = permissaoRepository.ReadById(permissaoId);
                permissao.Sistema = sistemaRepository.ReadById(permissao.GetSistemaId());
                permissoes.Add(permissao);
            }

            ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao = new ModeloCadastroPerfilPermissao();
            modeloCadastroPerfilPermissao.TodosSistemas = sistemaRepository.ReadAll();
            modeloCadastroPerfilPermissao.Perfil = perfil;
            modeloCadastroPerfilPermissao.PermissoesSelecionadas = permissoes;
            modeloCadastroPerfilPermissao.TodasPermissoes = permissaoRepository.ReadAll();

            return View(modeloCadastroPerfilPermissao);
        }

        // Página para a listagem de permissões específica de cada permissao
        public IActionResult ListagemEspecifica(int id) {
            Perfil perfil = perfilRepository.ReadById(id);
            List<int> permissoesId = perfilPermissaoRepository.ReadByPerfil(perfil).Select(up => up.PermissaoId).ToList();

            List<Permissao> permissoes = new();
            foreach (int permissaoId in permissoesId) {
                Permissao permissao = permissaoRepository.ReadById(permissaoId);
                permissao.Sistema = sistemaRepository.ReadById(permissao.GetSistemaId());
                permissoes.Add(permissao);
            }

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
        public IActionResult Cadastrar([FromBody] ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao) {
            try {
                List<Permissao> permissoesSelecionadas = new();
                foreach (int idPerfil in modeloCadastroPerfilPermissao.PermissoesSelecionadasIds) {
                    permissoesSelecionadas.Add(permissaoRepository.ReadById(idPerfil));
                }
                modeloCadastroPerfilPermissao.setPermissoesSelecionadas(permissoesSelecionadas);
                int id = perfilPermissaoRepository.Create(modeloCadastroPerfilPermissao);
                TempData["MensagemSucesso"] = "Perfil cadastrado com sucesso!";
                return Ok(id);
            } catch (Exception erro) {
                TempData["MensagemErro"] = "Houve um erro no cadastro do permissao, entre em contato com o suporte." + erro.Message;
                return RedirectToAction("Cadastrar");
            }
        }

        [HttpPost]
        public IActionResult EditarBanco([FromBody] ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao) {
            try {
                List<Permissao> permissoesSelecionadas = new();
                foreach (int idPerfil in modeloCadastroPerfilPermissao.PermissoesSelecionadasIds) {
                    permissoesSelecionadas.Add(permissaoRepository.ReadById(idPerfil));
                }
                modeloCadastroPerfilPermissao.setPermissoesSelecionadas(permissoesSelecionadas);
                int id = perfilPermissaoRepository.Update(modeloCadastroPerfilPermissao);
                TempData["MensagemSucesso"] = "Perfil editado com sucesso!";
                return Ok(id);
            } catch (Exception erro) {
                TempData["MensagemErro"] = "Houve um erro na edição do permissao, entre em contato com o suporte. " + erro.Message;
                return RedirectToAction("Cadastrar");
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
                TempData["MensagemErro"] = "Houve um erro na exclusão do permissao, entre em contato com o suporte. " + erro.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
