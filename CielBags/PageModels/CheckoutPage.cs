using CielBags.PageModels.POM;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CielBags.PageModels
{
    class CheckoutPage : BasePage
    {

        const string searchInputSelector = "search-field"; // id
        const string searchButtonSelector = "search_button"; // id
        const string addToCart = "ctl10_ctl00_product_ctl00_btnAdd"; //id
        const string orderDetailsSelector = "#modal_add_to_cart_confirm > div > div > div.macc_actions > a.right"; //css
        const string nextStepSelector = "ctl08_btnProceedToCheckout"; //id
        const string cardPaymentSelector = "#ctl08_paymentMethods > div > div > ul > li:nth-child(2) > div > div.fields > p"; //css
        const string continueToCheckoutSelector = "ctl08_btnContinue"; // id
        const string checkTermsSelector = "ctl08_chkTerms"; //id
        const string sendOrderSelector = "ctl08_btnContinue"; //id

        const string inputCardSelector = "paymentCardNumber"; //id
        const string inputCardholderSelector = "paymentCardName"; //id
        const string expiryMonthSelector = "paymentExpMonth"; // id
        const string expiryYearSelector = "paymentExpYear"; //id
        const string inputCvvSelector = "paymentCVV2Number"; //id
        const string paySelector = "#cardPaymentForm > div:nth-child(6) > div:nth-child(2) > button"; //css

        public CheckoutPage(IWebDriver driver) : base(driver)
        {
        }

        public void Search(string keyword)
        {
            var search = driver.FindElement(By.Id(searchInputSelector));
            search.Clear();
            search.SendKeys(keyword);

            var searchButton = driver.FindElement(By.Id(searchButtonSelector));
            searchButton.Click();
        }

        public void AddToCart()
        {
            var add = driver.FindElement(By.Id(addToCart));
            add.Click();
        }

        public void CompletePurchase()
        {
            var orderDetails = driver.FindElement(By.CssSelector(orderDetailsSelector));
            orderDetails.Click();

            Thread.Sleep(2000);

            var pasulUrmator = driver.FindElement(By.Id(nextStepSelector));
            pasulUrmator.Click();

            Thread.Sleep(2000);

            var methodPayment = driver.FindElement(By.CssSelector(cardPaymentSelector));
            methodPayment.Click();

            var continuetocheckout = driver.FindElement(By.Id(continueToCheckoutSelector));
            continuetocheckout.Click();

            var agreeTerms = driver.FindElement(By.Id(checkTermsSelector));
            agreeTerms.Click();

            var sendOrder = driver.FindElement(By.Id(sendOrderSelector));
            sendOrder.Click();

        }

        public void FinishPayment(string creditNumber, string cardName, string expiryMonth, string expiryYear, string cardholderVV)
        {
            var card = driver.FindElement(By.Id(inputCardSelector));
            card.Clear();
            card.SendKeys(creditNumber);

            var cardholder = driver.FindElement(By.Id(inputCardholderSelector));
            cardholder.Clear();
            cardholder.SendKeys(cardName);

            SelectElement expMonth = new SelectElement(driver.FindElement(By.Id(expiryMonthSelector)));
            expMonth.SelectByText(expiryMonth);

            SelectElement expYear = new SelectElement(driver.FindElement(By.Id(expiryYearSelector)));
            expYear.SelectByText(expiryYear);

            var cvv = driver.FindElement(By.Id(inputCvvSelector));
            cvv.Clear();
            cvv.SendKeys(cardholderVV);

            var pay = driver.FindElement(By.CssSelector(paySelector));
            pay.Click();

        }


    }
}
