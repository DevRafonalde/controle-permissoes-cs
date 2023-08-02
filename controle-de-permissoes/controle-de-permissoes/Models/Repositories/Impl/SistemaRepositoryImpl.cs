using controle_de_permissoes.Models.DB;
using controle_de_permissoes.Models.Entities.Orm;
using controle_de_permissoes.Models.Repositories.Interfaces;

namespace controle_de_permissoes.Models.Repositories.Impl
{
    public class SistemaRepositoryImpl : SistemaRepository {
        private readonly BancoContext bancoContext;

        public SistemaRepositoryImpl(BancoContext bancoContext) {
            this.bancoContext = bancoContext;
        }

        public List<Sistema> ReadAll() {
            return bancoContext.tbl_Sistema.ToList();
        }
    }
}
