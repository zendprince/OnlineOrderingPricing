using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Common.Utils;
using System.Collections.Generic;
using System.Linq;

namespace OnlineOrdering.Common.Controls
{
    public class DropdownList : BaseControl
    {
        public DropdownList(IWebElement control) : base(control)
        {
            Select = new SelectElement(WrappedControl);
        }

        private SelectElement Select { get; set; }

        /// <summary>
        /// Select an item on Dropdown-List by text displayed
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="partialMatch"></param>
        public void SelectItem(string itemName, bool partialMatch = false)
        {
            Select.SelectByText(itemName, partialMatch);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(WrappedControl), string.Format("The page was selected item {0} with the left mouse dropdown list.", itemName));
        }

        /// <summary>
        /// Select an item on Dropdown-List by Index
        /// </summary>
        /// <param name="itemName"></param>
        public void SelectItem(int itemName)
        {
            Select.SelectByIndex(itemName);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(WrappedControl), string.Format("The page was selected item {0} with the left mouse dropdown list.", itemName));
        }

        /// <summary>
        /// Select an item on Dropdown-List by Value(ID)
        /// </summary>
        /// <param name="itemValue"></param>
        public void SelectItemByValue(string itemValue)
        {
            Select.SelectByValue(itemValue);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(WrappedControl), string.Format("The page was selected item {0} with the left mouse dropdown list.", itemValue));
        }

        /// <summary>
        /// Get selected item name 
        /// </summary>
        public string SelectedItemName
        {
            get { return Select.SelectedOption.Text; }
        }

        /// <summary>
        /// Get selected index
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                int.TryParse(Select.SelectedOption.GetAttribute("index"), out int index);
                return index;
            }
        }

        /// <summary>
        /// Get selected value
        /// </summary>
        public string SelectedValue
        {
            get { return Select.SelectedOption.GetAttribute("value"); }
        }

        /// <summary>
        /// Get total items on dropdown-list
        /// </summary>
        public int GetTotalItems
        {
            get { return Select.Options.Count(); }
        }

        /// <summary>
        /// Get all items from dropdown-list
        /// </summary>
        public IList<IWebElement> GetItems
        {
            get { return Select.Options; }
        }

        /// <summary>
        /// Get Selected Item
        /// </summary>
        public IWebElement SelectedItem
        {
            get { return Select.SelectedOption; }
        }

    }
}
