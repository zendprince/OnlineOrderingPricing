using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OnlineOrdering.Common.Utils;

namespace OnlineOrdering.Common.BaseClass
{
    [TestFixture]
    public abstract class BaseTestScript : InitialSetting
    {
        public string Log;

        [SetUp]
        public void BaseSetUp()
        {
            Log = string.Empty;

            // Create feature
            ExtentReportsHelper.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        // [Test] method

        [TearDown]
        public void BaseTearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message)
                    ? ""
                    : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.Message);
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            ExtentReportsHelper.GetTest().Log(logstatus, "Test ended with " + logstatus + stacktrace);
            if (logstatus == Status.Fail)
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen());

            // Close all unhandle alert if it display on Pages
            try
            {
                // Close all alert
                BaseValues.DriverSession.SwitchTo().Alert().Dismiss();
                // Close all modal
                new Actions(BaseValues.DriverSession).SendKeys(Keys.Escape);
            }
            catch (NoAlertPresentException) { }

            // Log out if need
        }
    }
}
