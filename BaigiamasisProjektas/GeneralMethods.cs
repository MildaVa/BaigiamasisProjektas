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
using OpenQA.Selenium.DevTools.V109.Target;

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
        // Funkcija nenaudojama, tai nera klaida jeigu sita
        // klase bus pernaudojama kituose projektuose ar ateityje
        // bet cia velgi kodel ji yra jeigu nenaudojama?
        // Jeigu norejai tureti varianta su errorMessage'u ir be
        // tada geresnis variantas yra uzklojimas
        // pasidaryti ClickElement kuris suvalgytu ir errorMessage nebuvimą
        // ir tada:
        // public void ClickElement(string xpath) => ClickElement(xpath, "");
        // BET, pasigilinus tai matosi kad ir funkcija visai nenaudoja errorMessage
        // kintamojo tai jis kaip ir viai nereikalingas.
        // ir siaip dvi funkcijos vienodi pavadinimai ir tas pats kodas :)
        // speju mintis buvo:

        //public void ClickElement(string xpath, string errorMessage)
        //{
        //    wait.Message = errorMessage;
        //    IWebElement elm = wait.Until(x => x.FindElement(By.XPath(xpath)));
        //    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        //    js.ExecuteScript("arguments[0].scrollIntoView(true);", elm);
        //    elm.Click();
        //}

        // tada jeigu neperduotas zinute reiktu kazka daryti
        // Tokiu atveju galima apjunti pirma ir antra varianta į:

        //public void ClickElement(string xpath, string errorMessage = "Cookie accept button was not found")
        //{
        //    wait.Message = errorMessage;
        //    IWebElement elm = wait.Until(x => x.FindElement(By.XPath(xpath)));
        //    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
        //    js.ExecuteScript("arguments[0].scrollIntoView(true);", elm);
        //    elm.Click();
        //}

        // Vietoje abieju funkciju

        public void ClickElement(string xpath, string errorMessage)
        {
            wait.Message = "Cookie accept button was not found";
            IWebElement elm = wait.Until(x => x.FindElement(By.XPath(xpath)));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", elm);
            elm.Click();
        }

        public IWebElement GetElement(By by) => wait.Until(x => x.FindElement(by));

        public void ClickElement(string xpath)
        {
            wait.Message = "Cookie accept button was not found";
            IWebElement elm = wait.Until(x => x.FindElement(By.XPath(xpath)));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", elm);
            elm.Click();
        }

        // Nenaudojamas metodas, tai galioje kaip ir anksčiau aprašytame komentare
        public void ClickElementJS(string xpath)
        {
            IWebElement el = driver.FindElement(By.XPath(xpath));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", el);
            js.ExecuteScript("arguments[0].click();", el);
        }

        public void EnterText(string xpath, string text)
        {
            // Ar tikrai tokia žinutė turėtų būti? :?
            wait.Message = "Cookie accept button was not found";
            IWebElement elm = wait.Until(x => x.FindElement(By.XPath(xpath)));
            elm.SendKeys(text);
        }


        public int GetElementsCountByXpath(string xpath)
        {
            By elements = By.XPath(xpath);
            return driver.FindElements(elements).Count();
        }

        // Pavadinimas funkcijos misleading. Tikėčiausi kad gražins
        // ką nors kaip true / false. Bet čia yra labiau iš kodo
        // ScrollToElement
        // .. Čia dabar rašau po kurio laiko tai auškčiau yra pirmos mintys dabar tesiu ..
        // Sita funkcija atlieka elemento sulaukimo ir suradimo veiksmą ir beveik
        // nieko su juo nepadaro. Nes Elementas jau yra randamas tada del visa pikta
        // toks vaizdas pascrolinkim, ir tada nieko. bet tose vietose kur naudojama
        // šita funkcija tada vėl ieškom elemento to paties ir kažką su juo darom.
        // Veikti veikia. Bet nepatinka iš funkcinės pusės.
        public void CheckElementExistsByXpath(string xpath)
        {
            // Ar tikrai tokia žinutė turėtų būti? :?
            wait.Message = "Cookie accept button was not found";
            IWebElement elm = wait.Until(x => x.FindElement(By.XPath(xpath)));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", elm);
            //driver.FindElement(By.XPath(xpath));
        }
        // Nenaudojamas metodas, tai galioje kaip ir anksčiau aprašytame komentare
        public void CheckElementExistsByID(string id)
        {
            driver.FindElement(By.Id(id));
        }
        public string GetEmail()
        {
            // Absoliutus Path'as
            // kadangi pas Tave padaryta kad credentialu failą 
            // kopijuoja į build directoriją
            // turėtų suveikti tiesiog:
             string[] lines = File.ReadAllLines(@"credentials.txt");

            // System.IO. nereikalingas, nes naudoji using System.IO;
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Milda\source\repos\paskaitos33\credentials.txt");

            // Debuginant ar kuriant išvesti yra viskas ok.
            // bet nereiktų palikti tokių dalykų nes
            // niekas i juos neziures
            // bereikalingas kodas kuris ilgins testo laiką
            foreach (string line in lines)
            {
                Console.WriteLine("\t" + line);
            }
            return lines[0];
        }

        public string GetPassword()
        {
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Milda\source\repos\paskaitos33\credentials.txt");
            string[] lines = System.IO.File.ReadAllLines(@"Credentials.txt");

            // Debuginant ar kuriant išvesti yra viskas ok.
            // bet nereiktų palikti tokių dalykų nes
            // niekas i juos neziures
            // bereikalingas kodas kuris ilgins testo laiką
            foreach (string line in lines)
            {
                Console.WriteLine("\t" + line);
            }
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
