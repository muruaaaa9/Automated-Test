using System.IO;
using Journey.Test.Support;
using NUnit.Framework;

namespace Journey.Test
{
    public class BaseJourneyTest
    {
        [SetUp]
        public void BaseSetUp()
        {
            Browser.Current.Reset();
        }

        [TearDown]
        public void BaseTearDown()
        {
            if (TestContext.CurrentContext.Result.Status == TestStatus.Failed)
            {
                var testName = TestContext.CurrentContext.Test.Name;
                var imageName = Path.Combine(TestContext.CurrentContext.TestDirectory, string.Format("{0}.jpg", testName));
                Browser.Current.CaptureScreenShot(imageName);
            }
        }
    }
}
