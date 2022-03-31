using CielBags.PageModels.POM;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace CielBags.PageModels
{
    class LogoutPage : BasePage
    {

        const string emailSelector = "ctl08_UserName"; // id
        const string emailErrorSelector = "error"; // class
        const string loginButtonSelector = "ctl08_LoginButton"; // id
        const string loginLabelSelector = "hello"; // class
        const string passInputSelector = "ctl08_Password"; //id  
        const string passLoginButton = "ctl08_loginButtonPassword"; //id

        const string disconnectSelector = "#tbar > div.user_box > ul > li:nth-child(2) > a"; //css
        const string submitDisconnect = "ctl10_ctl00_btnYes"; //id

        public LogoutPage(IWebDriver driver) : base(driver)
        {
        }

        public void Logout(string email, string password)
        {
            var emailInput = driver.FindElement(By.Id(emailSelector));
            emailInput.Clear();
            emailInput.SendKeys(email);

            var loginButton = driver.FindElement(By.Id(loginButtonSelector));
            loginButton.Click();

            var passwordInput = driver.FindElement(By.Id(passInputSelector));
            passwordInput.Clear();
            passwordInput.SendKeys(password);

            var continueButton = driver.FindElement(By.Id(passLoginButton));
            continueButton.Click();

            var logoutButton = driver.FindElement(By.CssSelector(disconnectSelector));
            logoutButton.Click();

            var confirmLogut = driver.FindElement(By.Id(submitDisconnect));
            confirmLogut.Submit();
        }

    }



}