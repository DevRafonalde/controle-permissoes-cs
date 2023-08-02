namespace controle_de_permissoes.Models.Entities {
    public class Permissao {
        public int Id { get; set; }

        public int? SistemaId { get; set; }

        public string Nome { get; set; }

        public string? Descricao { get; set; }

        public bool GerarLog { get; set; }

        public int? IdPermissaoSuperior { get; set;}

        public bool Desabilitado { get; set; }

        public string? Mnemonico { get; set; }

        public Sistema? Sistema { get; set; }

        public virtual List<PerfilPermissao>? PerfisPermissao { get; set; }

        public virtual List<UsuarioPermissao>? UsuariosPermissao { get; set; }

        public Sistema GetSistema() {
            if (Sistema == null) {
                Sistema sistemaVazio = new Sistema();
                sistemaVazio.Nome = string.Empty;
                return sistemaVazio;
            }
            return Sistema;
        }
    }
}
