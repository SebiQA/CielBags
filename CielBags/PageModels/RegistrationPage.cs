using CielBags.PageModels.POM;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace CielBags.PageModels
{
    public class RegistrationPage : BasePage
    {

        const string registrationLabelSelector = "#ctl08_loginWithEmailAddress > div.hello"; // CSS
        const string emailInputSelector = "ctl08_UserName"; // id
        const string registerLabelSelector = "ctl08_LoginButton"; //id
        const string nameInputSelector = "ctl08_UserNameRegistration"; //id
        const string passwordInputSelector = "ctl08_PasswordRegistration";// id
        const string confirmPassInputSelector = "ctl08_PasswordConfirmRegistration"; //id
        const string termsandconditionsAgreement = "ctl08_ckNotifyFinancial"; //id
        const string notificationsAcceptance = "ctl08_ckNotifyCommercial"; //id
        const string confirmRegistrationLabelSelector = "ctl08_registerNewUser"; // id

        const string nameRequired = "ctl08_UserNameRegistrationRequired"; //id
        const string passRequired = "ctl08_PasswordRequired"; //id
        const string passConfirmationRequired = "ctl08_ConfirmPasswordRequired"; //id
        const string tcAgreementRequired = "ctl08_vTerms > span"; //id

    

        public Boolean CheckRegistrationLabel(string label)
        {
            return String.Equals(label.ToLower(), driver.FindElement(By.CssSelector(registrationLabelSelector)).Text.ToLower());
        }

        public RegistrationPage(IWebDriver driver) : base(driver)
        {

        }

        public void RegisterUser(string email, string name, string password, string passwordConfirmation)
        {
            var emailInput = driver.FindElement(By.Id(emailInputSelector));
            emailInput.Clear();
            emailInput.SendKeys(email);

            var loginButton = driver.FindElement(By.Id(registerLabelSelector));
            loginButton.Click();

            var nameInput = driver.FindElement(By.Id(nameInputSelector));
            nameInput.Clear();
            nameInput.SendKeys(name);

            var passwordInput = driver.FindElement(By.Id(passwordInputSelector));
            passwordInput.Clear();
            passwordInput.SendKeys(password);

            var repeatPaswordInput = driver.FindElement(By.Id(confirmPassInputSelector));
            repeatPaswordInput.Clear();
            repeatPaswordInput.SendKeys(passwordConfirmation);

            WebElement checkBoxElement = (WebElement)driver.FindElement(By.Id(termsandconditionsAgreement));
            checkBoxElement.Click();

            var submit = driver.FindElement(By.Id(confirmRegistrationLabelSelector));
            submit.Submit();

        }
    }
}
