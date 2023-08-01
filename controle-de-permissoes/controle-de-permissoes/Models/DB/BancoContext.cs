using controle_de_permissoes.Models.DB.Map;
using controle_de_permissoes.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace controle_de_permissoes.Models.DB {
    public class BancoContext : DbContext {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) {
        }

        public DbSet<Perfil> tbl_Perfil { get; set; }

        public DbSet<PerfilPermissao> tbl_PerfilPermissao { get; set; }

        public DbSet<Permissao> tbl_Permissao { get; set; }

        public DbSet<Sistema> tbl_Sistema { get; set; }

        public DbSet<Usuario> tbl_Usuario { get; set; }

        public DbSet<UsuarioPermissao> tbl_UsuarioPermissao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new PerfilMap());
            modelBuilder.ApplyConfiguration(new PerfilPermissaoMap());
            modelBuilder.ApplyConfiguration(new PermissaoMap());
            modelBuilder.ApplyConfiguration(new SistemaMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new UsuarioPermissaoMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
