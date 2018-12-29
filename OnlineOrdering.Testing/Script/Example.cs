using NUnit.Framework;
using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Testing.Pages.Login;
using System.IO;
using System.Reflection;

namespace OnlineOrdering.Testing.Script
{
    // Inherit the BaseTestScript
    [TestFixture]
    public class Example : BaseTestScript
    {
        readonly string pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\FileNameExcel.xlsx";

        // Define the page which you test
        private LoginPage _dashboardPage;

        // Set up and initial this page
        [SetUp]
        public void SetupTest()
        {
            _dashboardPage = new LoginPage(pathExcelFile);
        }


        #region"Test case"
        // The testcase run only 1 time
        [Test]
        [Category("Template"), Order(1)]
        public void Test1()
        {

        }

        [Test]
        [Category("Template"), Order(2)]
        public void Test2()
        {

        }

        [Test]
        [Category("Template"), Order(3)]
        public void Test3()
        {

        }
        [TestCase(1)]
        [TestCase(2)]
        [Category("Template"), Order(4)]
        public void TestDriven(int sequence)
        {
            // This testcase will rune 2 time and 
            // 1st the "sequence" = 1
            // 2nd the "sequence" = 2
        }
        #endregion

        // After rune all test
        [TearDown]
        public void TearDownTest()
        {

        }
    }
}
