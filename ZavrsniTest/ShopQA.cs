using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;
using System.Collections.ObjectModel;
using Cas27.Lib;

namespace ZavrsniTest
{
    class ShopQA : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Logger.setFileName(@"C:\SeleniumLogger\Tests.log");
            this.driver = new FirefoxDriver();
            this.driver.Manage().Window.Maximize();
            this.wait = new WebDriverWait(this.driver, new TimeSpan(0, 0, 10));
        }

        [TearDown]
        public void TearDown()
        {
            this.driver.Close();
        }
        [Test]
        [Category("http://test.qa.rs/")]
        public void LoginTest()
        {
            Logger.beginTest("LoginInputTest");
            Logger.log("INFO", "Starting Test:");
            this.GoToURL("http://test.qa.rs/");
            this.ExplicitWait(100);
            IWebElement LinkLogin = this.MyFindElement(By.LinkText("Login"));
            LinkLogin.Click();

            IWebElement UsernameInput = this.MyFindElement(By.Name("username"));
            UsernameInput.SendKeys("Miha.Zavrsni");
            this.ExplicitWait(200);
            IWebElement PasswordInput = this.MyFindElement(By.Name("password"));
            PasswordInput.SendKeys("someComplicatedPassword");
            IWebElement LoginSubmit = this.MyFindElement(By.Name("login"));
            LoginSubmit.Click();

            IWebElement LogoutCheckLing = this.WaitForElement(EC.ElementIsVisible(By.LinkText("Logout Miha")));
            Assert.IsTrue(LogoutCheckLing.Displayed);
        }

        [Test]
        [Category("http://test.qa.rs/")]
        public void TestAddToCart()
        {
            string packagequantity = "5";
            Logger.beginTest("TestAddToCart");
            Logger.log("INFO", "Begin Test:");

            this.GoToURL("http://test.qa.rs/");
            this.ExplicitWait(100);
            IWebElement LinkLogin = this.MyFindElement(By.LinkText("Login"));
            LinkLogin.Click();

            PopulateInput(By.Name("username"), "Miha.Zavrsni");
            PopulateInput(By.Name("password"), "someComplicatedPassword");
            IWebElement LoginSubmit = this.MyFindElement(By.Name("login"));
            LoginSubmit.Click();
            this.ExplicitWait(250);

            IWebElement InsertNumber1 = this.MyFindElement(By.XPath("//INPUT[@type='number']/self::INPUT"));
            InsertNumber1.SendKeys("2");
            this.ExplicitWait(250);

            IWebElement SideDishSelect1 = this.MyFindElement(By.XPath("//SELECT[@name='side']/self::SELECT"));
            SelectElement dishselect1 = new SelectElement(SideDishSelect1);
            dishselect1.SelectByValue("or");



            IWebElement DropDownSubmit = this.MyFindElement(By.XPath("//INPUT[@type='submit']/self::INPUT"));
            DropDownSubmit.Click();
            this.ExplicitWait(250);

            IWebElement SubmitCheckout = this.MyFindElement(By.Name("checkout"));
            SubmitCheckout.Click();

            IWebElement h1cart = this.WaitForElement(EC.ElementIsVisible(By.XPath("//h1[contains(text(),'Order #')]")));
           
        }

        [STAThread]
        static void Main()
        {
        }

    }
}
