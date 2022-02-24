using NUnit.Framework;
using System.Threading.Tasks;

namespace Mc2.CrudTest.AcceptanceTests
{

    using static Testing;

    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}