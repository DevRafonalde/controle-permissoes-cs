namespace controle_de_permissoes.Models.Entities.Orm
{
    public class UsuarioPermissao
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public int? PerfilId { get; set; }

        public int? PermissaoId { get; set; }

        public bool Negacao { get; set; }

        public DateTime DataHora { get; set; }

        public bool Excluido { get; set; }

        public Usuario? Usuario { get; set; }

        public Perfil? Perfil { get; set; }

        public Permissao? Permissao { get; set; }
    }
}
