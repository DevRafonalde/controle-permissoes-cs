using controle_de_permissoes.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace controle_de_permissoes.Models.DB.Map {
    public class UsuarioPermissaoMap : IEntityTypeConfiguration<UsuarioPermissao> {
        public void Configure(EntityTypeBuilder<UsuarioPermissao> builder) {
            builder.HasKey(up => up.Id);

            builder.HasOne(up => up.Perfil)
                   .WithMany(p => p.UsuariosPermissao)
                   .HasForeignKey(up => up.PerfilId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(up => up.Usuario)
                   .WithMany(u => u.UsuariosPermissao)
                   .HasForeignKey(up => up.UsuarioId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(up => up.Permissao)
                   .WithMany(p => p.UsuariosPermissao)
                   .HasForeignKey(up => up.PermissaoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(up => up.Id).HasColumnName("ID").HasColumnOrder(1);
            builder.Property(up => up.UsuarioId).HasColumnName("ID_Usuario").HasColumnOrder(2);
            builder.Property(up => up.PerfilId).HasColumnName("ID_Perfil").HasColumnOrder(3);
            builder.Property(up => up.PermissaoId).HasColumnName("ID_Permissao").HasColumnOrder(4);
            builder.Property(up => up.Negacao).HasColumnName("Negacao").HasColumnOrder(5);
            builder.Property(up => up.DataHora).HasColumnName("DataHora").HasColumnType("datetime").HasColumnOrder(6);
            builder.Property(up => up.Excluido).HasColumnName("Excluido").HasColumnOrder(7);

            builder.Ignore(up => up.Usuario);
            builder.Ignore(up => up.Perfil);
        }
    }
}
