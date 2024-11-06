using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumCsharp
{
    public class FirstTestCase
    {
        public string Dependencypath = "C:\\Users\\opilane\\source\\repos\\TARpe22\\Vider\\SeleniumCsharp\\SeleniumCsharp\\Dependencies";
        private IWebDriver driver;
        private WebDriverWait wait;

        public static void Main() { }

        [SetUp]
        public void Setup()
        {
            string binaryLocation = "C:\\Users\\opilane\\source\\repos\\TARpe22\\Vider\\FFP\\FirefoxPortable\\App\\Firefox64\\firefox.exe";
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = binaryLocation;

            driver = new FirefoxDriver("C:\\Users\\opilane\\source\\repos\\TARpe22\\Vider\\SeleniumCsharp\\SeleniumCsharp\\drivers", options);

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));  
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }

        [Test]
        public void Test_progressBar()
        {
            driver.Url = "http://www.uitestingplayground.com/progressbar";

            var startBtn = driver.FindElement(By.Id("startButton"));
            startBtn.Click();

            var progressBar = driver.FindElement(By.Id("progressBar"));

            wait.Until(driver =>
            {
                var progress = Int32.Parse(progressBar.GetAttribute("aria-valuenow"));
                return progress >= 70;
            });

            var stopBtn = driver.FindElement(By.Id("stopButton"));
            stopBtn.Click();

            var finalProgressValue = Int32.Parse(progressBar.GetAttribute("aria-valuenow"));

            Assert.IsTrue(Math.Abs(finalProgressValue - 75) <= 5, $"Expected progress to be within 5% of 75, but got {finalProgressValue}%");
        }
    }
}
