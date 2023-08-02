using controle_de_permissoes.Models.Entities.Orm;

namespace controle_de_permissoes.Models.Entities.View {
    public class ModeloCadastroUsuarioPerfil {
        public Usuario Usuario { get; set; }

        public List<Perfil> PerfisUsuario { get; set; }
    }
}
