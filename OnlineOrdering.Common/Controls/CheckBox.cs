using OpenQA.Selenium;
using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Common.Utils;

namespace OnlineOrdering.Common.Controls
{
    public class CheckBox : BaseControl
    {
        public CheckBox(IWebElement control) : base(control)
        {
        }

        public void Check()
        {
            if (!WrappedControl.Selected)
            {
                WrappedControl.Click();
            }
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(WrappedControl), string.Format("The page was checked with the left mouse checkbox."));
        }

        public void UnCheck()
        {
            if (WrappedControl.Selected)
            {
                WrappedControl.Click();
            }
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(WrappedControl), string.Format("The page was unchecked with the left mouse checkbox."));
        }

        public bool IsCheck
        {
            get
            {
                return WrappedControl.Selected;
            }
        }

    }
}
