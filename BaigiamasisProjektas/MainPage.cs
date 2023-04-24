using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisProjektas
{
    internal class MainPage
    {
        // Kintamasis nenaudojamas
        IWebDriver driver;
        GeneralMethods generalMethods;
        public MainPage(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }
       
        public void SearchByText(string text)
        {
            generalMethods.EnterText("//input[@id='input-field-search-id']", text);
            generalMethods.ClickElement("//button[@type='submit']");
        }
        public void ClickLogInButton()
        {
            generalMethods.ClickElement("//span[contains(text(), 'Prisijungti')]");
        }
    }
}
