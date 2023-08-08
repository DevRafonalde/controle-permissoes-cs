using controle_de_permissoes.Models.DB;
using controle_de_permissoes.Models.Entities.Orm;
using controle_de_permissoes.Models.Repositories.Interfaces;

namespace controle_de_permissoes.Models.Repositories.Impl
{
    public class PerfilRepositoryImpl : PerfilRepository {
        private readonly BancoContext bancoContext;

        public PerfilRepositoryImpl(BancoContext bancoContext) {
            this.bancoContext = bancoContext;
        }

        public Perfil Create(Perfil perfil) {
            perfil.Excluido = false;
            bancoContext.tbl_Perfil.Add(perfil);
            bancoContext.SaveChanges();
            return perfil;
        }

        public List<Perfil> ReadAll() {
            return bancoContext.tbl_Perfil.ToList();
        }

        public Perfil ReadById(int id) {
            return bancoContext.tbl_Perfil.FirstOrDefault(p => p.Id == id);
        }

        public Perfil Update(Perfil perfil) {
            Perfil perfilBanco = ReadById(perfil.Id);

            if (perfilBanco == null) {
                throw new Exception("Houve um erro ao encontrar o Perfil a ser editado");
            }

            perfilBanco.SistemaId = perfil.SistemaId;
            perfilBanco.Sistema = perfil.Sistema;
            perfilBanco.Nome = perfil.Nome;
            perfilBanco.Descricao = perfil.Descricao;
            perfilBanco.Excluido = false;

            bancoContext.tbl_Perfil.Update(perfilBanco);
            bancoContext.SaveChanges();
            return perfilBanco;
        }

        public bool Delete(Perfil perfil) {
            Perfil perfilBanco = ReadById(perfil.Id);

            if (perfilBanco == null ) {
                throw new Exception("Houve um erro ao encontrar o Perfil a ser excluído");
            }

            bancoContext.tbl_Perfil.Remove(perfilBanco);
            bancoContext.SaveChanges();
            return true;
        }
    }
}
