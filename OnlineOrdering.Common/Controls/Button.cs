using OpenQA.Selenium;
using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Common.Utils;

namespace OnlineOrdering.Common.Controls
{
    public class Button : BaseControl
    {
        public Button(IWebElement control)
          : base(control)
        {
        }

        public void Click()
        {
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(WrappedControl), string.Format("The page was clicked with the left mouse button."));
            WrappedControl.Click();
        }

    }
}
