using controle_de_permissoes.Models.Entities.Orm;

namespace controle_de_permissoes.Models.Entities.View {
    public class ModeloCadastroPerfilPermissao {
        public Perfil Perfil { get; set; }

        public List<Permissao> PermissoesSelecionadas { get; set; }

        public List<int> PermissoesSelecionadasIds { get; set; }

        public List<Sistema> TodosSistemas { get; set; }

        public List<Permissao> TodasPermissoes { get; set; }

        public List<Permissao> getPermissoesSelecionadas() {
            return PermissoesSelecionadas;
        }

        public void setPermissoesSelecionadas(List<Permissao> permissoes) {
            PermissoesSelecionadas = permissoes;
        }
    }
}
