using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OnlineOrdering.Common.Enums;
using OnlineOrdering.Common.Utils;

namespace OnlineOrdering.Common.BaseClass
{
    public abstract class BaseControl
    {
        internal IWebElement WrappedControl { get; private set; }

        protected BaseControl(IWebElement control)
        {
            WrappedControl = control;
        }

        public bool Enabled
        {
            get
            {
                return WrappedControl.Enabled;
            }
        }

        public bool Displayed
        {
            get
            {
                return WrappedControl.Displayed;
            }
        }

        public string Text
        {
            get
            {
                return WrappedControl.Text;
            }
        }

        public string GetAttribute(string attributeName)
        {
            return WrappedControl.GetAttribute(attributeName);
        }

        public string GetAttribute(AttributeType attributeName)
        {
            return WrappedControl.GetAttribute(attributeName.ToString());
        }

        public void HoverMouse()
        {
            Actions action = new Actions(BaseValues.DriverSession);
            action.MoveToElement(WrappedControl).Perform();
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(WrappedControl), string.Format("The page was hovered on the button."));
        }

        public void JavaScriptClick()
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)BaseValues.DriverSession;
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(WrappedControl), string.Format("The page was clicked with the left mouse on button."));
            executor.ExecuteScript("arguments[0].click();", WrappedControl);
        }

        public void CoordinateClick()
        {
            Actions action = new Actions(BaseValues.DriverSession);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(WrappedControl), string.Format("The page was clicked with the left mouse on button."));
            action.MoveToElement(WrappedControl).Click().Build().Perform();
        }
    }
}
