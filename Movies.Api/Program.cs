using Movies.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/*  NOTE: Why I don't do this: The business logic layer (Movies.Application) should be reusable
    (from Razor Pages, Blazor, etc.). The users/consumers don't have to know 
    the implementation details (interface name, appropriate lifetime, etc.)
    */
// builder.Services.AddSingleton<IMovieRepository, MovieRepository>();
// Instead, provide an extension method class on IServiceCollection
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

app.Run();
