using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Common.Enums;
using OnlineOrdering.Common.Utils;
using System;
using System.Collections.ObjectModel;

namespace OnlineOrdering.Common.Controls
{
    public class IControls : BaseControl
    {
        public IControls(IWebElement control) : base(control)
        {
        }

        public void Click()
        {
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(WrappedControl), string.Format("The page was clicked with the left mouse button."));
            WrappedControl.Click();
        }

        #region "Find Element in Element"
        public IWebElement FindElement(FindType findElementType, string valueToFind, int timeoutInSeconds = 10)
        {
            return FindElement(WrappedControl, findElementType, valueToFind, timeoutInSeconds);
        }

        public IWebElement FindElement(string findElementType, string valueToFind, int timeoutInSeconds = 10)
        {
            Enum.TryParse(findElementType, out FindType findType);
            return FindElement(WrappedControl, findType, valueToFind, timeoutInSeconds);
        }

        private IWebElement FindElement(IWebElement control, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(BaseValues.DriverSession, TimeSpan.FromSeconds(timeoutInSeconds));
                try
                {
                    return wait.Until(d =>
                    {
                        try { return control.FindElement(by); }
                        catch { return null; }
                    });
                }
                catch (WebDriverTimeoutException) { return null; }
                catch (TimeoutException) { return null; }
            }
            return WrappedControl.FindElement(by);
        }

        private IWebElement FindElement(IWebElement control, FindType findElementType, string valueToFind, int timeoutInSeconds = 10)
        {
            switch (findElementType)
            {
                case FindType.Name:
                    return FindElement(control, By.Name(valueToFind), timeoutInSeconds);
                case FindType.XPath:
                    return FindElement(control, By.XPath(valueToFind), timeoutInSeconds);
                case FindType.Id:
                    return FindElement(control, By.Id(valueToFind), timeoutInSeconds);
                case FindType.TagName:
                    return FindElement(control, By.TagName(valueToFind), timeoutInSeconds);
                default:
                    throw new NotSupportedException(string.Format("Find Element Type - {0} - is not supported for the method FindElement.", findElementType));
            }
        }

        #endregion

        #region "Find Elements in Element"
        public ReadOnlyCollection<IWebElement> FindElements(FindType findElementType, string valueToFind, int timeoutInSeconds = 10)
        {
            return FindElements(WrappedControl, findElementType, valueToFind, timeoutInSeconds);
        }

        public ReadOnlyCollection<IWebElement> FindElements(string findElementType, string valueToFind, int timeoutInSeconds = 10)
        {
            Enum.TryParse(findElementType, out FindType findType);
            return FindElements(WrappedControl, findType, valueToFind, timeoutInSeconds);
        }

        private ReadOnlyCollection<IWebElement> FindElements(IWebElement control, FindType findElementType, string valueToFind, int timeoutInSeconds = 10)
        {
            switch (findElementType)
            {
                case FindType.Name:
                    return FindElements(control, By.Name(valueToFind), timeoutInSeconds);
                case FindType.XPath:
                    return FindElements(control, By.XPath(valueToFind), timeoutInSeconds);
                case FindType.TagName:
                    return FindElements(control, By.TagName(valueToFind), timeoutInSeconds);
                case FindType.Id:
                    return FindElements(control, By.Id(valueToFind), timeoutInSeconds);
                default:
                    throw new NotSupportedException(string.Format("Find Element Type - {0} - is not supported for the method FindElements.", findElementType));
            }
        }

        private ReadOnlyCollection<IWebElement> FindElements(IWebElement control, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(BaseValues.DriverSession, TimeSpan.FromSeconds(timeoutInSeconds));
                try
                {
                    return wait.Until(drv => (control.FindElements(by).Count > 0) ? WrappedControl.FindElements(by) : null);
                }
                catch (WebDriverTimeoutException) { return null; }
                catch (TimeoutException) { return null; }
            }
            var elements = WrappedControl.FindElements(by);
            return elements;
        }

        #endregion


    }
}
