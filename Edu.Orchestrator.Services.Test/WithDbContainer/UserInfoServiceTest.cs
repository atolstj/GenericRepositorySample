using Edu.Orchestrator.Services.DbContext;
using Xunit.Abstractions;

namespace Edu.Orchestrator.Services.Test.WithDbContainer;

public class UserInfoServiceTest(ITestOutputHelper outputHelper) 
    : PostgreSqlContainerPerTest(outputHelper)
{
    [Fact]
    public async Task Get_User_Info_By_Id()
    {
        var ctx = new OrchestratorDbContext(Configuration);
        
        await ctx.Database.EnsureCreatedAsync();

        await ctx.PopulateTestDataAsync();

        var userInfoService = new UserInfoService(ctx);

        var result = await userInfoService.GetByIdAsync(2);
    }
}