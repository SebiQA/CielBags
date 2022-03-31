using CielBags.PageModels.POM;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace CielBags.PageModels
{
    class LoginPage : BasePage
    {

        const string emailSelector = "ctl08_UserName"; // id
        const string emailErrorSelector = "error"; // class
        const string loginButtonSelector = "ctl08_LoginButton"; // id
        const string loginLabelSelector = "hello"; // class
        const string passInputSelector = "ctl08_Password"; //id  
        const string passLoginButton = "ctl08_loginButtonPassword"; //id

        const string passwordResetSelector = "ctl08_sendMyPassword"; //id

        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public Boolean CheckLoginLabel(string label)
        { 
            return String.Equals(label.ToLower(), driver.FindElement(By.ClassName(loginLabelSelector)).Text.ToLower());
        }

        public void Login(string email, string password)
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

        }

        public void PassReset(string email)
        {
            var emailInput = driver.FindElement(By.Id(emailSelector));
            emailInput.Clear();
            emailInput.SendKeys(email);

            var loginButton = driver.FindElement(By.Id(loginButtonSelector));
            loginButton.Click();

            var resetPass = driver.FindElement(By.Id(passwordResetSelector));
            resetPass.Click();

        }

    }

    
    
}
