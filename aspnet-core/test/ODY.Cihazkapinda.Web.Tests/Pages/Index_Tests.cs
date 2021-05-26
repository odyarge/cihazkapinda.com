using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace ODY.Cihazkapinda.Pages
{
    public class Index_Tests : CihazkapindaWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
