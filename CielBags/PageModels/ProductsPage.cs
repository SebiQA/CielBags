using CielBags.PageModels.POM;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace CielBags.PageModels
{
    class ProductsPage : BasePage

    {
        public ProductsPage(IWebDriver driver) : base(driver)
        {
        }

        const string products = "#mm_extralinks > div.left > ul > li:nth-child(1) > a"; //css
        const string firstBag = "#main > div > div.product_array_display.product_grid_display > div.__i > div > div.__e.__e1 > div > div > div > div > div > div.__h > div > div.name > div > a > h2"; //css
        const string addToCart = "ctl10_ctl00_product_ctl00_btnAdd"; //id
        const string continueShoping = "#modal_add_to_cart_confirm > div > div > div.macc_actions > a.left.modal_close"; //css
        const string myCart = "#ctl02_ctl05_upBasketDetailed > a > span.icon.im.im_cm_hdr_cart"; //css
        const string removeProduct = "ctl08_repProducts_lbDelete_0"; //id

        const string addToFavorites = "ctl10_ctl00_product_ctl00_lbWishlist"; //id
        const string favoritesSelector = "ctrl_btn"; //class
        const string deleteFavorites = "ctl10_repProducts_btnDelete_0"; //id

        const string gentiTip = "#filterbar > div > div.ts_filterbar > div:nth-child(3) > span > div:nth-child(3) > span.filter_value > input[type=checkbox]"; //css
        const string gentiDimensiune = "#filterbar > div > div.ts_filterbar > div:nth-child(5) > span > div:nth-child(2) > span.filter_value > input[type=checkbox]"; //css
        const string gentiCuloare = "#filterbar > div > div.ts_filterbar > div:nth-child(6) > span > div:nth-child(4) > span.filter_value > input[type=checkbox]"; //css

        public void AddToCart()
        {
            var produse = driver.FindElement(By.CssSelector(products));
            produse.Click();

            var seeBag = driver.FindElement(By.CssSelector(firstBag));
            seeBag.Click();

            var add = driver.FindElement(By.Id(addToCart));
            add.Click();

            var continu = driver.FindElement(By.CssSelector(continueShoping));
            continu.Click();

        }

        public void RemoveFromCart()
        {
            var produse = driver.FindElement(By.CssSelector(products));
            produse.Click();

            var seeBag = driver.FindElement(By.CssSelector(firstBag));
            seeBag.Click();

            var add = driver.FindElement(By.Id(addToCart));
            add.Click();

            var continu = driver.FindElement(By.CssSelector(continueShoping));
            continu.Click();

            var cart = driver.FindElement(By.CssSelector(myCart));
            cart.Click();

            var remove = driver.FindElement(By.Id(removeProduct));
            remove.Click();

        }

        public void AddToFavorites()
        {
            var produse = driver.FindElement(By.CssSelector(products));
            produse.Click();

            var seeBag = driver.FindElement(By.CssSelector(firstBag));
            seeBag.Click();

            var addFavorite = driver.FindElement(By.Id(addToFavorites));
            addFavorite.Click();
        }

        public void RemoveFromFavorites()
        {
            var favorites = driver.FindElement(By.ClassName(favoritesSelector));
            favorites.Click();

            var removeFavorites = driver.FindElement(By.Id(deleteFavorites));
            removeFavorites.Click();

            driver.SwitchTo().Alert().Accept();

        }

        public void Filters()
        {
            var produse = driver.FindElement(By.CssSelector(products));
            produse.Click();

            WebElement tip = (WebElement)driver.FindElement(By.CssSelector(gentiTip));
            tip.Click();

            WebElement dimensiune = (WebElement)driver.FindElement(By.CssSelector(gentiDimensiune));
            dimensiune.Click();

            WebElement culoare = (WebElement)driver.FindElement(By.CssSelector(gentiCuloare));
            culoare.Click();

        }

 
    }
}
