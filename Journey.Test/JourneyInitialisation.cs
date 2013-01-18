using Journey.Test.Support;
using Journey.Test.Support.Web;
using NUnit.Framework;

namespace Journey.Test
{
    [SetUpFixture]
    public class JourneyInitialisation
    {
        [SetUp]
        public void SetUp()
        {
            Browser.Initialise(WebDriverFactory.GetWebdriver());
        }

        [TearDown]
        public void TearDown()
        {
            Browser.Close();
        }
    }
}