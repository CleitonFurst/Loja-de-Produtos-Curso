using LojaProdutosCurso.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaProdutosCurso.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<ProdutosBaixadosModel> ProdutosBaixados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaModel>().HasData(
                new CategoriaModel() { Id = 1, Nome = "Eletrônicos" },
                new CategoriaModel() { Id = 2, Nome = "Roupas" },
                new CategoriaModel() { Id = 3, Nome = "Calçados" },
                new CategoriaModel() { Id = 4, Nome = "Livros" }
            );
            modelBuilder.Entity<ProdutoModel>().HasData(
                new ProdutoModel()
                {
                    Id = 1,
                    Nome = "Smartphone XYZ",
                    Marca = "MarcaA",
                    Foto = "smartphone_xyz.jpg",
                    Valor = 1500.00M,
                    QuantidadeEstoque = 50,
                    CategoriaModelId = 1
                },
                new ProdutoModel()
                {
                    Id = 2,
                    Nome = "Notebook ABC",
                    Marca = "MarcaB",
                    Foto = "notebook_abc.jpg",
                    Valor = 3500.00M,
                    QuantidadeEstoque = 30,
                    CategoriaModelId = 1
                },
                new ProdutoModel()
                {
                    Id = 3,
                    Nome = "Camiseta Casual",
                    Marca = "MarcaC",
                    Foto = "camiseta_casual.jpg",
                    Valor = 80.00M,
                    QuantidadeEstoque = 100,
                    CategoriaModelId = 2
                },
                new ProdutoModel()
                {
                    Id = 4,
                    Nome = "Tênis Esportivo",
                    Marca = "MarcaD",
                    Foto = "tenis_esportivo.jpg",
                    Valor = 200.00M,
                    QuantidadeEstoque = 75,
                    CategoriaModelId = 3
                },
                new ProdutoModel()
                {
                    Id = 5,
                    Nome = "Livro de C# Avançado",
                    Marca = "EditoraX",
                    Foto = "livro_csharp_avancado.jpg",
                    Valor = 120.00M,
                    QuantidadeEstoque = 40,
                    CategoriaModelId = 4
                }
            );
        }
    }
}
