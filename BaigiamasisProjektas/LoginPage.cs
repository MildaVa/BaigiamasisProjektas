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
        // kintamasis nenaudojamas klaseje.
        IWebDriver driver;
        GeneralMethods generalMethods;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }

        public void EnterEmail()
        {
            generalMethods.EnterText("//input[@id='email']", generalMethods.GetEmail());
        }
        //Puikus komentaras
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
        // Velgi noretusi arba grazinamo elemento ar true / false
        // pagal pavadinimą metodo.
        public void CheckAccountExists()
        {
            generalMethods.CheckElementExistsByXpath("//span[contains(text(), 'Mano paskyra')]");
        }
    }
}

