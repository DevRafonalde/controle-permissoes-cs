using controle_de_permissoes.Models.Entities.Orm;

namespace controle_de_permissoes.Models.Entities.View {
    public class ModeloCadastroUsuarioPerfil {
        public Usuario Usuario { get; set; }

        public List<Perfil>? PerfisSelecionados { get; set; }

        public List<int> PerfisSelecionadosIds { get; set; }

        public List<Perfil>? TodosPerfis { get; set; }

        public List<Perfil> getPerfisSelecionados() {
            return PerfisSelecionados;
        }

        public void setPerfisSelecionados(List<Perfil> perfis) {
            PerfisSelecionados = perfis;
        }
    }
}
