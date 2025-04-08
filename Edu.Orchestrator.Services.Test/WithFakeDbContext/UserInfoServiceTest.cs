using DbContext;

namespace Edu.Orchestrator.Services.Test.WithFakeDbContext;

public class UserInfoServiceTest
{
    [Fact]
    public async Task Get_User_Info_By_Id()
    {
        var ctx = new FakeDbContext();

        await ctx.PopulateTestDataAsync();

        var userInfoService = new UserInfoService(ctx);

        var result = await userInfoService.GetByIdAsync(2);
    }
}