using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OnlineOrdering.Common.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OnlineOrdering.Common.Utils
{
    public static class CommonHelper
    {
        /// <summary>
        /// Create list
        /// </summary>
        /// <typeparam name="Cast list elements to list controls which define on this framework"></typeparam>
        /// <param name="List Elements"></param>
        /// <returns></returns>
        public static List<T> CreateList<T>(ReadOnlyCollection<IWebElement> elements) where T : BaseControl
        {
            var result = new List<T>();
            result.AddRange(elements.Select(e => (T)Activator.CreateInstance(typeof(T), e)));
            return result;
        }

        /// <summary>
        /// Compare 2 list string
        /// </summary>
        /// <param name="list string 1"></param>
        /// <param name="list string 2"></param>
        /// <returns></returns>
        public static bool IsEqual2List(IList<string> list1, IList<string> list2)
        {
            bool isEqual = list1.Count == list2.Count && !list1.Except(list2).Any();
            return isEqual;
        }

        /// <summary>
        /// Cast list controls to list string
        /// </summary>
        /// <param name="listControls"></param>
        /// <returns>List text of elements</returns>
        public static List<string> CastListControlsToListString(List<IWebElement> listItem)
        {
            List<string> list = new List<string>();
            foreach (var item in listItem)
                list.Add(item.Text);
            return list;
        }

        /// <summary>
        /// Click Element by position
        /// </summary>
        /// <param name="element"></param>
        public static void CoordinateClick(IWebElement element)
        {
            Actions action = new Actions(BaseValues.DriverSession);
            action.MoveToElement(element).Click().Build().Perform();
        }

        /// <summary>
        /// Click on the element by javascript
        /// </summary>
        /// <param name="control"></param>
        public static void JavaScriptClick(IWebElement control)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)BaseValues.DriverSession;
            executor.ExecuteScript("arguments[0].click();", control);
        }

        /// <summary>
        /// Cast list string to string
        /// </summary>
        /// <param name="itemList"></param>
        /// <returns></returns>
        public static string CastListToString(IList<string> itemList)
        {
            return String.Join(";", itemList);
        }

        /// <summary>
        /// Cast string value to List<string>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IList<string> CastValueToList(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            var paths = value.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            return paths.ToList();
        }

        /// <summary>
        /// Capture screeshot with base64 img
        /// </summary>
        public static string CaptureScreen(IWebElement iControl = null)
        {
            HighLightElement(iControl);

            ITakesScreenshot ts = (ITakesScreenshot)BaseValues.DriverSession;
            Screenshot screenshot = ts.GetScreenshot();

            string screenshotBase64 = screenshot.AsBase64EncodedString;

            bool exists = Directory.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ErrorScreenShots\\" + DateTime.Now.ToString("ddd-MMM-yyyy"));
            if (!exists)
                Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ErrorScreenShots\\" + DateTime.Now.ToString("ddd-MMM-yyyy"));

            string finalpth = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ErrorScreenShots\\" + DateTime.Now.ToString("ddd-MMM-yyyy") + "\\" + DateTime.Now.ToString("hh-mm-ss") + ".png";

            string localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);

            RemoveHighLightElement(iControl);
            if (BaseValues.SaveImageAsBase64)
                return screenshotBase64;
            return finalpth;
        }

        private static void HighLightElement(IWebElement iControl = null)
        {
            if (iControl != null)
            {
                IJavaScriptExecutor js = BaseValues.DriverSession as IJavaScriptExecutor;
                js.ExecuteScript("arguments[0].scrollIntoView({block: 'end'});", iControl);
                js.ExecuteScript("arguments[0].style='border: 2px solid red;'", iControl);
            }
        }

        private static void RemoveHighLightElement(IWebElement iControl = null)
        {
            if (iControl != null)
            {
                IJavaScriptExecutor js = BaseValues.DriverSession as IJavaScriptExecutor;
                js.ExecuteScript("arguments[0].style=''", iControl);
            }
        }
    }
}