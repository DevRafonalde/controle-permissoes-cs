using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace controle_de_permissoes.Migrations
{
    /// <inheritdoc />
    public partial class TesteConfigTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Perfil",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Sistema = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Perfil", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Permissao",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Sistema = table.Column<int>(type: "int", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GerarLog = table.Column<bool>(type: "bit", nullable: false),
                    ID_Permissao_Superior = table.Column<int>(type: "int", nullable: true),
                    Desabilitado = table.Column<bool>(type: "bit", nullable: false),
                    Mnemonico = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Permissao", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Sistema",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prefixo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VersaoBanco = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Sistema", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Usuario",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeAmigavel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenhaUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_WebRI = table.Column<int>(type: "int", nullable: false),
                    ID_WebTD = table.Column<int>(type: "int", nullable: false),
                    ID_WebRI_Caixa = table.Column<int>(type: "int", nullable: false),
                    ID_WebTD_Caixa = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    CaixaVirtual = table.Column<bool>(type: "bit", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Usuario", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PerfilPermissao",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Perfil = table.Column<int>(type: "int", nullable: false),
                    ID_Permissao = table.Column<int>(type: "int", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PerfilPermissao", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_PerfilPermissao_tbl_Perfil_ID_Perfil",
                        column: x => x.ID_Perfil,
                        principalTable: "tbl_Perfil",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_UsuarioPermissao",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Usuario = table.Column<int>(type: "int", nullable: false),
                    ID_Perfil = table.Column<int>(type: "int", nullable: false),
                    Negacao = table.Column<bool>(type: "bit", nullable: false),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_UsuarioPerfil", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tbl_UsuarioPerfil_tbl_Perfil_ID_Perfil",
                        column: x => x.ID_Perfil,
                        principalTable: "tbl_Perfil",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_UsuarioPerfil_tbl_Usuario_ID_Usuario",
                        column: x => x.ID_Usuario,
                        principalTable: "tbl_Usuario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PerfilPermissao_ID_Perfil",
                table: "tbl_PerfilPermissao",
                column: "ID_Perfil");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UsuarioPerfil_ID_Perfil",
                table: "tbl_UsuarioPermissao",
                column: "ID_Perfil");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UsuarioPerfil_ID_Usuario",
                table: "tbl_UsuarioPermissao",
                column: "ID_Usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_PerfilPermissao");

            migrationBuilder.DropTable(
                name: "tbl_Permissao");

            migrationBuilder.DropTable(
                name: "tbl_Sistema");

            migrationBuilder.DropTable(
                name: "tbl_UsuarioPermissao");

            migrationBuilder.DropTable(
                name: "tbl_Perfil");

            migrationBuilder.DropTable(
                name: "tbl_Usuario");
        }
    }
}
