using Microsoft.Extensions.Configuration;
using Testcontainers.PostgreSql;
using Xunit.Abstractions;

namespace Edu.Orchestrator.Services.Test.WithDbContainer;

public abstract class PostgreSqlContainerPerTest(ITestOutputHelper outputHelper) : IAsyncLifetime
{
    // this is called for each test, since each test
    // instantiates a new class instance
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .Build();

    protected IConfiguration Configuration => new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnectionString"] = _container.GetConnectionString()
            })
            .Build();
    
    public Task InitializeAsync() => _container.StartAsync();
    
    public Task DisposeAsync() => _container.StopAsync();
}