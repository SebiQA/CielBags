using CielBags.PageModels;
using CielBags.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CielBags.Tests.AccountEdits
{
    class passwordResetTests : BaseTest
    {

        string url = FrameworkConstants.GetUrl();

        private static IEnumerable<TestCaseData> GetCredentialsDataCsvReset()
        {
            foreach (var values in Utils.GetGenericData("TestData\\resetData.csv"))
            {
                yield return new TestCaseData(values);
            }
        }
        [Test, TestCaseSource("GetCredentialsDataCsvReset")]

        public void AddToCart(string email)
        {
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);
            _driver.Navigate().GoToUrl(url);

            HomePage hp = new HomePage(_driver);
            hp.MoveToLoginPage();

            LoginPage lp = new LoginPage(_driver);
            lp.PassReset(email);
        }
    }
}
