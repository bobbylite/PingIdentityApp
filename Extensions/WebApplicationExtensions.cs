using Microsoft.EntityFrameworkCore;
using GetChatty.Data;

namespace PingIdentityApp.Extensions;

public static class WebApplicationExtensions
{
    /// <summary>
    /// Run database migrations.
    /// </summary>
    /// <param name="webApplication"></param>
    /// <returns><see cref="WebApplication"/></returns>
    public static WebApplication RunDatabaseMigrations(this WebApplication webApplication)
    {
        ArgumentNullException.ThrowIfNull(webApplication);

        try
        {
            using var scope = webApplication.Services.CreateScope();
            var threadsDbContext = scope.ServiceProvider.GetRequiredService<GetChattyDataContext>();
            threadsDbContext.Database.Migrate();
            return webApplication;
        }
        catch (Exception ex)
        {
            var logger = webApplication.Services.GetRequiredService<ILogger<WebApplication>>();
            logger.LogError(ex, "An error occurred while migrating the database.");
            return webApplication;
        }
    }
}
