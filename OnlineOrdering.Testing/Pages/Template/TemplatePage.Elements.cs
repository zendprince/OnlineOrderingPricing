using OnlineOrdering.Common.Controls;
using OnlineOrdering.Common.Enums;
using OnlineOrdering.Common.Pages;
using OnlineOrdering.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrdering.Testing.Template.Pages
{
    public partial class TemplatePage : NormalPage
    {
        public TemplatePage() : base()
        {
        }

        public Label Template_lbl => new Label(FindElementHelper.FindElement(FindType.XPath, "//*[@id='sstwrapper']/div/form/div[1]/header/h2/span"));
        public Button Template_btn => new Button(FindElementHelper.FindElement(FindType.XPath, "//*[@id='User']"));
        public Textbox Template_txt => new Textbox(FindElementHelper.FindElement(FindType.XPath, "//*[@id='Password']"));
    }
}
