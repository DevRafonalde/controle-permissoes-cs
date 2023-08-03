using controle_de_permissoes.Models.Entities.Orm;

namespace controle_de_permissoes.Models.Entities.View {
    public class ModeloCadastroPerfilPermissao {
        public Perfil Perfil;

        public List<Permissao> PermissoesSelecionadas;

        public List<Sistema> TodosSistemas;

        public List<Permissao> TodasPermissoes;
    }
}
