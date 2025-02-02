using Nabeey.Web.Extensions;
using Nabeey.Web.Middlewares;
using Nabeey.Service.Helpers;
using Nabeey.Service.Extensions;
using InfoZest.WebApi.Extensions;
using Nabeey.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.FullName); // Avtomatik skhemaID generatsiya qilish
});
builder.Services.AddSwaggerGenNewtonsoftSupport();

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

#region Custom services
// Migrate Database
app.MigrateDatabase();

// Initialize Accessor
HttpContextExtensions.InitAccessor(app);

// Path for Static Files
PathHelper.WebRootPath = Path.GetFullPath("wwwroot");
#endregion	

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

