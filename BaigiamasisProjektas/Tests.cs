using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Data;
using System.Xml.Linq;
using NUnit.Framework.Interfaces;
using System.IO;

namespace BaigiamasisProjektas
{
    internal class Tests
    {
        static IWebDriver driver;
        GeneralMethods general;
        MainPage main;
        [SetUp]
        public void SETUP()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArguments("--disable-notifications"); // to disable notification
            driver = new ChromeDriver(options);
            general = new GeneralMethods(driver);
            main = new MainPage(driver);
            driver.Manage().Window.Maximize();
            driver.Url = "https://eavalyne.lt/";
            general.ClickElement("//button[@class='button base-button primary normal green']");
            
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var name =
                    $"{TestContext.CurrentContext.Test.MethodName}" +
                    $" Error at " +
                    $"{DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'_'mm'_'ss")}";
                GeneralMethods.TakeScreenshot(driver, name);
                File.WriteAllText(
                    $"Screenshots\\{name}.txt",
                    TestContext.CurrentContext.Result.Message);
            }
            driver.Close();
            driver.Quit();
        }

        [Test]
        public void CheckPriceSorting()
        {
            ProductList product = new ProductList(driver);

            main.SearchByText("adidas");
            product.ChooseSorting("Žemiausia kaina");
            Thread.Sleep(3000); //wait for sorted page to load, no unique element in sorted page
            Assert.IsTrue(product.CheckListSortingAsc());
            product.ChooseSorting("Aukščiausia kaina");
            Thread.Sleep(3000); //wait for sorted page to load, no unique element in sorted page
            Assert.IsTrue(product.CheckListSortingDesc());

        }
        [Test]
        public void CheckLogIn()
        {
            LoginPage login = new LoginPage(driver);

            main.ClickLogInButton();
            login.EnterEmail();
            login.EnterPassword();
            login.ClickLogin();
            Assert.IsTrue(login.CheckAccountExists());

        }

        [Test]
        public void CheckCart()
        {
            ProductList product = new ProductList(driver);
            ProductCard card = new ProductCard(driver);
            Cart cart = new Cart(driver);

            main.SearchByText("adidas");
            product.OpenProduct();
            string name = card.GetName();
            string price = card.GetPrice();
            if (!card.ChooseSize())
            {
                throw new Exception("Visi produkto dydziai ispirkti");
            }
            card.AddToCart();
            card.GoToCart();
            general.CheckElementExistsByXpath("//h1[contains(text(), 'Krepšelis')]"); //wait for page to load
            cart.CompareNames(name);
            cart.ComparePrices(price);

        }
    }
}
