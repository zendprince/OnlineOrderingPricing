using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Common.Controls;
using OnlineOrdering.Common.Enums;
using OnlineOrdering.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrdering.Testing.Pages.OODefault
{
    public partial class OODefaultPage: BasePage
    {
        //hluong: determine the text box which using to enter the model number on default page. 
        public Textbox txt_productFindInput {
            get {
                return new Textbox(FindElementHelper.FindElement(FindType.XPath, "//*[@id='productFindInput']"));
            }
        }

        //hluong: determine "FIND"n button on default page. 
        public Button btn_Find
        {
            get
            {
                return new Button(FindElementHelper.FindElement(FindType.XPath, "//*[@id='productFindBtn']"));
            }
        }


    }
}
