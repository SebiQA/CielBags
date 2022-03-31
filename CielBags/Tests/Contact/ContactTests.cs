using CielBags.PageModels;
using CielBags.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CielBags.Tests.Contact
{
    class ContactTests : BaseTest
    {
        string url = FrameworkConstants.GetUrl();

        private static IEnumerable<TestCaseData> GetCredentialsDataCsvAuth()
        {
            foreach (var values in Utils.GetGenericData("TestData\\loginData.csv"))
            {
                yield return new TestCaseData(values);
            }
        }
        [Test, TestCaseSource("GetCredentialsDataCsvAuth")]

        public void Contact(string email, string password)
        {
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);
            _driver.Navigate().GoToUrl(url);

            HomePage hp = new HomePage(_driver);
            hp.MoveToLoginPage();

            LoginPage lp = new LoginPage(_driver);
            lp.Login(email, password);

            AccountPage ap = new AccountPage(_driver);
            ap.AboutUsContact("Sebastian", "sebi_filip07@yahoo.com", "No problemo");

        }
    
    }
}
