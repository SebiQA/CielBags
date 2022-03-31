using CielBags.PageModels;
using CielBags.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CielBags.Tests.AccountEdits
{
    class DeleteAccountTests : BaseTest
    {
        string url = FrameworkConstants.GetUrl();

        private static IEnumerable<TestCaseData> GetCredentialsDataCsvDelete()
        {
            foreach (var values in Utils.GetGenericData("TestData\\deleteData.csv"))
            {
                yield return new TestCaseData(values);
            }
        }
        [Test, TestCaseSource("GetCredentialsDataCsvDelete")]

        public void DeleteAcc(string email, string password)
        {
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);
            _driver.Navigate().GoToUrl(url);

            HomePage hp = new HomePage(_driver);
            hp.MoveToLoginPage();

            AccountPage ap = new AccountPage(_driver);
            ap.DeleteAccount(email, password);

        }
    }
   
}
