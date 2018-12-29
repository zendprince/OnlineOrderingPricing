using OpenQA.Selenium;
using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Common.Enums;
using OnlineOrdering.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineOrdering.Common.Controls
{
    public class Grid
    {
        //total Row int _iRows = WrappedControl.FindElements(FindType.XPath, "./tbody/tr").Count;

        private IControls WrappedControl { get; set; }

        private string XPathGridLoad { get; set; }

        public Grid(IWebElement control, string xpathGridLoad)
        {
            WrappedControl = new IControls(control);
            XPathGridLoad = xpathGridLoad;
        }

    }
}
