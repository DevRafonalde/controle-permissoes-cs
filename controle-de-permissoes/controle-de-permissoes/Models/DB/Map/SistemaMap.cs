using controle_de_permissoes.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace controle_de_permissoes.Models.DB.Map {
    public class SistemaMap : IEntityTypeConfiguration<Sistema> {
        public void Configure(EntityTypeBuilder<Sistema> builder) {
            builder.Property(p => p.Id).HasColumnName("ID");
            builder.Property(p => p.Nome).HasColumnName("Nome");
            builder.Property(p => p.Prefixo).HasColumnName("Prefixo");
            builder.Property(p => p.Descricao).HasColumnName("Descricao");
            builder.Property(p => p.VersaoBanco).HasColumnName("VersaoBanco");

            builder.Ignore(p => p.Permissoes);
            builder.Ignore(p => p.Perfis);
        }
    }
}
