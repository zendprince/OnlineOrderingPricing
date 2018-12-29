using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Common.Controls;
using OnlineOrdering.Common.Enums;
using OnlineOrdering.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineOrdering.Testing.Pages.OOProducPricing
{
    public partial class OOProductPricingPage: BasePage
    {
        //hluong: get label Each in Description 
        public Label lbl_Each
        {
            get
            {
                return new Label(FindElementHelper.FindElement(FindType.XPath, "//*[@id='prcTable']/tbody/tr[1]/td[1]"));
            }
        }

        //hluong: get quaity(Each)
        public Label lbl_1
        {
            get
            {
                return new Label(FindElementHelper.FindElement(FindType.XPath, "//*[@id='prcTable']/tbody/tr[1]/td[2]"));
            }
        }

        //hluong: get Net Price per Unit 
        public Label lbl_UnitNetPrice
        {
            get
            {
                return new Label(FindElementHelper.FindElement(FindType.XPath, "//*[@id='prcTable']/tbody/tr[1]/td[5]"));
            }
        }

        //hluong: get Package Net Price 
        public Label lbl_PkgNetPrice
        {
            get
            {
                return new Label(FindElementHelper.FindElement(FindType.XPath, "//*[@id='prcTable']/tbody/tr[1]/td[6]"));
            }
        }




    }
}
