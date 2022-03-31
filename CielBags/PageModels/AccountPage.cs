using CielBags.PageModels.POM;
using Humanizer.Localisation;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CielBags.PageModels
{
    class AccountPage : BasePage
    {

        const string emailSelector = "ctl08_UserName"; // id
        const string emailErrorSelector = "error"; // class
        const string loginButtonSelector = "ctl08_LoginButton"; // id
        const string loginLabelSelector = "hello"; // class
        const string passInputSelector = "ctl08_Password"; //id  
        const string passLoginButton = "ctl08_loginButtonPassword"; //id

        const string myAccountSelector = "#hdr_user > a"; //css
        const string deleteMyAccount = "#sidebar-inner > div > ul > li:nth-child(8) > a"; //css
        const string confPassforDeletion = "ctl10_ctl00_tbPassword"; //id
        const string submitDeletion = "ctl10_ctl00_btnSubmit"; // id

        const string editDetails = "ctl10_ctl00_ctl00_btnEdit"; //id
        const string addressSelector = "ctl10_ctl00_tbAddress"; //id
        const string phoneSelector = "ctl10_ctl00_tbAddressPhone"; //id
        const string countyInput = "ctl10_ctl00_ddlCounty"; //id
        const string cityInput = "ddlCity"; //id
        const string postalCodeInput = "ctl10_ctl00_tbZipCode"; //id
        const string submitChanges = "ctl10_ctl00_btnEdit"; //id

        const string aboutUsSelector = "tab_1663"; //id
        const string contactSelector = "tab_1664"; //id
        const string nameSelector = "ctl10_FORM1_First_Name"; //id
        const string emailContactSelector = "ctl10_FORM1_Email_Address"; //id
        const string problemSelector = "ctl10_FORM1_Details"; //id
        const string sendProblemSelector = "ctl10_btnSubmit"; //id

        public AccountPage(IWebDriver driver) : base(driver)
        {
        }


        public void DeleteAccount(string email, string password)
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

            var contulMeu = driver.FindElement(By.CssSelector(myAccountSelector));
            contulMeu.Click();

            var delete = driver.FindElement(By.CssSelector(deleteMyAccount));
            delete.Click();

            var delPassConf = driver.FindElement(By.Id(confPassforDeletion));
            delPassConf.Clear();
            delPassConf.SendKeys(password);

            var submintDelete = driver.FindElement(By.Id(submitDeletion));
            submintDelete.Click();

            driver.SwitchTo().Alert().Accept();
        }

        public void EditAccount(string Address, string Phone, string County, string City, string PostalC)
        {

            var contulMeu = driver.FindElement(By.CssSelector(myAccountSelector));
            contulMeu.Click();

            var edit = driver.FindElement(By.Id(editDetails));
            edit.Click();

            var address = driver.FindElement(By.Id(addressSelector));
            address.Clear();
            address.SendKeys(Address);

            var phone = driver.FindElement(By.Id(phoneSelector));
            phone.Clear();
            phone.SendKeys(Phone);

            SelectElement county = new SelectElement(driver.FindElement(By.Id(countyInput)));
            county.SelectByText(County);

            Thread.Sleep(2000);

            SelectElement city = new SelectElement(driver.FindElement(By.Id(cityInput)));
            city.SelectByText(City);

            var postcode = driver.FindElement(By.Id(postalCodeInput));
            postcode.Clear();
            postcode.SendKeys(PostalC);

            var confirmChanges = driver.FindElement(By.Id(submitChanges));
            confirmChanges.Click();

        }

        public void AboutUsContact(string nume, string mail, string problema)
        {
            var about = driver.FindElement(By.Id(aboutUsSelector));
            about.Click();

            var contact = driver.FindElement(By.Id(contactSelector));
            contact.Click();

            var name = driver.FindElement(By.Id(nameSelector));
            name.Clear();
            name.SendKeys(nume);

            var email = driver.FindElement(By.Id(emailContactSelector));
            email.Clear();
            email.SendKeys(mail);

            var problem = driver.FindElement(By.Id(problemSelector));
            problem.Clear();
            problem.SendKeys(problema);

            //var submitProblem = driver.FindElement(By.Id(sendProblemSelector));
            //submitProblem.Click();
        }

    }

}
