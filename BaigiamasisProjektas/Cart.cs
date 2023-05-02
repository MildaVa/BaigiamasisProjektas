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
        

        public Cart(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void CompareNames(string name)
        {
            string cartname = driver.FindElement(By.XPath("//a[@class= 'name text-link']")).Text;
            //aha, man irgi nepatinka, bet tais aidas (ar kitas brandas kitu atveju) vidury
            //stringo, tai contains nelabai nori atpazint kad jie panasus:(:(
            cartname = cartname.Replace("adidas ", "");
            if (!cartname.Contains(name))
            {
                Assert.Fail("different product names: " + cartname + " does not contain " + name);
            }
        }

        public void ComparePrices(string price)
        {
            string cartPrice = driver.FindElement(By.XPath("(//div[@class='price-final'])[1]")).Text;
            Assert.AreEqual(cartPrice, price);
        }


    }
}

