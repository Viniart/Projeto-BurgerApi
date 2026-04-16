using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerApi.Migrations
{
    /// <inheritdoc />
    public partial class Teste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Produtos_TB_Categorias_CategoriaID",
                table: "TB_Produtos");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaID",
                table: "TB_Produtos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Produtos_TB_Categorias_CategoriaID",
                table: "TB_Produtos",
                column: "CategoriaID",
                principalTable: "TB_Categorias",
                principalColumn: "CategoriaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Produtos_TB_Categorias_CategoriaID",
                table: "TB_Produtos");

            migrationBuilder.AlterColumn<int>(
                name: "CategoriaID",
                table: "TB_Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Produtos_TB_Categorias_CategoriaID",
                table: "TB_Produtos",
                column: "CategoriaID",
                principalTable: "TB_Categorias",
                principalColumn: "CategoriaID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
