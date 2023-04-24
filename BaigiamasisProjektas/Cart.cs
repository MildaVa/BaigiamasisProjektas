using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace BaigiamasisProjektas
{
    internal class Cart
    {
        IWebDriver driver;

        // Šitie kintamieji nenaudojami sioje klase
        // Ju kaip ir nereikia
        GeneralMethods generalMethods;
        ProductCard card;


        public Cart(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
            card = new ProductCard(driver);  
        }

        public void CompareNames(string name)
        {
            string cartname = driver.FindElement(By.XPath("//a[@class= 'name text-link']")).Text;
            // Čia nelabai patinka nes prisiriša prie vieno konkretaus testo.
            // Kitų gal testų nelaužtų dabar, bet jeigu
            // ateityje rašysi kitą testą ir norėsi palyginti o pavadinime bus
            // Adidas. Reiktų geresnio sprendimo bet kol veikia tai gerai
            cartname = cartname.Replace("adidas ", "");
            if (!cartname.Contains(name))
            {
                // Typos :)
                Assert.Fail("different product names: " + cartname + " doesn not containt " + name);
            }
        }

        public void ComparePrices(string price)
        {
            // Mazas gripe'as cartprice -> cartPrice
            string cartprice = driver.FindElement(By.XPath("(//div[@class='price-final'])[1]")).Text;
            Assert.AreEqual(cartprice, price);
        }


    }
}

