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

        public List<PerfilPermissao> Create(ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao) {
            List<Permissao> permissoes = modeloCadastroPerfilPermissao.PermissoesPerfil;
            List <PerfilPermissao> registrosEfetuados = new();
            foreach (Permissao permissao in permissoes) {
                PerfilPermissao perfilPermissao = new PerfilPermissao();
                perfilPermissao.Perfil = modeloCadastroPerfilPermissao.Perfil;
                perfilPermissao.Perfil.Excluido = false;
                perfilPermissao.DataHora = DateTime.Now;
                perfilPermissao.Excluido = false;
                perfilPermissao.Permissao = permissao;
                registrosEfetuados.Add(perfilPermissao);
                bancoContext.tbl_PerfilPermissao.Add(perfilPermissao);
            }

            bancoContext.SaveChanges();
            return registrosEfetuados;
        }

        public List<PerfilPermissao> ReadAll() {
            return bancoContext.tbl_PerfilPermissao.ToList();
        }

        public PerfilPermissao ReadById(int id) {
            return bancoContext.tbl_PerfilPermissao.FirstOrDefault(pp => pp.Id == id);
        }

        public List<PerfilPermissao> ReadByPerfil(Perfil perfil) {
            return bancoContext.tbl_PerfilPermissao.Where(up => up.PerfilId == perfil.Id).ToList();
        }

        public List<PerfilPermissao> Update(ModeloCadastroPerfilPermissao modeloCadastroPerfilPermissao) {
            Perfil perfilMexido = modeloCadastroPerfilPermissao.Perfil;
            perfilMexido.Excluido = false;
            perfilRepository.Update(perfilMexido);

            List<PerfilPermissao> registrosExistentes = ReadByPerfil(perfilMexido);
            foreach (PerfilPermissao perfilPermissao in registrosExistentes) {
                Delete(perfilPermissao);
            }

            List<Permissao> permissoes = modeloCadastroPerfilPermissao.PermissoesPerfil;
            List<PerfilPermissao> registrosEfetuados = new();
            foreach (Permissao permissao in permissoes) {
                PerfilPermissao perfilPermissao = new PerfilPermissao();
                perfilPermissao.Perfil = perfilMexido;
                perfilPermissao.DataHora = DateTime.Now;
                perfilPermissao.Excluido = false;
                perfilPermissao.Permissao = permissao;
                registrosEfetuados.Add(perfilPermissao);
                bancoContext.tbl_PerfilPermissao.Add(perfilPermissao);
            }

            bancoContext.SaveChanges();
            return registrosEfetuados;
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
