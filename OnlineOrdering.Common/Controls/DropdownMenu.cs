using OpenQA.Selenium;
using OnlineOrdering.Common.Enums;
using System;

namespace OnlineOrdering.Common.Controls
{
    class DropdownMenu : IControls
    {
        public DropdownMenu(IWebElement control) : base(control)
        {
        }

        /// <summary>
        /// Select item on menu by name
        /// </summary>
        /// <param name="itemName"></param>
        public void SelectItem(string itemName)
        {
            this.Click();
            IControls control = new IControls(this.FindElement(FindType.XPath, "./../ul/li/a[text()='" + itemName + "']"));
            if (control is null)
                throw new NoSuchElementException(string.Format("Could not found element with name \"{0}\" on you Dropdown Menu", itemName));
            control.Click();
        }

        /// <summary>
        /// Select item on dropdown menu by index, the start number is 0
        /// </summary>
        /// <param name="itemIndex"></param>
        public void SelectItem(int itemIndex)
        {
            int iPosition = itemIndex + 1;
            this.Click();
            IControls controls = new IControls(this.FindElement(FindType.XPath, "./../ul/li/a[" + iPosition + "]"));
            if (controls is null)
                throw new IndexOutOfRangeException(string.Format("Could not found element with index \"{0}\" on you Dropdown Menu", itemIndex));
            controls.Click();
        }

    }
}
