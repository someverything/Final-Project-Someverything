using Business.DependencyResolver;
using Core.DependencyResolver;
using WebAPI.Middlewears;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCoreService();
builder.Services.AddBussinessServices();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();
builder.Services.AddTransient<GlobalHandlingExeptionsMiddlewear>();

var app = builder.Build();

app.UseMiddleware<GlobalHandlingExeptionsMiddlewear>();

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
