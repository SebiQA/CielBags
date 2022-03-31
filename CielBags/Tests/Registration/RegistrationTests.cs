using CielBags.PageModels;
using CielBags.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CielBags.Tests.Registration
{
    class RegistrationTests : BaseTest
    {
        string url = FrameworkConstants.GetUrl();

        private static IEnumerable<TestCaseData> GetCredentialsDataCsv()
        {
            foreach (var values in Utils.GetGenericData("TestData\\registrationData.csv"))
            {
                yield return new TestCaseData(values);
            }
        }

        [Test, TestCaseSource("GetCredentialsDataCsv")]
        public void RegisterTest(string email, string name, string password, string passwordConfirmation)
        {
            testName = TestContext.CurrentContext.Test.Name;
            _test = _extent.CreateTest(testName);
            _driver.Navigate().GoToUrl(url);

            _driver.Navigate().GoToUrl(url + registrationUrlPath);
            RegistrationPage rp = new RegistrationPage(_driver);
            Assert.IsTrue(rp.CheckRegistrationLabel("Autentificare"));
            rp.RegisterUser(email, name, password, passwordConfirmation);


        }
    }
  
}
