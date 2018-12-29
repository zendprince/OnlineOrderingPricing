using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Common.Utils;
using OnlineOrdering.Common.Enums;
using OpenQA.Selenium;
using OnlineOrdering.Common.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineOrdering.Common.Page
{
    public abstract class ToolbarMenuSection : BasePage
    {
        protected ToolbarMenuSection() : base() { }



        public void FindModel(string valueName)
        {
            var _find = FindElementHelper.FindElement(FindType.XPath, "");
            Textbox Find_txt = new Textbox(_find);
        }


    }
  
}
