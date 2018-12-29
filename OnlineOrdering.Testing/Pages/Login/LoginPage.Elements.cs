using LinqToExcel;
using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Common.Controls;
using OnlineOrdering.Common.Pages;
using OnlineOrdering.Common.Utils;
using System.Linq;

namespace OnlineOrdering.Testing.Pages.Login
{
    public partial class LoginPage : BasePage
    {
        public LoginPage(string pathOfExcelFile) : base()
        {
            ExcelHelper = new ExcelFactory(pathOfExcelFile);

            // Sheet contains repository of Login page. 
            MetaData = ExcelHelper.GetAllRows("Login_OR");
        }

        private static IQueryable<Row> MetaData { get; set; }

        private Row _title { get { return ExcelFactory.GetRow(MetaData, 1); } }
        public Label Title_lbl => new Label(FindElementHelper.FindElement(_title));

        private Row _userName { get { return ExcelFactory.GetRow(MetaData, 2); } }
        public Textbox UserName_txt => new Textbox(FindElementHelper.FindElement(_userName));

        private Row _password { get { return ExcelFactory.GetRow(MetaData, 3); } }
        public Textbox Password_txt => new Textbox(FindElementHelper.FindElement(_password));

        private Row _signIn { get { return ExcelFactory.GetRow(MetaData, 4); } }
        public Button Login_btn => new Button(FindElementHelper.FindElement(_signIn));
    }
}
