using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumTest
{
    [TestClass]
    public class SeleniumTest
    {
        private IWebDriver driver;
        private readonly string baseUrl = "https://rakultudbpcalculator.azurewebsites.net/";

        [TestInitialize]
        public void TestSetup()
        {
            // Use WebDriverManager to set up the ChromeDriver
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // Cleanup code, quit the driver
            driver.Quit();
        }

        private void PerformTest(string systolicValue, string diastolicValue, string expectedCategory)
        {
            driver.Navigate().GoToUrl(baseUrl);

            // Get systolic value element
            IWebElement systolicPressure = driver.FindElement(By.Id("BP_Systolic"));
            systolicPressure.Click();
            systolicPressure.Clear();
            systolicPressure.SendKeys(systolicValue);

            // Get diastolic value element
            IWebElement diastolicPressure = driver.FindElement(By.Id("BP_Diastolic"));
            diastolicPressure.Click();
            diastolicPressure.Clear();
            diastolicPressure.SendKeys(diastolicValue);

            // Submit button element
            IWebElement submitButton = driver.FindElement(By.XPath("//*[@id=\"form1\"]/div[3]/input"));
            submitButton.Click();

            // Verify Added Item name
            IWebElement itemtext = driver.FindElement(By.XPath("//*[@id='form1']/div[4]"));
            string getText = itemtext.Text;
            Assert.IsTrue(expectedCategory.Contains(getText));
        }

        [TestMethod]
        public void HBPCategory()
        {
            PerformTest("150", "90", "High Blood Pressure");
            Console.WriteLine("HBP Test Passed");
        }

        [TestMethod]
        public void PHBPCategory()
        {
            PerformTest("130", "85", "Pre-High Blood Pressure");
            Console.WriteLine("PHBP Test Passed");
        }

        [TestMethod]
        public void IBPCategory()
        {
            PerformTest("115", "75", "Ideal Blood Pressure");
            Console.WriteLine("IBP Test Passed");
        }

        [TestMethod]
        public void LBPCategory()
        {
            PerformTest("80", "50", "Low Blood Pressure");
            Console.WriteLine("LBP Test Passed");
        }
    }
}
