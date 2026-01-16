using LojaProdutosCurso.Data;
using LojaProdutosCurso.Services.Autenticacao;
using LojaProdutosCurso.Services.Categoria;
using LojaProdutosCurso.Services.Estoque;
using LojaProdutosCurso.Services.Produto;
using LojaProdutosCurso.Services.Sessao;
using LojaProdutosCurso.Services.Usuario;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// HttpContextAccessor para multi-tenancy
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Services antigos (manter temporariamente)
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IProdutoInterface, ProdutoService>();
builder.Services.AddScoped<ICategoriaInterface, CategoriaService>();
builder.Services.AddScoped<IEstoqueInterface, EstoqueService>();
builder.Services.AddScoped<IUsuariointerface, UsuarioService>();
builder.Services.AddScoped<IAutenticacaoInterface, AutententicacaoServices>();
builder.Services.AddScoped<ISessaoInterface, SessaoService>();

// TODO: Adicionar novos services B2B aqui nos próximos passos

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expiração da sessão
    options.Cookie.HttpOnly = true; // Impede acesso ao cookie via JavaScript
    options.Cookie.IsEssential = true; // Garante que o cookie seja enviado mesmo se o usuário não consentir
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); // Adiciona o middleware de sessão
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
