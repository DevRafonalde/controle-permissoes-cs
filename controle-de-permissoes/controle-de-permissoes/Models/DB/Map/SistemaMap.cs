using controle_de_permissoes.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace controle_de_permissoes.Models.DB.Map {
    public class SistemaMap : IEntityTypeConfiguration<Sistema> {
        public void Configure(EntityTypeBuilder<Sistema> builder) {
            builder.Property(p => p.Id).HasColumnName("ID");
            builder.Property(p => p.Nome).HasColumnName("Nome").HasMaxLength(100);
            builder.Property(p => p.Prefixo).HasColumnName("Prefixo").HasColumnType("varchar").HasMaxLength(10);
            builder.Property(p => p.Descricao).HasColumnName("Descricao");
            builder.Property(p => p.VersaoBanco).HasColumnName("VersaoBanco").HasMaxLength(100);

            builder.Ignore(p => p.Permissoes);
            builder.Ignore(p => p.Perfis);
        }
    }
}
