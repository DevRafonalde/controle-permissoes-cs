using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace controle_de_permissoes.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoTblUsuarioPermissao2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_UsuarioPermissao_tbl_Permissao_PermissaoId",
                table: "tbl_UsuarioPermissao");

            migrationBuilder.RenameColumn(
                name: "PermissaoId",
                table: "tbl_UsuarioPermissao",
                newName: "ID_Permissao");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_UsuarioPermissao_PermissaoId",
                table: "tbl_UsuarioPermissao",
                newName: "IX_tbl_UsuarioPermissao_ID_Permissao");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataHora",
                table: "tbl_UsuarioPermissao",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_UsuarioPermissao_tbl_Permissao_ID_Permissao",
                table: "tbl_UsuarioPermissao",
                column: "ID_Permissao",
                principalTable: "tbl_Permissao",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_UsuarioPermissao_tbl_Permissao_ID_Permissao",
                table: "tbl_UsuarioPermissao");

            migrationBuilder.RenameColumn(
                name: "ID_Permissao",
                table: "tbl_UsuarioPermissao",
                newName: "PermissaoId");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_UsuarioPermissao_ID_Permissao",
                table: "tbl_UsuarioPermissao",
                newName: "IX_tbl_UsuarioPermissao_PermissaoId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataHora",
                table: "tbl_UsuarioPermissao",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_UsuarioPermissao_tbl_Permissao_PermissaoId",
                table: "tbl_UsuarioPermissao",
                column: "PermissaoId",
                principalTable: "tbl_Permissao",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
