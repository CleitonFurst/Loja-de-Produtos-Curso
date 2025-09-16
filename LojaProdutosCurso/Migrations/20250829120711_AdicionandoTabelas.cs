using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LojaProdutosCurso.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantidadeEstoque = table.Column<int>(type: "int", nullable: false),
                    CategoriaModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaModelId",
                        column: x => x.CategoriaModelId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Eletrônicos" },
                    { 2, "Roupas" },
                    { 3, "Calçados" },
                    { 4, "Livros" }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "CategoriaModelId", "Foto", "Marca", "Nome", "QuantidadeEstoque", "Valor" },
                values: new object[,]
                {
                    { 1, 1, "smartphone_xyz.jpg", "MarcaA", "Smartphone XYZ", 50, 1500.00m },
                    { 2, 1, "notebook_abc.jpg", "MarcaB", "Notebook ABC", 30, 3500.00m },
                    { 3, 2, "camiseta_casual.jpg", "MarcaC", "Camiseta Casual", 100, 80.00m },
                    { 4, 3, "tenis_esportivo.jpg", "MarcaD", "Tênis Esportivo", 75, 200.00m },
                    { 5, 4, "livro_csharp_avancado.jpg", "EditoraX", "Livro de C# Avançado", 40, 120.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaModelId",
                table: "Produtos",
                column: "CategoriaModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
