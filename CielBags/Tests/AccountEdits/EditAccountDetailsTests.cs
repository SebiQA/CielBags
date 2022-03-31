using CielBags.PageModels;
using CielBags.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CielBags.Tests.AccountEdits
{
    class EditAccountDetailsTests : BaseTest
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

        public void EditAccount(string email, string password)
        {
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);
            _driver.Navigate().GoToUrl(url);

            HomePage hp = new HomePage(_driver);
            hp.MoveToLoginPage();

            LoginPage lp = new LoginPage(_driver);
            lp.Login(email, password);

            AccountPage ap = new AccountPage(_driver);
            ap.EditAccount("Calea Dorobantilor", "0777777777", "Cluj", "Cluj-Napoca", "400404");

        }
    }
}
