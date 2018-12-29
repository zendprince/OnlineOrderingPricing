using OpenQA.Selenium;
using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Common.Enums;
using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;
using LinqToExcel;
using OnlineOrdering.Common.Constants;

namespace OnlineOrdering.Common.Utils
{
    public static class FindElementHelper
    {
        #region "For this project only"
        /// <summary>
        /// Using "Row" to find element with "Row" contains 2 column "Field Find Type" and "Value To Find"
        /// </summary>
        /// <param name="row"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public static IWebElement FindElement(Row row, int timeoutInSeconds = 10)
        {
            return FindElement(row[BaseConstants.FindType], row[BaseConstants.ValueToFind], timeoutInSeconds);
        }

        /// <summary>
        /// Using "Row" to find elements with "Row" contains 2 column "Field Find Type" and "Value To Find"
        /// </summary>
        /// <param name="row"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public static ReadOnlyCollection<IWebElement> FindElements(Row row, int timeoutInSeconds = 10)
        {
            return FindElements(row[BaseConstants.FindType], row[BaseConstants.ValueToFind], timeoutInSeconds);
        }
        #endregion

        public static IWebElement FindElement(FindType findElementType, string valueToFind, int timeoutInSeconds = 10)
        {
            return FindElement(BaseValues.DriverSession, findElementType, valueToFind, timeoutInSeconds);
        }

        public static IWebElement FindElement(string findElementType, string valueToFind, int timeoutInSeconds = 10)
        {
            Enum.TryParse(findElementType, out FindType findType);
            return FindElement(BaseValues.DriverSession, findType, valueToFind, timeoutInSeconds);
        }

        public static ReadOnlyCollection<IWebElement> FindElements(FindType findElementType, string valueToFind, int timeoutInSeconds = 10)
        {
            return FindElements(BaseValues.DriverSession, findElementType, valueToFind, timeoutInSeconds);
        }

        public static ReadOnlyCollection<IWebElement> FindElements(string findElementType, string valueToFind, int timeoutInSeconds = 10)
        {
            Enum.TryParse(findElementType, out FindType findType);
            return FindElements(BaseValues.DriverSession, findType, valueToFind, timeoutInSeconds);
        }


        #region Internals
        internal static IWebElement FindElement(IWebDriver driver, FindType findElementType, string valueToFind, int timeoutInSeconds)
        {
            switch (findElementType)
            {
                case FindType.Name:
                    return FindElement(driver, By.Name(valueToFind), timeoutInSeconds);
                case FindType.XPath:
                    return FindElement(driver, By.XPath(valueToFind), timeoutInSeconds);
                case FindType.Id:
                    return FindElement(driver, By.Id(valueToFind), timeoutInSeconds);
                case FindType.TagName:
                    return FindElement(driver, By.TagName(valueToFind), timeoutInSeconds);
                default:
                    throw new NotSupportedException(string.Format("Find Element Type - {0} - is not supported for the method FindElement.", findElementType));
            }
        }

        internal static ReadOnlyCollection<IWebElement> FindElements(IWebDriver driver, FindType findElementType, string valueToFind, int timeoutInSeconds)
        {
            switch (findElementType)
            {
                case FindType.Name:
                    return FindElements(driver, By.Name(valueToFind), timeoutInSeconds);
                case FindType.XPath:
                    return FindElements(driver, By.XPath(valueToFind), timeoutInSeconds);
                case FindType.TagName:
                    return FindElements(driver, By.TagName(valueToFind), timeoutInSeconds);
                case FindType.Id:
                    return FindElements(driver, By.Id(valueToFind), timeoutInSeconds);
                default:
                    throw new NotSupportedException(string.Format("Find Element Type - {0} - is not supported for the method FindElements.", findElementType));
            }
        }


        #endregion


        #region "Privates"
        private static IWebElement FindElement(IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                try
                {
                    return wait.Until(d =>
                    {
                        try { return d.FindElement(by); }
                        catch { return null; }
                    });
                }
                catch (WebDriverTimeoutException) { return null; }
                catch (TimeoutException) { return null; }
            }
            return driver.FindElement(by);
        }

        private static ReadOnlyCollection<IWebElement> FindElements(IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                try
                {
                    return wait.Until(drv => (drv.FindElements(by).Count > 0) ? drv.FindElements(by) : null);
                }
                catch (WebDriverTimeoutException) { return null; }
                catch (TimeoutException) { return null; }
            }
            var elements = driver.FindElements(by);
            return elements;
        }

        #endregion
    }
}
