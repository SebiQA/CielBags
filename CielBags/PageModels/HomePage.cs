using CielBags.PageModels.POM;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace CielBags.PageModels
{
    class HomePage : BasePage
    {
        
        const string MyAccountButtonSelector = "#tbar > div.user_box > ul > li:nth-child(1) > a"; // css
        const string loginButtonExpandedSelector = "#navbarLoginButton > span"; // css
        const string reloadPageSelector = "mat-button-wrapper"; // css

        public HomePage(IWebDriver driver) : base(driver)
        {

        }


        public void MoveToLoginPage()
        {
            driver.FindElement(By.CssSelector(MyAccountButtonSelector)).Click();
        }

    }
    
}
