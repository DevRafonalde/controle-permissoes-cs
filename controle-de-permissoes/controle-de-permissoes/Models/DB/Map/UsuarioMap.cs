using controle_de_permissoes.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace controle_de_permissoes.Models.DB.Map {
    public class UsuarioMap : IEntityTypeConfiguration<Usuario> {
        public void Configure(EntityTypeBuilder<Usuario> builder) {
            builder.Property(p => p.Id).HasColumnName("ID");
            builder.Property(p => p.NomeCompleto).HasColumnName("NomeCompleto").HasMaxLength(150);
            builder.Property(p => p.NomeAmigavel).HasColumnName("NomeAmigavel").HasMaxLength(100);
            builder.Property(p => p.NomeUser).HasColumnName("NomeUser").HasColumnType("varchar").HasMaxLength(30);
            builder.Property(p => p.SenhaUser).HasColumnName("SenhaUser").HasColumnType("varchar").HasMaxLength(50);
            builder.Property(p => p.IdWebRi).HasColumnName("ID_WebRI");
            builder.Property(p => p.IdWebTd).HasColumnName("ID_WebTD");
            builder.Property(p => p.IdWebRiCaixa).HasColumnName("ID_WebRI_Caixa");
            builder.Property(p => p.IdWebTdCaixa).HasColumnName("ID_WebTD_Caixa");
            builder.Property(p => p.Ativo).HasColumnName("Ativo");
            builder.Property(p => p.CaixaVirtual).HasColumnName("CaixaVirtual");
            builder.Property(p => p.Observacao).HasColumnName("Observacao").HasMaxLength(2000);

            builder.Ignore(p => p.UsuariosPermissao);
        }
    }
}
