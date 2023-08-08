using controle_de_permissoes.Models.DB;
using controle_de_permissoes.Models.Repositories.Impl;
using controle_de_permissoes.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<BancoContext>(item => item.UseSqlServer(configuration.GetConnectionString("Database")));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<PerfilPermissaoRepository, PerfilPermissaoRepositoryImpl>();
builder.Services.AddScoped<PerfilRepository, PerfilRepositoryImpl>();
builder.Services.AddScoped<PermissaoRepository, PermissaoRepositoryImpl>();
builder.Services.AddScoped<SistemaRepository, SistemaRepositoryImpl>();
builder.Services.AddScoped<UsuarioPermissaoRepository, UsuarioPermissaoRepositoryImpl>();
builder.Services.AddScoped<UsuarioRepository, UsuarioRepositoryImpl>();
builder.Services.AddSession(o => {
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});

builder.Configuration.AddJsonFile("appsettings.json", optional: false);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
