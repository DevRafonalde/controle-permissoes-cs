using controle_de_permissoes.Models.DB;
using controle_de_permissoes.Models.Entities.Orm;
using controle_de_permissoes.Models.Repositories.Interfaces;

namespace controle_de_permissoes.Models.Repositories.Impl
{
    public class PermissaoRepositoryImpl : PermissaoRepository {
        private readonly BancoContext bancoContext;

        public PermissaoRepositoryImpl(BancoContext bancoContext) {
            this.bancoContext = bancoContext;
        }

        public Permissao Create(Permissao permissao) {
            List<Permissao> permissoes = ReadAll();
            int ultimoId = permissoes.LastOrDefault().Id;
            ultimoId++;
            permissao.Desabilitado = false;
            permissao.Id = ultimoId;
            
            bancoContext.tbl_Permissao.Add(permissao);
            bancoContext.SaveChanges();
            return permissao;
        }

        public List<Permissao> ReadAll() {
            return bancoContext.tbl_Permissao.ToList();
        }

        public Permissao ReadById(int id) {
            return bancoContext.tbl_Permissao.FirstOrDefault(p => p.Id == id);
        }

        public Permissao Update(Permissao permissao) {
            Permissao permissaoBanco = ReadById(permissao.Id);

            if (permissaoBanco == null) {
                throw new Exception("Houve um erro ao encontrar a permissão a ser editada");
            }

            List<Permissao> permissoes = ReadAll();
            int ultimoId = permissoes.LastOrDefault().Id;
            ultimoId++;
            permissao.Desabilitado = false;
            permissao.Id = ultimoId;

            bancoContext.tbl_Permissao.Add(permissao);
            bancoContext.SaveChanges();
            return permissao;
        }

        public bool Delete(Permissao permissao) {
            Permissao permissaoBanco = ReadById(permissao.Id);

            if (permissaoBanco == null) {
                throw new Exception("Houve um erro ao encontrar a permissão a ser excluída");
            }

            bancoContext.tbl_Permissao.Remove(permissao);
            bancoContext.SaveChanges();
            return true;
        }
    }
}
