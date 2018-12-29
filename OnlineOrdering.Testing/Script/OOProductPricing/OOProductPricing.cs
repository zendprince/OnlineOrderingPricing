using NUnit.Framework;
using OnlineOrdering.Common.BaseClass;
using OnlineOrdering.Testing.Pages.OOProducPricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrdering.Testing.Script.OOProductPricing
{
    class OOProductPricing: BaseTestScript
    {
        OOProductPricingPage _productPricingPage;
        // Set up and initial this page
        [SetUp] //Pre-condition in test case 
        public void SetupTest()
        {
            _productPricingPage = new OOProductPricingPage();
        }


        // The testcase run only 1 time
        [Test] // Steps in test case 
        public void VerifyProductPricing()
        {
            //Verify Description : Each 
            Assert.AreEqual(_productPricingPage.lbl_Each, "Each");
            Assert.AreEqual(_productPricingPage.lbl_1, "1");
            Assert.AreEqual(_productPricingPage.lbl_UnitNetPrice, "148.6100");
            Assert.AreEqual(_productPricingPage.lbl_PkgNetPrice, "148.61");

        }


        // After rune all test
        [TearDown]
        public void TearDownTest()
        {


        }
    }
}
