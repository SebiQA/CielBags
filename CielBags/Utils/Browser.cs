using CielBags.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;

namespace CielBags
{
    public class Browser
    {

        public static IWebDriver GetDriver(WebBrowsers browserType)
        {
            switch (browserType) 
            {
                // Instantiate a chrome driver
                case WebBrowsers.Chrome:
                    {
                        var options = new ChromeOptions();
                        //options for the driver based on flags from FrameWorkConstants
                        if (FrameworkConstants.startMaximized)
                        {
                            options.AddArgument("--start-maximized");
                        }
                 
                        if (FrameworkConstants.startHeadless)
                        {
                            options.AddArgument("headless");
                        }
                        if (FrameworkConstants.ignoreCertErr)
                        {
                            options.AddArgument("ignore-certificate-errors");
                        }
                        options.AddArgument("no-sandbox");
                        //proxy definition
                        var proxy = new Proxy
                        {
                            HttpProxy = FrameworkConstants.browserProxy,
                            IsAutoDetect = false
                        };
                        if (FrameworkConstants.useProxy)
                        {
                            options.Proxy = proxy;
                        }
                        //initiate chrome driver with options
                        return new ChromeDriver(options);
                    }
                case WebBrowsers.Firefox:
                    {
                        //options for the driver based on flags from FrameWorkConstants
                        var firefoxOptions = new FirefoxOptions();
                        List<string> optionList = new List<string>();
                        if (FrameworkConstants.startHeadless)
                        {
                            optionList.Add("--headless");
                        }
                        if (FrameworkConstants.ignoreCertErr) 
                        {
                            optionList.Add("--ignore-certificate-errors");
                        }

                        firefoxOptions.AddArguments(optionList);
                        FirefoxProfile fProfile = new FirefoxProfile();

                        //Adding extension if the oprion is enable in FrameworkConstants
                        if (FrameworkConstants.startWithExtension)
                        {
                            fProfile.AddExtension(FrameworkConstants.GetExtensionName(browserType));
                        }
                        firefoxOptions.Profile = fProfile;

                        //initiate the Firefox driver with options
                        return new FirefoxDriver(firefoxOptions);
                    }
                case WebBrowsers.Edge:
                    {

                        var edgeOptions = new EdgeOptions();
                        //edgeOptions.AddExtension("C:\\Users\\Sebastian\\Downloads\\extension_4_42_0_0.crx");
                        //edgeOptions.AddArguments("args", "['start-maximized', 'headless']");
                        if (FrameworkConstants.startMaximized)
                        {
                            edgeOptions.AddArguments("--start-maximized");
                        }
                        if (FrameworkConstants.startHeadless)
                        {
                            edgeOptions.AddArguments("headless");
                        }
                        if (FrameworkConstants.startWithExtension)
                        {
                            edgeOptions.AddExtension(FrameworkConstants.GetExtensionName(browserType));
                        }
                        return new EdgeDriver(edgeOptions);
                    }

                    // if the specified browser is not implemented
                default:
                    {
                        throw new BrowserTypeException(browserType.ToString());
                    }
            }
        }

        // This method will provide a driver, based on the config file browser attribute
        public static IWebDriver GetDriver()
        {
            WebBrowsers cfgBrowser;
            switch (FrameworkConstants.configBrowser.ToUpper())
            {
                case "FIREFOX":
                    {
                        cfgBrowser = WebBrowsers.Firefox;
                        break;
                    }
                case "CHROME":
                    {
                        cfgBrowser = WebBrowsers.Chrome;
                        break;
                    }
                case "EDGE":
                    {
                        cfgBrowser = WebBrowsers.Edge;
                        break;
                    }
                default:
                    {
                        throw new BrowserTypeException(String.Format("Browser {0} not supported", FrameworkConstants.configBrowser));
                    }
            }

            return GetDriver(cfgBrowser);
        }
    }

}

    //browser Enum with the supported browser types
    public enum WebBrowsers
    {
        Chrome,
        Firefox,
        Edge
    }

