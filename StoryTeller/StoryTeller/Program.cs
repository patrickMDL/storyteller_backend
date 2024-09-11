using Microsoft.EntityFrameworkCore;
using StoryTeller.AppDataContext;
using StoryTeller.Interface;
using StoryTeller.Models;
using StoryTeller.Services;
using StoryTellerAPI.Middleware;
using Microsoft.OpenApi.Models;
using System.Reflection;
using StoryTeller.SwaggerSchemaFilter;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços necessários à aplicação
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configura o Swagger com personalizações
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "StoryTeller API",
        Version = "v1",
        Description = "API que alimentará o StoryTeller, um site para jogar RPG de mesa usando o sistema Ordem Paranormal.",
        Contact = new OpenApiContact
        {
            Name = "Patrick Medeiros De Luca",
            Email = "patrick_mdl@outlook.com.com",
            Url = new Uri("https://github.com/patrickMDL")
        }
    });
    c.EnableAnnotations();
    c.SchemaFilter<UserSchemaFilter>();
    c.SchemaFilter<ClassAbilitySchemaFilter>();
    // Incluir comentários XML para gerar documentação automática
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Configurações do banco de dados e outras dependências
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddDbContext<StorytellerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));
builder.Services.AddProblemDetails();

builder.Services.AddLogging();
builder.Services.AddScoped<IUserServices, UserServices>();

var app = builder.Build();

// Criação de escopo para serviços
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider;
    // Possível uso do contexto ou inicialização de serviços
}

// Configuração do middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoryTeller API v1");
        c.RoutePrefix = string.Empty;  // Exibe o Swagger na raiz da aplicação
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
