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

    internal class ProductCard
    {
        IWebDriver driver;
        GeneralMethods generalMethods;

        public ProductCard(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }

        public string GetName()
        {
            // Mhm kuo toliau tuo labiau nelabai man patinka šitas pavadinimas / ar užnaudojimas
            // Jis kaip ir prašosi kad būtų metamas exceptionas,
            // arba jis yra tiesiog labai paslėptas wait'as
            generalMethods.CheckElementExistsByXpath("//h1//strong[contains(@class, 'product-name')]");
            string name = driver.FindElement(By.XPath("//h1//strong[contains(@class, 'product-name')] ")).Text;
            
            return name;
        }
        public string GetPrice() {
            // Tas pats nepatikimas kaip ir aukščiau.
            // Bet bent galiu įvertinti kad gražiai atrodo iš skaitomumo pusės mažai kodo
            // jeigu pavadinimai būtų tikslesnis būtų labai gerai
            generalMethods.CheckElementExistsByXpath("//div[@data-test-id='final-price']");
            string price = driver.FindElement(By.XPath("//div[@data-test-id='final-price']")).Text;
            return price;
        }
        public bool ChooseSize()
        {
            generalMethods.ClickElement("//button[contains(@class, 'choose-size')]");
            var n = generalMethods.GetElementsCountByXpath("//button[contains(@class, 'size')]");
            for (int i = 2; i <= n+1; i++)
            {
                string xpath = "(//button[contains(@class, 'size')])[" + i + "]";
                string text =   generalMethods.GetElement(By.XPath(xpath)).Text;// driver.FindElement(By.XPath(xpath)).Text;
                if (!text.Contains("Praneškite man"))
                {
                    generalMethods.ClickElement(xpath);
                    return true;// break;
                }
              
            }
            return false;
        }

        // Ir noriu ir nenoriu čia sakyti, kad AddToCart paima pirma
        // pasitaikiusį objektą. Jeigu toks užmanymas tada norėtųsi 
        // aiškesnio pavadinimo. Dabar toks AddToCart iškrenta truputį.
        public void AddToCart()
        {
            generalMethods.ClickElement("//button[contains(@class, 'add-to-cart')]");
        }

        public void GoToCart()
        {
            generalMethods.ClickElement("//button[@data-test-id='go-to-cart-button']");
        }
    }
}
