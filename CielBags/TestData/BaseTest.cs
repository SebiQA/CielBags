using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using CielBags.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using CielBags;

namespace CielBags.Tests
{
    public class BaseTest
    {
        ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        public IWebDriver _driver;
        public static ExtentReports _extent;
        public ExtentTest _test;
        public string testName;

        protected static string loginUrlPath = "/autentificare";
        protected static string registrationUrlPath = "/autentificare";

        [OneTimeSetUp]
        protected void ExtentStart()
        {
            var path = System.Reflection.Assembly.GetCallingAssembly().CodeBase; //path to the location of the test that are running
            var actualPath = path.Substring(0, path.LastIndexOf("bin")); //get  bin folder
            var projectPath = new Uri(actualPath).LocalPath;
            Directory.CreateDirectory(projectPath.ToString() + "Reports");
            DateTime time = DateTime.Now;
            var reportPath = projectPath + "Reports\\Index.html" + time.ToString("h_mm_ss") + ".html";
            var htmlReporter = new ExtentV3HtmlReporter(reportPath);
            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);
            _extent.AddSystemInfo("Host Name", Environment.MachineName);
            _extent.AddSystemInfo("Environment", "Test ENV");
            _extent.AddSystemInfo("UserName", "Seb Filip");
            htmlReporter.LoadConfig(projectPath + "report-config.xml");
        }


        public static string conDetails;
        // Before each test
        [SetUp]
        public void Setup()
        {
            // Instatiate the browser using the Browser Factory class created in Utilities

            driver.Value = Browser.GetDriver();
            _driver = driver.Value;

            // Read the encrypted connection string (server=;user=;password=;port=;database=) from json in conDetails variable
           


        }

        // After each test
        [TearDown]
        public void Teardown()
        {
            var currentStatus = TestContext.CurrentContext.Result.Outcome.Status; // poate fi pass faill inconclusive
            bool passed = currentStatus == NUnit.Framework.Interfaces.TestStatus.Passed;
            var currentStackTrace = TestContext.CurrentContext.Result.StackTrace;
            var stackTrace = string.IsNullOrEmpty(currentStackTrace) ? "" : currentStackTrace;

            Status logstatus = Status.Pass;
            String screenShotPath, filename;
            DateTime time = DateTime.Now;
            filename = "Screenshot_" + time.ToString("h_mm_ss") + testName + ".png";

            switch (currentStatus)
            {
                case NUnit.Framework.Interfaces.TestStatus.Failed:
                    {
                        logstatus = Status.Fail;
                        var screenshotEntity = Utils.CaptureScreenShot(_driver, filename);
                        _test.Log(Status.Fail, "Fail");
                        _test.Fail("Test failed: ", screenshotEntity);
                        //_test.Log(Status.Fail, _test.AddScreenCaptureFromPath("Screenshot\\" + filename).ToString()); - metoda de salvare screenshot
                        break;
                    }

                case NUnit.Framework.Interfaces.TestStatus.Passed:
                    {
                        logstatus = Status.Pass;
                        _test.Log(Status.Pass, "Pass");
                        _test.Pass("Test passed: ", Utils.CaptureScreenShot(_driver, filename));
                        break;
                    }

                case NUnit.Framework.Interfaces.TestStatus.Inconclusive:
                    {
                        logstatus = Status.Warning;
                        _test.Log(Status.Warning, "Inconclusive");
                        break;
                    }

                case NUnit.Framework.Interfaces.TestStatus.Skipped:
                    {
                        logstatus = Status.Skip;
                        _test.Log(Status.Skip, "Skkiped");
                        break;
                    }
                default:
                    {
                        logstatus = Status.Error;
                        _test.Log(Status.Error, "Test had Errors");
                        break;
                    }

                    

            }
            _test.Log(logstatus, "Test" + testName + "was" + logstatus + "\n" + stackTrace);
            _driver.Quit();

        }

        [OneTimeTearDown]
        public void Allteardown()
        {
            
            _extent.Flush();
        }
    }
}