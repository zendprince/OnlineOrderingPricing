using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OnlineOrdering.Common.Enums;
using OnlineOrdering.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineOrdering.Common.BaseClass
{
    public abstract class BasePage
    {
        protected ExcelFactory ExcelHelper { get; set; }

        protected BasePage()
        {
            PageLoad();
        }

        /// <summary>
        /// The timeout of pages can be overridden for especially purpose
        /// </summary>
        /// <param name="iSeconds"></param>
        // Wait until this page load completed (all elements on DOM is ready state)
        public static void PageLoad(int iSeconds = 60)
        {
            if (iSeconds != BaseValues.PageloadTimeouts) iSeconds = BaseValues.PageloadTimeouts;
            var wait = new WebDriverWait(BaseValues.DriverSession, TimeSpan.FromSeconds(iSeconds));
            wait.Until((x) =>
                {
                    return ((IJavaScriptExecutor)BaseValues.DriverSession).ExecuteScript("return document.readyState").Equals("complete");
                });
        }

        /// <summary>
        /// Page waiting until all jQuery is completed loading
        /// </summary>
        /// <param name="Timeout(seconds)"></param>
        public static void JQueryLoad(int iSeconds = 60)
        {
            if (iSeconds != BaseValues.PageloadTimeouts) iSeconds = BaseValues.PageloadTimeouts;
            try
            {
                var wait = new WebDriverWait(BaseValues.DriverSession, TimeSpan.FromSeconds(iSeconds));
                wait.Until((d) =>
                {
                    Boolean isJqueryCallDone = (Boolean)((IJavaScriptExecutor)BaseValues.DriverSession).ExecuteScript("return jQuery.active==0");
                    if (!isJqueryCallDone) Console.WriteLine("JQuery call is in Progress");
                    return isJqueryCallDone;
                });
            }
            // If it have any unhandle alert displayed, throw new exception and break the test
            catch (UnhandledAlertException)
            {
                throw new UnhandledAlertException(string.Format("An unexpected alert is displayed on your page, the test is stopped. The alert is displayed with the message: \"{0}\"", BaseValues.DriverSession.SwitchTo().Alert().Text));
            }

        }


        /// <summary>
        /// Get all controls with type toast message
        /// </summary>
        /// <returns>Return list control toast message</returns>
        public IList<IWebElement> GetListMessageControls()
        {
            var _listToastContent = FindElementHelper.FindElements(FindType.XPath, "/html/body/div[@class='toast-container toast-position-top-center']/div[@class = 'toast-item-wrapper']");
            return _listToastContent;
        }

        /// <summary>
        /// Get lastest toast message control on current page
        /// </summary>
        public IWebElement GetLastestMessageControl
        {
            get { return FindElementHelper.FindElement(FindType.XPath, "/html/body/div[@class='toast-container toast-position-top-center']/div[@class = 'toast-item-wrapper' and position()=last()]"); }
        }

        /// <summary>
        /// Get latest message
        /// </summary>
        public string GetLastestMessage
        {
            get { return GetLastestMessageControl.Text; }
        }

        /// <summary>
        /// Get list message of Toast Message
        /// </summary>
        /// <returns>List message</returns>
        public IList<string> GetListMessage()
        {
            IList<string> listMessage = new List<string>();
            var _listContent = FindElementHelper.FindElement(FindType.XPath, "/html/body/div[@class='toast-container toast-position-top-center'])");
            // Get the latest toast messsage
            if (_listContent != null)
            {
                var ToastMessage = _listContent.FindElements(By.XPath("/html/body/div[@class='toast-container toast-position-top-center']/div[@class = 'toast-item-wrapper']/div/p"));
                foreach (var item in ToastMessage)
                {
                    listMessage.Add(item.Text);
                }
                return listMessage;
            }
            // if the ToastMessage is not exist on this page, return null
            return null;
        }

        /// <summary>
        /// Use this function to handle dialog on the pages. 
        /// </summary>
        /// <param name="Action Name (OK/Cancel)"></param>
        public static void ConfirmDialog(ConfirmType confirm)
        {
            switch (confirm)
            {
                case ConfirmType.OK:
                    {
                        try
                        {
                            BaseValues.DriverSession.SwitchTo().Alert().Accept();
                        }
                        catch (NoAlertPresentException)
                        {
                            throw new NoAlertPresentException(string.Format("Could not find any alert dialog on your browser"));
                        }
                        PageLoad();
                        break;
                    }
                default: // Cancel to close the dialog
                    try
                    {
                        BaseValues.DriverSession.SwitchTo().Alert().Dismiss();
                    }
                    catch (NoAlertPresentException)
                    {
                        throw new NoAlertPresentException(string.Format("Could not find any alert dialog on your browser"));
                    }
                    break;
            }
            // Back to main windows
            BaseValues.DriverSession.SwitchTo().Window(BaseValues.DriverSession.WindowHandles.First());
        }

        /// <summary>
        /// Get title of current Page
        /// </summary>
        public string Title => BaseValues.DriverSession.Title;

        /// <summary>
        /// Get curent URL of page
        /// </summary>
        public string CurrentURL => BaseValues.DriverSession.Url;
    }
}
