using controle_de_permissoes.Models.DB;
using controle_de_permissoes.Models.Entities.Orm;
using controle_de_permissoes.Models.Entities.View;
using controle_de_permissoes.Models.Repositories.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace controle_de_permissoes.Models.Repositories.Impl
{
    public class PerfilPermissaoRepositoryImpl : PerfilPermissaoRepository {
        private readonly BancoContext bancoContext;
        private readonly PerfilRepository perfilRepository;

        public PerfilPermissaoRepositoryImpl(BancoContext bancoContext, PerfilRepository perfilRepository) {
            this.bancoContext = bancoContext;
            this.perfilRepository = perfilRepository;
        }

        public int Create(ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao) {
            Perfil perfilNovo = modeloCadastroPerfilPermissao.Perfil;
            perfilNovo.Excluido = false;
            perfilNovo = perfilRepository.Create(perfilNovo);
            int idPerfil = perfilNovo.Id;

            List<Permissao> permissoes = modeloCadastroPerfilPermissao.PermissoesSelecionadas;
            List <PerfilPermissao> registrosEfetuados = new();
            foreach (Permissao permissao in permissoes) {
                PerfilPermissao perfilPermissao = new PerfilPermissao();
                idPerfil = perfilNovo.Id;
                perfilPermissao.PerfilId = perfilNovo.Id;
                perfilPermissao.DataHora = DateTime.Now;
                perfilPermissao.Excluido = false;
                perfilPermissao.PermissaoId = permissao.Id;
                registrosEfetuados.Add(perfilPermissao);
                bancoContext.tbl_PerfilPermissao.Add(perfilPermissao);
            }

            bancoContext.SaveChanges();
            return idPerfil;
        }

        public List<PerfilPermissao> ReadAll() {
            return bancoContext.tbl_PerfilPermissao.ToList();
        }

        public PerfilPermissao ReadById(int id) {
            return bancoContext.tbl_PerfilPermissao.FirstOrDefault(pp => pp.Id == id);
        }

        public List<PerfilPermissao> ReadByPerfil(Perfil perfil) {
            return bancoContext.tbl_PerfilPermissao.Where(pp => pp.PerfilId == perfil.Id).ToList();
        }

        public List<PerfilPermissao> ReadByPermissao(Permissao permissao) {
            return bancoContext.tbl_PerfilPermissao.Where(pp => pp.PermissaoId == permissao.Id).ToList();
        }

        public int Update(ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao) {
            Perfil perfilMexido = modeloCadastroPerfilPermissao.Perfil;
            perfilMexido.Excluido = false;
            perfilMexido = perfilRepository.Update(perfilMexido);
            int idPerfil = perfilMexido.Id;

            List<PerfilPermissao> registrosExistentes = ReadByPerfil(perfilMexido);
            foreach (PerfilPermissao perfilPermissao in registrosExistentes) {
                Delete(perfilPermissao);
            }

            List<Permissao> permissoes = modeloCadastroPerfilPermissao.PermissoesSelecionadas;
            List<PerfilPermissao> registrosEfetuados = new();
            foreach (Permissao permissao in permissoes) {
                PerfilPermissao perfilPermissao = new PerfilPermissao();
                idPerfil = perfilMexido.Id;
                perfilPermissao.PerfilId = perfilMexido.Id;
                perfilPermissao.DataHora = DateTime.Now;
                perfilPermissao.Excluido = false;
                perfilPermissao.PermissaoId = permissao.Id;
                registrosEfetuados.Add(perfilPermissao);
                bancoContext.tbl_PerfilPermissao.Add(perfilPermissao);
            }

            bancoContext.SaveChanges();
            return idPerfil;
        }

        public bool Delete(PerfilPermissao perfilPermissao) {
            PerfilPermissao perfilPermissaoBanco = ReadById(perfilPermissao.Id);

            if (perfilPermissaoBanco == null) {
                throw new Exception("Houve um erro ao encontrar o registro a ser excluído");
            }

            bancoContext.tbl_PerfilPermissao.Remove(perfilPermissao);
            bancoContext.SaveChanges();
            return true;
        }
    }
}
