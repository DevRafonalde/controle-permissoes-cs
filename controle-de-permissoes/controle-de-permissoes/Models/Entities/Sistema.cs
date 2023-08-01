namespace controle_de_permissoes.Models.Entities {
    public class Sistema {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Prefixo { get; set; }

        public string Descricao { get; set; }

        public string VersaoBanco { get; set; }

        public virtual List<Perfil>? Perfis { get; set; }

        public virtual List<Permissao>? Permissoes { get; set; }
    }
}
