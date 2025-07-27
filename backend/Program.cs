using Microsoft.OpenApi.Models;
using Repository;
using Repository.Interfaces;
using Service;
using Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("*")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Controle de Gastos API", Version = "v1" });
});

builder.Services.AddSingleton<ITransactionRepository, InMemoryTransactionRepository>();
builder.Services.AddSingleton<IPersonRepository, InMemoryPersonRepository>();
builder.Services.AddSingleton<ICategoryRepository, InMemoryCategoryRepository>();  
builder.Services.AddScoped<IBalanceService, BalanceService>();
builder.Services.AddControllers();

var app = builder.Build();
app.UseCors("AllowFrontend");

app.UseSwagger();
app.UseSwaggerUI();



//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Backend rodando! Veja o Swagger em /swagger.");

app.Run();
