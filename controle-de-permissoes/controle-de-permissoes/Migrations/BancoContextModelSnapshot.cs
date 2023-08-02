﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using controle_de_permissoes.Models.DB;

#nullable disable

namespace controle_de_permissoes.Migrations
{
    [DbContext(typeof(BancoContext))]
    partial class BancoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("controle_de_permissoes.Models.Entities.Perfil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Descricao");

                    b.Property<bool>("Excluido")
                        .HasColumnType("bit")
                        .HasColumnName("Excluido");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Nome");

                    b.Property<int?>("SistemaId")
                        .HasColumnType("int")
                        .HasColumnName("ID_Sistema");

                    b.HasKey("Id");

                    b.ToTable("tbl_Perfil");
                });

            modelBuilder.Entity("controle_de_permissoes.Models.Entities.PerfilPermissao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("datetime")
                        .HasColumnName("DataHora");

                    b.Property<bool>("Excluido")
                        .HasColumnType("bit")
                        .HasColumnName("Excluido");

                    b.Property<int>("PerfilId")
                        .HasColumnType("int")
                        .HasColumnName("ID_Perfil");

                    b.Property<int>("PermissaoId")
                        .HasColumnType("int")
                        .HasColumnName("ID_Permissao");

                    b.HasKey("Id");

                    b.HasIndex("PerfilId");

                    b.ToTable("tbl_PerfilPermissao");
                });

            modelBuilder.Entity("controle_de_permissoes.Models.Entities.Permissao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Desabilitado")
                        .HasColumnType("bit")
                        .HasColumnName("Desabilitado");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Descricao");

                    b.Property<bool>("GerarLog")
                        .HasColumnType("bit")
                        .HasColumnName("GerarLog");

                    b.Property<int?>("IdPermissaoSuperior")
                        .HasColumnType("int")
                        .HasColumnName("ID_Permissao_Superior");

                    b.Property<string>("Mnemonico")
                        .HasMaxLength(255)
                        .HasColumnType("varchar")
                        .HasColumnName("Mnemonico");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Nome");

                    b.Property<int?>("SistemaId")
                        .HasColumnType("int")
                        .HasColumnName("ID_Sistema");

                    b.HasKey("Id");

                    b.ToTable("tbl_Permissao");
                });

            modelBuilder.Entity("controle_de_permissoes.Models.Entities.Sistema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Descricao");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Nome");

                    b.Property<string>("Prefixo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar")
                        .HasColumnName("Prefixo");

                    b.Property<string>("VersaoBanco")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("VersaoBanco");

                    b.HasKey("Id");

                    b.ToTable("tbl_Sistema");
                });

            modelBuilder.Entity("controle_de_permissoes.Models.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit")
                        .HasColumnName("Ativo");

                    b.Property<bool>("CaixaVirtual")
                        .HasColumnType("bit")
                        .HasColumnName("CaixaVirtual");

                    b.Property<int?>("IdWebRi")
                        .HasColumnType("int")
                        .HasColumnName("ID_WebRI");

                    b.Property<int?>("IdWebRiCaixa")
                        .HasColumnType("int")
                        .HasColumnName("ID_WebRI_Caixa");

                    b.Property<int?>("IdWebTd")
                        .HasColumnType("int")
                        .HasColumnName("ID_WebTD");

                    b.Property<int?>("IdWebTdCaixa")
                        .HasColumnType("int")
                        .HasColumnName("ID_WebTD_Caixa");

                    b.Property<string>("NomeAmigavel")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("NomeAmigavel");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("NomeCompleto");

                    b.Property<string>("NomeUser")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar")
                        .HasColumnName("NomeUser");

                    b.Property<string>("Observacao")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)")
                        .HasColumnName("Observacao");

                    b.Property<string>("SenhaUser")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar")
                        .HasColumnName("SenhaUser");

                    b.HasKey("Id");

                    b.ToTable("tbl_Usuario");
                });

            modelBuilder.Entity("controle_de_permissoes.Models.Entities.UsuarioPermissao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("datetime")
                        .HasColumnName("DataHora");

                    b.Property<bool>("Excluido")
                        .HasColumnType("bit")
                        .HasColumnName("Excluido");

                    b.Property<bool>("Negacao")
                        .HasColumnType("bit")
                        .HasColumnName("Negacao");

                    b.Property<int?>("PerfilId")
                        .HasColumnType("int")
                        .HasColumnName("ID_Perfil");

                    b.Property<int?>("PermissaoId")
                        .HasColumnType("int")
                        .HasColumnName("ID_Permissao");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int")
                        .HasColumnName("ID_Usuario");

                    b.HasKey("Id");

                    b.HasIndex("PerfilId");

                    b.HasIndex("PermissaoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("tbl_UsuarioPermissao");
                });

            modelBuilder.Entity("controle_de_permissoes.Models.Entities.PerfilPermissao", b =>
                {
                    b.HasOne("controle_de_permissoes.Models.Entities.Perfil", null)
                        .WithMany("PerfisPermissao")
                        .HasForeignKey("PerfilId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("controle_de_permissoes.Models.Entities.UsuarioPermissao", b =>
                {
                    b.HasOne("controle_de_permissoes.Models.Entities.Perfil", null)
                        .WithMany("UsuariosPermissao")
                        .HasForeignKey("PerfilId");

                    b.HasOne("controle_de_permissoes.Models.Entities.Permissao", "Permissao")
                        .WithMany("UsuariosPermissao")
                        .HasForeignKey("PermissaoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("controle_de_permissoes.Models.Entities.Usuario", null)
                        .WithMany("UsuariosPermissao")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permissao");
                });

            modelBuilder.Entity("controle_de_permissoes.Models.Entities.Perfil", b =>
                {
                    b.Navigation("PerfisPermissao");

                    b.Navigation("UsuariosPermissao");
                });

            modelBuilder.Entity("controle_de_permissoes.Models.Entities.Permissao", b =>
                {
                    b.Navigation("UsuariosPermissao");
                });

            modelBuilder.Entity("controle_de_permissoes.Models.Entities.Usuario", b =>
                {
                    b.Navigation("UsuariosPermissao");
                });
#pragma warning restore 612, 618
        }
    }
}
