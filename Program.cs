using Chapter.WebApi.Contexts;
using Chapter.WebApi.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ChapterContext, ChapterContext>();
builder.Services.AddControllers();
builder.Services.AddTransient<LivroRepository, LivroRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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

var app = builder.Build();

app.UseDeveloperExceptionPage();
// ativa o middleware para uso do swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChapterApi v1"));

app.UseRouting();

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
});

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run();
