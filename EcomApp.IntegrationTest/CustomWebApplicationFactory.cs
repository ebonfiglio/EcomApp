using EcomApp.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");

        builder.ConfigureServices(services =>
        {
            // Find and remove the real database context registration if necessary
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<EcomAppDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add a database context for testing
            services.AddDbContext<EcomAppDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            // Additional service configuration for testing

            // Build the service provider and create a scope
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<EcomAppDbContext>();
                var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory>>();

                // Ensure the database is created
                db.Database.EnsureCreated();

                try
                {
                    // Seed the database
                    SeedDatabase(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the test database. Error: {Message}", ex.Message);
                }
            }
        });
    }

    private void SeedDatabase(EcomAppDbContext db)
    {
        // Implement database seeding logic
    }
}
