using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisProjektas
{
    internal class LoginPage
    {
        GeneralMethods generalMethods;
        public LoginPage(IWebDriver driver)
        {
            generalMethods = new GeneralMethods(driver);
        }

        public void EnterEmail()
        {
            generalMethods.EnterText("//input[@id='email']", generalMethods.GetEmail());
        }
        public void EnterEmail(string email) //overloadinu, kad butu variantu
        {
            generalMethods.EnterText("//input[@id='email']", email);
        }
        public void EnterPassword()
        {
            generalMethods.EnterText("//input[@id='password']", generalMethods.GetPassword());
        }
        public void ClickLogin()
        {
            generalMethods.ClickElement("(//button[@type='submit'])[2]");
        }
        public bool CheckAccountExists()
        {
           return generalMethods.FindElementByXpath("//span[contains(text(), 'Mano paskyra')]");
        }
    }
}

