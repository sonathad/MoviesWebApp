using System.Reflection;
using Microsoft.OpenApi.Models;
using Movies.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Movies API", 
        Description = "An API for interacting with a movie database.", 
        Version = "1.0"
    });
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    
    options.IncludeXmlComments(xmlPath);
});


/*  NOTE: Why I don't do this: The business logic layer (Movies.Application) should be reusable
    (from Razor Pages, Blazor, etc.). The users/consumers don't have to know 
    the implementation details (interface name, appropriate lifetime, etc.)
    */
// builder.Services.AddSingleton<IMovieRepository, MovieRepository>();

// Instead, I'm providing an extension method class on IServiceCollection
// with a clear, descriptive name
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
