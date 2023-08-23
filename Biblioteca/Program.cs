using Biblioteca.Context;
using Biblioteca.Repository;
using Biblioteca.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DataBase");

builder.Services.AddDbContext<BibliotecaContext>(options =>
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//builder.Services.AddDbContext<BibliotecaContext>(options =>
//options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
//assembly => assembly.MigrationsAssembly(typeof(BibliotecaContext).Assembly.FullName)));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Biblioteca",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Jackson",
            Email = "jacksonsilva_fsa@outlook.com",
            Url = new Uri("https://github.com/jacks0nsilva")
        }
    });

    var xmlFile = "Biblioteca.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    x.IncludeXmlComments(xmlPath);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ILivroRepository, LivroRepository>();


builder.Services.AddControllers().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
