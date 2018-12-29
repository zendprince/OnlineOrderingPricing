using OpenQA.Selenium;
using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Common.Utils;

namespace OnlineOrdering.Common.Controls
{
    public class Textbox : BaseControl
    {
        public Textbox(IWebElement control)
          : base(control)
        {
        }

        public void SendKeys(string text)
        {
            WrappedControl.Clear();
            WrappedControl.SendKeys(text);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(WrappedControl), string.Format("The text '{0}' was entered in the text editor.", text));
        }

        public void Clear()
        {
            WrappedControl.Clear();
        }

        public void SetText(string text)
        {
            WrappedControl.Clear();
            WrappedControl.SendKeys(text);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(WrappedControl), string.Format("The text '{0}' was entered in the text editor.", text));
        }
    }
}
