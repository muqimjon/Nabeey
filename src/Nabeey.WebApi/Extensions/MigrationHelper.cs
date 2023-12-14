using Nabeey.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace InfoZest.WebApi.Extensions;

public static class MigrationHelper
{
    public static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
}
