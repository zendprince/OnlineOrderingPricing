using NUnit.Framework;
using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Testing.Pages.OODefault;
using OnlineOrdering.Testing.Pages.OOLogin;
using OnlineOrdering.Testing.Pages.OOProducPricing;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrdering.Testing.Script.OODefault
{
    public partial class FindProductPricing: BaseTestScript
    {
        OODefaultPage _defaultPage;  //hluong: Initial OODefaultPage
        OOLoginPage _login;  //hluong: Initial login page          
        OOProductPricingPage _productPricingPage; // //hluong: Initial Product pricing page 

        // Set up and initial this page
        [SetUp] //Pre-condition in test case 
        public void SetupTest()
        {
            _login = new OOLoginPage();
        }


        // The testcase run only 1 time
        [Test] // Steps in test case 
        public void Test()
        {
            // Step #1: Login 

            _login.txt_Username.SetText("hluong");
            _login.txt_Password.SetText("Hung123456");
            _login.btn_Login.Click();

            //Step #2: Enter model number then click FIND button. 
            _defaultPage = new OODefaultPage();
            _defaultPage.txt_productFindInput.SetText("A12A187DNB");
            _defaultPage.btn_Find.Click();

            // Step #3: Veirfy description and pricing
            _productPricingPage = new OOProductPricingPage();
            Assert.AreEqual(_productPricingPage.lbl_Each.Text, "Each");
            Assert.AreEqual(_productPricingPage.lbl_1.Text, "1");
            Assert.AreEqual(_productPricingPage.lbl_UnitNetPrice.Text, "148.6100");
            Assert.AreEqual(_productPricingPage.lbl_PkgNetPrice.Text, "148.61");

        }

        // After rune all test
        [TearDown]
        public void TearDownTest()
        {


        }

    }
}
