using Business.DependencyResolver;
using Core.DependencyResolver;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;
using WebAPI.Middlewears;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCoreService();
builder.Services.AddBussinessServices();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddLocalization(options => options.ResourcesPath = "Resource");


var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("ru-RU"),
    // Add other cultures as needed
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("az");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
            expires != null ? expires > DateTime.UtcNow : false,
    };
});

builder.Services.AddLogging();
builder.Services.AddTransient<GlobalHandlingExeptionsMiddlewear>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<GlobalHandlingExeptionsMiddlewear>();

app.UseHttpsRedirection();

app.UseRequestLocalization();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
