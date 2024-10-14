using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCsharp
{
    public class FirstTestCase
    {
        static void Main(string[] args)
        {
            string binaryLocation = "I:\\Oppematerjal\\Tarkvara testimine\\FFP\\FirefoxPortable\\App\\Firefox64\\firefox.exe";

            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = binaryLocation;

            IWebDriver driver = new FirefoxDriver("C:\\Users\\opilane\\source\\repos\\TARpe22\\Vider\\SeleniumCsharp\\SeleniumCsharp\\drivers", options);
            driver.Url = "https://store.steampowered.com/";
        }
    }
}
