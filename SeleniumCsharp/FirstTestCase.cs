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

        [Test]
        public void Test_classAttributeButtonClick()
        {
            driver.Url = "http://www.uitestingplayground.com/classattr";

            var primaryButton = driver.FindElement(By.XPath("//button[contains(concat(' ', normalize-space(@class), ' '), ' btn-primary ')]"));
            primaryButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(drv => drv.SwitchTo().Alert() != null);

            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();

            Assert.IsTrue(true, "Clicked on the primary button and handled the alert.");
        }

        [Test]
        public void Test_hiddenLayerButtonClick()
        {
            driver.Url = "http://www.uitestingplayground.com/hiddenlayers";
            var greenButton = driver.FindElement(By.Id("greenButton"));
            greenButton.Click();

            try
            {
                var overlay = driver.FindElement(By.CssSelector("div.spa-view[style*='z-index: 2']"));
                wait.Until(driver => overlay.Displayed);

                try
                {
                    greenButton.Click();
                    Assert.Fail("The green button was clicked twice.");
                }
                catch (ElementClickInterceptedException)
                {
                    Assert.Pass("The green button could not be clicked twice, asa expected!");
                }
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Overlay was not found.");
            }
        }

    }
}
