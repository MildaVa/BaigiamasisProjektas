using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using OpenQA.Selenium.DevTools.V108.CSS;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Data;


namespace BaigiamasisProjektas
{
    internal class Tests
    {
        static IWebDriver driver;
        GeneralMethods general;
        [SetUp]
        public void SETUP()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArguments("--disable-notifications"); // to disable notification
            driver = new ChromeDriver(options);
            general = new GeneralMethods(driver);
            driver.Manage().Window.Maximize();
            driver.Url = "https://eavalyne.lt/";
            general.ClickElement("//button[@class='button base-button primary normal green']");
            
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                general.TakeScreenshot();
            }
            //driver.Quit();
        }

        [Test]
        public static void CheckPriceSorting()
        {

            MainPage main = new MainPage(driver);
            ProductList product = new ProductList(driver);

            main.SearchByText("adidas");
            product.ChooseSorting("Žemiausia kaina");
            Thread.Sleep(3000); //wait for sorted page to load
            product.CheckListSortingAsc();
            product.ChooseSorting("Aukščiausia kaina");
            Thread.Sleep(3000); //wait for sorted page to load
            product.CheckListSortingDesc();

        }
        [Test]
        public void CheckLogIn()
        {
            MainPage main = new MainPage(driver);
            LoginPage login = new LoginPage(driver);

            main.ClickLogInButton();
            login.EnterEmail();
            login.EnterPassword();
            login.ClickLogin();
            login.CheckAccountExists();

        }

        [Test]
        public void CheckCart()
        {

            MainPage main = new MainPage(driver);
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
            Thread.Sleep(3000); //wait for page to load
            cart.CompareNames(name);
            cart.ComparePrices(price);

        }
    }
}
