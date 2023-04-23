using Chapter.WebApi.Contexts;
using Chapter.WebApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ChapterContext, ChapterContext>();
builder.Services.AddControllers();
// configura o swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ChapterApi",
        Version
    = "v1"
    });
});
// Forma de autenticacão.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})

// Parâmetros de validacão do token.
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Valida quem está solicitando.
        ValidateIssuer = true,
        // Valida quem está recebendo.
        ValidateAudience = true,
        // Define se o tempo de expiração será validado.
        ValidateLifetime = true,
        // Criptografia e validação da chave de autenticacão.
        IssuerSigningKey = new
    SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapterapi-chaveautenticacao")),
        // Valida o tempo de expiração do token.
        ClockSkew = TimeSpan.FromMinutes(30),
        // Nome do issuer, da origem.
        ValidIssuer = "chapterapi.webapi",
        // Nome do audience, para o destino.
        ValidAudience = "chapterapi.webapi"
    };
});
builder.Services.AddTransient<LivroRepository, LivroRepository>();
builder.Services.AddTransient<UsuarioRepository, UsuarioRepository>();
var app = builder.Build();
app.UseDeveloperExceptionPage();
// ativa o middleware para uso do swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChapterApi v1"));
app.UseRouting();
// habilitar a autenticação
app.UseAuthentication();
// habilitar a auotirzação
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
