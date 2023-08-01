using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace controle_de_permissoes.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoTblUsuarioPermissao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ID_Perfil",
                table: "tbl_UsuarioPermissao",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PermissaoId",
                table: "tbl_UsuarioPermissao",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_UsuarioPermissao_PermissaoId",
                table: "tbl_UsuarioPermissao",
                column: "PermissaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_UsuarioPermissao_tbl_Perfil_ID_Perfil",
                table: "tbl_UsuarioPermissao",
                column: "ID_Perfil",
                principalTable: "tbl_Perfil",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_UsuarioPermissao_tbl_Permissao_PermissaoId",
                table: "tbl_UsuarioPermissao",
                column: "PermissaoId",
                principalTable: "tbl_Permissao",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_UsuarioPermissao_tbl_Perfil_ID_Perfil",
                table: "tbl_UsuarioPermissao");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_UsuarioPermissao_tbl_Permissao_PermissaoId",
                table: "tbl_UsuarioPermissao");

            migrationBuilder.DropIndex(
                name: "IX_tbl_UsuarioPermissao_PermissaoId",
                table: "tbl_UsuarioPermissao");

            migrationBuilder.DropColumn(
                name: "PermissaoId",
                table: "tbl_UsuarioPermissao");

            migrationBuilder.AlterColumn<int>(
                name: "ID_Perfil",
                table: "tbl_UsuarioPermissao",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_UsuarioPermissao_tbl_Perfil_ID_Perfil",
                table: "tbl_UsuarioPermissao",
                column: "ID_Perfil",
                principalTable: "tbl_Perfil",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
