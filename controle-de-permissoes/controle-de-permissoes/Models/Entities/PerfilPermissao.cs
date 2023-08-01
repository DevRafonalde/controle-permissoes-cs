namespace controle_de_permissoes.Models.Entities {
    public class PerfilPermissao {
        public int Id { get; set; }

        public int PerfilId { get; set;}

        public int PermissaoId { get; set; }

        public DateTime DataHora { get; set; }

        public bool Excluido { get; set; }

        public Permissao? Permissao { get; set; }

        public Perfil? Perfil { get; set; }
    }
}
