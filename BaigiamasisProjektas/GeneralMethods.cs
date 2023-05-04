using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using OpenQA.Selenium.Support.Extensions;
using System.IO;

namespace BaigiamasisProjektas
{
    public class GeneralMethods
    {
        IWebDriver driver;
        DefaultWait<IWebDriver> wait;

        public GeneralMethods(IWebDriver driver)
        {
            this.driver = driver;
            wait = new DefaultWait<IWebDriver>(driver);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        public void ClickElement(string xpath, string errorMessage = "Cookie accept button was not found")
        {
            wait.Message = errorMessage;
            IWebElement elm = wait.Until(x => x.FindElement(By.XPath(xpath)));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", elm);
            elm.Click();
        }

        //pasilikau uzsikomentavus sau nenaudojamus metodus, tai nekreipkit demesio

        //public void ClickElement(string xpath, string errorMessage)
        //{
        //    wait.Message = "Cookie accept button was not found";
        //    IWebElement elm = wait.Until(x => x.FindElement(By.XPath(xpath)));
        //    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        //    js.ExecuteScript("arguments[0].scrollIntoView(true);", elm);
        //    elm.Click();
        //}

        //public void ClickElement(string xpath)
        //{
        //    wait.Message = "Cookie accept button was not found";
        //    IWebElement elm = wait.Until(x => x.FindElement(By.XPath(xpath)));
        //    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        //    js.ExecuteScript("arguments[0].scrollIntoView(true);", elm);
        //    elm.Click();
        //}

        //public void ClickElementJS(string xpath)
        //{
        //    IWebElement el = driver.FindElement(By.XPath(xpath));
        //    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        //    js.ExecuteScript("arguments[0].scrollIntoView(true);", el);
        //    js.ExecuteScript("arguments[0].click();", el);
        //}

        public void EnterText(string xpath, string text)
        {

            wait.Message = "Cookie accept button was not found";
            IWebElement elm = wait.Until(x => x.FindElement(By.XPath(xpath)));
            elm.SendKeys(text);
        }

        public IWebElement GetElement(By by) => wait.Until(x => x.FindElement(by));

        public int GetElementsCountByXpath(string xpath)
        {
            By elements = By.XPath(xpath);
            return driver.FindElements(elements).Count();
        }

        //As ji atvardinau atgal i originalu, man paciai aiskiau tad aka as veikiu
        //dabar kai boolean gal taip labai nepykdys jusu:DD
        public bool CheckElementExistsByXpath(string xpath)
        {
            wait.Message = "Cookie accept button was not found";
            IWebElement elm = wait.Until(x => x.FindElement(By.XPath(xpath)));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", elm);
            return true;
            //driver.FindElement(By.XPath(xpath));
        }
        //public void FindElementById(string id)
        //{
        //    driver.FindElement(By.Id(id));
        //}
        public string GetEmail()
        {
            string[] lines = System.IO.File.ReadAllLines(@"Credentials.txt");

            //foreach (string line in lines)
            //{
            //    Console.WriteLine("\t" + line);
            //}
            return lines[0];
        }

        public string GetPassword()
        {
         
            string[] lines = System.IO.File.ReadAllLines(@"Credentials.txt");

            //foreach (string line in lines)
            //{
            //    Console.WriteLine("\t" + line);
            //}
            return lines[1];
        }
        //public void TakeScreenshot()
        //{
        //    Screenshot ss = driver.TakeScreenshot();
        //    string screenshot = "screenshot" + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss") + ".jpg";
        //    ss.SaveAsFile("C:\\Users\\Milda\\source\\repos\\BaigiamasisProjektas" + screenshot);
        //}
        public static void TakeScreenshot(IWebDriver driver, string fileName)
        {
            var screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            if (!Directory.Exists("Screenshots"))
            {
                Directory.CreateDirectory("Screenshots");
            }
            screenshot.SaveAsFile(
                $"Screenshots\\{fileName}.png",
                ScreenshotImageFormat.Png);
        }
    }
}
