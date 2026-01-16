using LojaProdutosCurso.Models;
using Microsoft.EntityFrameworkCore;
using UsuarioB2B = LojaProdutosCurso.Domain.Entities.Usuario;
using EnderecoB2B = LojaProdutosCurso.Domain.Entities.Endereco;
using ProdutoB2B = LojaProdutosCurso.Domain.Entities.Produto;
using CategoriaB2B = LojaProdutosCurso.Domain.Entities.Categoria;
using LojaProdutosCurso.Domain.Entities;

namespace LojaProdutosCurso.Data
{
    public class DataContext : DbContext
    {
        private readonly IHttpContextAccessor? _httpContextAccessor;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor httpContextAccessor) 
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // Entidades antigas (controllers/services antigos ainda usam estes)
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<ProdutosBaixadosModel> ProdutosBaixados { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<EnderecoModel> Enderecos { get; set; }

        // Novas entidades B2B
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Domain.Entities.Usuario> UsuariosB2B { get; set; }
        public DbSet<Domain.Entities.Endereco> EnderecosB2B { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<EnderecoCliente> EnderecosClientes { get; set; }
        public DbSet<Domain.Entities.Produto> ProdutosB2B { get; set; }
        public DbSet<Domain.Entities.Categoria> CategoriasB2B { get; set; }
        public DbSet<ListaDePreco> ListasDePreco { get; set; }
        public DbSet<ItemListaPreco> ItensListaPreco { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Campanha> Campanhas { get; set; }
        public DbSet<EventoNavegacao> EventosNavegacao { get; set; }
        public DbSet<Recomendacao> Recomendacoes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração de multi-tenancy (Global Query Filter)
            // TODO: Implementar após configurar TenantContext
            // modelBuilder.Entity<Usuario>().HasQueryFilter(u => u.TenantId == _currentTenantId);
            // ... aplicar para todas as entidades BaseEntity

            // Configurações de relacionamentos
            ConfigurarRelacionamentos(modelBuilder);

            // Configurações de índices para performance
            ConfigurarIndices(modelBuilder);

            // Seed data das entidades antigas (manter temporariamente)
            SeedDataAntigo(modelBuilder);

            // Seed data para demonstração B2B
            SeedDataB2B(modelBuilder);
        }

        private void ConfigurarRelacionamentos(ModelBuilder modelBuilder)
        {
            // Usuario -> Empresa
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Empresa)
                .WithMany(e => e.Usuarios)
                .HasForeignKey(u => u.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Usuario -> Cliente (opcional)
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Cliente)
                .WithMany(c => c.Usuarios)
                .HasForeignKey(u => u.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Usuario -> Endereco (1:1)
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Endereco)
                .WithOne(e => e.Usuario)
                .HasForeignKey<Endereco>(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cliente -> Enderecos (1:N)
            modelBuilder.Entity<EnderecoCliente>()
                .HasOne(e => e.Cliente)
                .WithMany(c => c.Enderecos)
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Produto -> Categoria
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Categoria hierárquica
            modelBuilder.Entity<Categoria>()
                .HasOne(c => c.CategoriaPai)
                .WithMany(c => c.Subcategorias)
                .HasForeignKey(c => c.CategoriaPaiId)
                .OnDelete(DeleteBehavior.Restrict);

            // ListaDePreco -> Cliente (opcional)
            modelBuilder.Entity<ListaDePreco>()
                .HasOne(l => l.Cliente)
                .WithMany()
                .HasForeignKey(l => l.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemListaPreco -> ListaDePreco
            modelBuilder.Entity<ItemListaPreco>()
                .HasOne(i => i.ListaDePreco)
                .WithMany(l => l.Itens)
                .HasForeignKey(i => i.ListaDePrecoId)
                .OnDelete(DeleteBehavior.Cascade);

            // ItemListaPreco -> Produto
            modelBuilder.Entity<ItemListaPreco>()
                .HasOne(i => i.Produto)
                .WithMany(p => p.ItensListaPreco)
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Pedido -> Cliente
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Pedido -> EnderecoEntrega
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.EnderecoEntrega)
                .WithMany()
                .HasForeignKey(p => p.EnderecoEntregaId)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemPedido -> Pedido
            modelBuilder.Entity<ItemPedido>()
                .HasOne(i => i.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            // ItemPedido -> Produto
            modelBuilder.Entity<ItemPedido>()
                .HasOne(i => i.Produto)
                .WithMany()
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Recomendacao -> CampanhaSugerida (opcional)
            modelBuilder.Entity<Recomendacao>()
                .HasOne(r => r.CampanhaSugerida)
                .WithMany()
                .HasForeignKey(r => r.CampanhaSugeridaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Recomendacao -> Produto (opcional)
            modelBuilder.Entity<Recomendacao>()
                .HasOne(r => r.Produto)
                .WithMany()
                .HasForeignKey(r => r.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Recomendacao -> AprovadoPor
            modelBuilder.Entity<Recomendacao>()
                .HasOne(r => r.AprovadoPor)
                .WithMany()
                .HasForeignKey(r => r.AprovadoPorUsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigurarIndices(ModelBuilder modelBuilder)
        {
            // Índices para multi-tenancy
            modelBuilder.Entity<Usuario>().HasIndex(u => u.TenantId);
            modelBuilder.Entity<Cliente>().HasIndex(c => c.TenantId);
            modelBuilder.Entity<Produto>().HasIndex(p => p.TenantId);
            modelBuilder.Entity<Pedido>().HasIndex(p => p.TenantId);

            // Índices para busca
            modelBuilder.Entity<Cliente>().HasIndex(c => c.CNPJ);
            modelBuilder.Entity<Cliente>().HasIndex(c => c.Email);
            modelBuilder.Entity<Cliente>().HasIndex(c => c.Segmento);
            modelBuilder.Entity<Produto>().HasIndex(p => p.Codigo);
            modelBuilder.Entity<Pedido>().HasIndex(p => p.Numero);
            modelBuilder.Entity<Pedido>().HasIndex(p => p.Status);
            modelBuilder.Entity<Pedido>().HasIndex(p => p.DataPedido);
            
            // Índice composto para queries comuns
            modelBuilder.Entity<Pedido>().HasIndex(p => new { p.ClienteId, p.Status });
            modelBuilder.Entity<ItemListaPreco>().HasIndex(i => new { i.ListaDePrecoId, i.ProdutoId });
        }

        private void SeedDataAntigo(ModelBuilder modelBuilder)
        {
            // Seed data das categorias antigas (manter)
            modelBuilder.Entity<CategoriaModel>().HasData(
                new CategoriaModel() { Id = 1, Nome = "Eletrônicos" },
                new CategoriaModel() { Id = 2, Nome = "Roupas" },
                new CategoriaModel() { Id = 3, Nome = "Calçados" },
                new CategoriaModel() { Id = 4, Nome = "Livros" }
            );

            // Seed data dos produtos antigos (manter)
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

        private void SeedDataB2B(ModelBuilder modelBuilder)
        {
            // Empresa de demonstração
            var empresaId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var tenantId = empresaId;

            modelBuilder.Entity<Empresa>().HasData(
                new Empresa
                {
                    Id = empresaId,
                    RazaoSocial = "Distribuidora XYZ Ltda",
                    NomeFantasia = "Distribuidora XYZ",
                    CNPJ = "12345678000190",
                    InscricaoEstadual = "123456789",
                    Email = "contato@distribuidoraxyz.com.br",
                    Telefone = "(11) 99999-9999",
                    DataCadastro = DateTime.UtcNow,
                    Ativo = true
                }
            );

            // Categorias B2B (materiais de construção)
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, TenantId = tenantId, Nome = "Cimentos", Descricao = "Cimentos e argamassas", Ativo = true },
                new Categoria { Id = 2, TenantId = tenantId, Nome = "Ferramentas", Descricao = "Ferramentas elétricas e manuais", Ativo = true },
                new Categoria { Id = 3, TenantId = tenantId, Nome = "Hidráulica", Descricao = "Tubos, conexões e registros", Ativo = true },
                new Categoria { Id = 4, TenantId = tenantId, Nome = "Elétrica", Descricao = "Fios, cabos e disjuntores", Ativo = true }
            );

            // Produtos B2B de demonstração
            modelBuilder.Entity<Produto>().HasData(
                new Produto
                {
                    Id = 1,
                    TenantId = tenantId,
                    Codigo = "CIM-001",
                    Nome = "Cimento Portland CP-II 50kg",
                    Descricao = "Cimento Portland CP-II-E-32 para uso geral",
                    Marca = "VotorantimCimentos",
                    UnidadeMedida = "SC",
                    Peso = 50,
                    QuantidadeEstoque = 500,
                    EstoqueMinimo = 100,
                    CategoriaId = 1,
                    Ativo = true
                },
                new Produto
                {
                    Id = 2,
                    TenantId = tenantId,
                    Codigo = "ARG-001",
                    Nome = "Argamassa ACIII 20kg",
                    Descricao = "Argamassa colante ACIII para assentamento de porcelanato",
                    Marca = "Quartzolit",
                    UnidadeMedida = "SC",
                    Peso = 20,
                    QuantidadeEstoque = 300,
                    EstoqueMinimo = 80,
                    CategoriaId = 1,
                    Ativo = true
                },
                new Produto
                {
                    Id = 3,
                    TenantId = tenantId,
                    Codigo = "FER-001",
                    Nome = "Furadeira Impacto 1/2\" 850W",
                    Descricao = "Furadeira de impacto profissional 850W com maleta",
                    Marca = "Bosch",
                    UnidadeMedida = "UN",
                    Peso = 2.5m,
                    QuantidadeEstoque = 50,
                    EstoqueMinimo = 10,
                    CategoriaId = 2,
                    Ativo = true
                }
            );
        }
    }
}
