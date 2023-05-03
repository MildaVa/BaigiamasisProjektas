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
    internal class ProductList
    {
        IWebDriver driver;
        GeneralMethods generalMethods;
        

        public ProductList(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }
        
        public void ChooseSorting(string sortingtype)
        {
            generalMethods.ClickElement("//div[@class='dropdown sort-dropdown']");
            generalMethods.ClickElement("//option[contains(text(), '" + sortingtype + "')]");
        }
        public bool CheckListSortingAsc()
        {
            
            By products = By.XPath("//div[@class='price-final']");

            List<double> prices = new List<double>();
            foreach (IWebElement el in driver.FindElements(products))
            {
               
                string oneprice = el.Text.Replace(",", "");
                oneprice = oneprice.Substring(0, oneprice.Length - 2);
                int onepriceint = int.Parse(oneprice);
                prices.Add(onepriceint);
               // Console.WriteLine(oneprice);
            }
            for (int i = 0; i < prices.Count - 1; i++)
            {
                if (prices[i] > prices[i + 1])
                {
                    return false;
                    //Assert.Fail("blogas rikiavimas");
                }
            }
            return true;
        }

        public bool CheckListSortingDesc()
        {

            By products = By.XPath("//div[@class='price-final']");

            List<double> prices = new List<double>();
            foreach (IWebElement el in driver.FindElements(products))
            {

                string oneprice = el.Text.Replace(",", "");
                oneprice = oneprice.Substring(0, oneprice.Length - 2);
                int onepriceint = int.Parse(oneprice);
                prices.Add(onepriceint);
               // Console.WriteLine(oneprice);
            }
            for (int i = 0; i < prices.Count - 1; i++)
            {
                if (prices[i] < prices[i + 1])
                {
                    return false;
                 //   Assert.Fail("blogas rikiavimas");
                }
            }
            return true;
        }

        public void OpenProduct(int i = 1)
        {
            generalMethods.ClickElement($"(//div[@class='product-switcher product-image'])[{i}]");
        }
    }
}
