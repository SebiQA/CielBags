using CielBags.PageModels.POM;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace CielBags.PageModels
{
    class SearchPage : BasePage
    {

        const string searchInputSelector = "search-field"; // id
        const string searchButtonSelector = "search_button"; // id


        public SearchPage(IWebDriver driver) : base(driver)
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


    }
}
