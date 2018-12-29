using LinqToExcel;
using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Common.Controls;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OnlineOrdering.Common.Utils
{
    public class CommonFuncs
    {
        public static void LoginToOnlineOrdering(string userName, string password)
        {
            string pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\LoginParams.xlsx";

            LoginPage _loginPage = new LoginPage(pathExcelFile);
            _loginPage.LoginWithUser(userName)
                      .LoginWithPassword(password)
                      .SignIn();
        }

        private class LoginPage : BasePage
        {
            internal LoginPage(string pathOfExcelFile) : base()
            {
                ExcelHelper = new ExcelFactory(pathOfExcelFile);

                // Sheet contains repository of Dashboard
                MetaData = ExcelHelper.GetAllRows("Login_OR");
            }
            static IQueryable<Row> MetaData { get; set; }

            Row _title { get { return ExcelFactory.GetRow(MetaData, 1); } }
            Label Title_lbl => new Label(FindElementHelper.FindElement(_title));

            Row _userName { get { return ExcelFactory.GetRow(MetaData, 2); } }
            Textbox UserName_txt => new Textbox(FindElementHelper.FindElement(_userName));

            Row _password { get { return ExcelFactory.GetRow(MetaData, 3); } }
            Textbox Password_txt => new Textbox(FindElementHelper.FindElement(_password));

            Row _signIn { get { return ExcelFactory.GetRow(MetaData, 4); } }
            Button Login_btn => new Button(FindElementHelper.FindElement(_signIn));

            internal LoginPage LoginWithUser(string userName)
            {
                UserName_txt.WrappedControl.SendKeys(userName);
                return this;
            }

            internal LoginPage LoginWithPassword(string pwd)
            {
                Password_txt.WrappedControl.SendKeys(pwd);
                return this;
            }

            internal LoginPage SignIn()
            {
                Login_btn.WrappedControl.Click();
                return this;
            }
        }
    }
}
