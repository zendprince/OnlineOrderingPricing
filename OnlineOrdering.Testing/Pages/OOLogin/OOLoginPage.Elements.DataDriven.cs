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
using LinqToExcel;
using OnlineOrdering.Common.Constants;


namespace OnlineOrdering.Testing.Pages.OOLogin
{
    public partial class OOLoginPageDataDriven : BasePage
    {
        public OOLoginPageDataDriven(string pathOfExcelFile) : base()
        {
            ExcelHelper = new ExcelFactory(pathOfExcelFile);
            Login_OR_Sheet = ExcelHelper.GetAllRows("Login_OR");
        }

        private static IQueryable<Row> Login_OR_Sheet { get; set; }
        private Row _title = ExcelFactory.GetRow(Login_OR_Sheet, 1);
        public Label lbel_Title_a => new Label(FindElementHelper.FindElement(_title[BaseConstants.FindType], _title[BaseConstants.ValueToFind]));


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
