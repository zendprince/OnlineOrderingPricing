using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Common.Controls;
using OnlineOrdering.Common.Enums;
using OnlineOrdering.Common.Pages;
using OnlineOrdering.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrdering.Testing.Pages.OOLogin
{
    public partial class OOLoginPage: BasePage 
    {
        public Label lbel_Title => new Label(FindElementHelper.FindElement(FindType.XPath, "//*[@id='sstwrapper']/div/form/div[1]/header/h2/span"));
        public Textbox txt_Username => new Textbox(FindElementHelper.FindElement(FindType.XPath, "//*[@id='User']"));
        public Textbox txt_Password => new Textbox(FindElementHelper.FindElement(FindType.XPath, "//*[@id='Password']"));
        public Button btn_Login
        {
            get
            {
                return new Button(FindElementHelper.FindElement(FindType.XPath, "//*[@id='login']"));
            }
            
        }

    }
}
