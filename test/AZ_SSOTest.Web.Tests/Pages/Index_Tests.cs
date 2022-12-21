using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace AZ_SSOTest.Pages;

public class Index_Tests : AZ_SSOTestWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
