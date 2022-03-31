using NUnit.Framework;
using CielBags.Tests;
using CielBags.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using CielBags.PageModels;

namespace CielBags.Tests.Authentication
{
    public class AuthenticationTests : BaseTest
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

        public void Auth(string email, string password)
        {
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);
            _driver.Navigate().GoToUrl(url);

            HomePage hp = new HomePage(_driver);
            hp.MoveToLoginPage();

            LoginPage lp = new LoginPage(_driver);
            Assert.IsTrue(lp.CheckLoginLabel("Autentificare"));
            lp.Login(email, password);


        }
    }
}