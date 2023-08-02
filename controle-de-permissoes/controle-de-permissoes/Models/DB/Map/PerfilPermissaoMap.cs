using controle_de_permissoes.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace controle_de_permissoes.Models.DB.Map {
    public class PerfilPermissaoMap : IEntityTypeConfiguration<PerfilPermissao> {
        public void Configure(EntityTypeBuilder<PerfilPermissao> builder) {
            builder.HasKey(pp => pp.Id);

            builder.HasOne(pp => pp.Perfil)
                   .WithMany(p => p.PerfisPermissao)
                   .HasForeignKey(pp => pp.PerfilId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pp => pp.Permissao)
                   .WithMany(p => p.PerfisPermissao)
                   .HasForeignKey(pp => pp.PermissaoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(pp => pp.Id).HasColumnName("ID");
            builder.Property(pp => pp.PerfilId).HasColumnName("ID_Perfil");
            builder.Property(pp => pp.PermissaoId).HasColumnName("ID_Permissao");
            builder.Property(pp => pp.DataHora).HasColumnName("DataHora").HasColumnType("datetime");
            builder.Property(pp => pp.Excluido).HasColumnName("Excluido");

            builder.Ignore(pp => pp.Permissao);
            builder.Ignore(pp => pp.Perfil);
        }
    }
}
