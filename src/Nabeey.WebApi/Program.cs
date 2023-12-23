using Nabeey.Web.Extensions;
using Nabeey.Web.Middlewares;
using Nabeey.Service.Helpers;
using Nabeey.Service.Extensions;
using InfoZest.WebApi.Extensions;
using Nabeey.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Nabeey.Service.Interfaces;
using Nabeey.Domain.Entities.Users;
using Nabeey.Service.DTOs.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Authorization
builder.Services.ConfigureSwagger();
// Lowercase routing

builder.Services.AddControllers(options =>
{
	options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
});

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddServices();

builder.Services.AddControllersWithViews()
	.AddNewtonsoftJson(options =>
	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

// JWT
builder.Services.AddJwt(builder.Configuration);

var app = builder.Build();

app.MigrateDatabase();

// Get Accessor
HttpContextExtensions.InitAccessor(app);

PathHelper.WebRootPath = Path.GetFullPath("wwwroot");

//var userService = app.Services.GetRequiredService<IUserService>();
//var adminInfo = app.Configuration.GetSection("AdminInfo").Get<UserCreationDto>();
//await userService.AddAsync(adminInfo);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.UseAuthentication();

app.UseStaticFiles();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

