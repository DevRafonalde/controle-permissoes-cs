using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using controle_de_permissoes.Models.Entities.Orm;

namespace controle_de_permissoes.Models.DB.Map
{
    public class PermissaoMap : IEntityTypeConfiguration<Permissao> {
        public void Configure(EntityTypeBuilder<Permissao> builder) {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Sistema)
                   .WithMany(s => s.Permissoes)
                   .HasForeignKey(p => p.SistemaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Id).HasColumnName("ID");
            builder.Property(p => p.SistemaId).HasColumnName("ID_Sistema");
            builder.Property(p => p.Nome).HasColumnName("Nome").HasMaxLength(255);
            builder.Property(p => p.Descricao).HasColumnName("Descricao");
            builder.Property(p => p.GerarLog).HasColumnName("GerarLog");
            builder.Property(p => p.IdPermissaoSuperior).HasColumnName("ID_Permissao_Superior");
            builder.Property(p => p.Desabilitado).HasColumnName("Desabilitado");
            builder.Property(p => p.Mnemonico).HasColumnName("Mnemonico").HasColumnType("varchar").HasMaxLength(255);

            builder.Ignore(p => p.Sistema);
            builder.Ignore(p => p.PerfisPermissao);
        }
    }
}
