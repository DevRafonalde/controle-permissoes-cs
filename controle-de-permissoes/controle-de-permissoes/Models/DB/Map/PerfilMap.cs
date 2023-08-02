using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using controle_de_permissoes.Models.Entities.Orm;

namespace controle_de_permissoes.Models.DB.Map
{
    public class PerfilMap : IEntityTypeConfiguration<Perfil> {
        public void Configure(EntityTypeBuilder<Perfil> builder) {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Sistema)
                   .WithMany(s => s.Perfis)
                   .HasForeignKey(p => p.SistemaId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Id).HasColumnName("ID");
            builder.Property(p => p.SistemaId).HasColumnName("ID_Sistema");
            builder.Property(p => p.Nome).HasColumnName("Nome").HasMaxLength(100);
            builder.Property(p => p.Descricao).HasColumnName("Descricao");
            builder.Property(p => p.Excluido).HasColumnName("Excluido");

            builder.Ignore(p => p.Sistema);
            builder.Ignore(p => p.PerfisPermissao);
            builder.Ignore(p => p.UsuariosPermissao);
        }
    }
}
