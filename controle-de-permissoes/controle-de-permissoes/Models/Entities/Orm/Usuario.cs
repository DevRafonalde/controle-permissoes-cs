namespace controle_de_permissoes.Models.Entities.Orm
{
    public class Usuario
    {
        public int Id { get; set; }

        public string NomeCompleto { get; set; }

        public string? NomeAmigavel { get; set; }

        public string NomeUser { get; set; }

        public string SenhaUser { get; set; }

        public int? IdWebRi { get; set; }

        public int? IdWebTd { get; set; }

        public int? IdWebRiCaixa { get; set; }

        public int? IdWebTdCaixa { get; set; }

        public bool Ativo { get; set; }

        public bool CaixaVirtual { get; set; }

        public string? Observacao { get; set; }

        public virtual List<UsuarioPermissao>? UsuariosPermissao { get; set; }
    }
}
