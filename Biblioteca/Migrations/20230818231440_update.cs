using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaLivro_Categorias_CategoriaId",
                table: "CategoriaLivro");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaLivro_Livros_LivroId",
                table: "CategoriaLivro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriaLivro",
                table: "CategoriaLivro");

            migrationBuilder.RenameTable(
                name: "CategoriaLivro",
                newName: "CategoriaLivros");

            migrationBuilder.RenameIndex(
                name: "IX_CategoriaLivro_LivroId",
                table: "CategoriaLivros",
                newName: "IX_CategoriaLivros_LivroId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriaLivros",
                table: "CategoriaLivros",
                columns: new[] { "CategoriaId", "LivroId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaLivros_Categorias_CategoriaId",
                table: "CategoriaLivros",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaLivros_Livros_LivroId",
                table: "CategoriaLivros",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaLivros_Categorias_CategoriaId",
                table: "CategoriaLivros");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaLivros_Livros_LivroId",
                table: "CategoriaLivros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriaLivros",
                table: "CategoriaLivros");

            migrationBuilder.RenameTable(
                name: "CategoriaLivros",
                newName: "CategoriaLivro");

            migrationBuilder.RenameIndex(
                name: "IX_CategoriaLivros_LivroId",
                table: "CategoriaLivro",
                newName: "IX_CategoriaLivro_LivroId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriaLivro",
                table: "CategoriaLivro",
                columns: new[] { "CategoriaId", "LivroId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaLivro_Categorias_CategoriaId",
                table: "CategoriaLivro",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaLivro_Livros_LivroId",
                table: "CategoriaLivro",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
