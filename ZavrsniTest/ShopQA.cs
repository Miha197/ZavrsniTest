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
            InsertNumber1.Click();
            InsertNumber1.SendKeys("2");
            this.ExplicitWait(250);

            IWebElement SideDishSelect1 = this.MyFindElement(By.XPath("//SELECT[@name='side']/self::SELECT"));
            SelectElement dishselect1 = new SelectElement(SideDishSelect1);
            dishselect1.SelectByValue("or");



            IWebElement DropDownSubmit = this.MyFindElement(By.XPath("//INPUT[@type='submit']/self::INPUT"));
            DropDownSubmit.Click();
            this.ExplicitWait(250);


            IWebElement ContinueShoppingButton = this.MyFindElement(By.XPath("//a[contains(.,'Continue shopping')]"));
            ContinueShoppingButton.Click();

            IWebElement DoubleBSubmit = this.MyFindElement(By.XPath("(//INPUT[@type='submit'])[2]"));
            DoubleBSubmit.Click();

            IWebElement ContinueShoppingButton2 = this.MyFindElement(By.XPath("//a[contains(.,'Continue shopping')]"));
            ContinueShoppingButton2.Click();

            IWebElement MegaBSubmit = this.MyFindElement(By.XPath("(//INPUT[@type='submit'])[3]"));
            MegaBSubmit.Click();

            IWebElement TotalCart = this.MyFindElement(By.XPath("//td[contains(., 'Total:')]"));
            string TotalCartS = TotalCart.Text.Substring(7);
            Logger.log("INFO:", $"Cart price:{TotalCartS}");


            IWebElement SubmitCheckout = this.MyFindElement(By.Name("checkout"));
            SubmitCheckout.Click();

            IWebElement CardCharged = this.MyFindElement(By.XPath("//h3[contains(.,'Your credit card has been charged')]"));
            string NewCardCharged = CardCharged.Text.Substring(CardCharged.Text.IndexOf("$"));
            Logger.log("INFO:", $"CardChargedNew:{NewCardCharged}");
            Assert.AreEqual(TotalCartS,NewCardCharged);
           
        }
        [Test]
        [Category("http://test.qa.rs/")]
        public void LogoutTest()
        {
            Logger.beginTest("LogoutTest");
            Logger.log("INFO", "Begin Test:");
            this.GoToURL("http://test.qa.rs/");

            IWebElement LinkLogin = this.MyFindElement(By.LinkText("Login"));
            LinkLogin.Click();

            PopulateInput(By.Name("username"), "Miha.Zavrsni");
            PopulateInput(By.Name("password"), "someComplicatedPassword");
            IWebElement LoginSubmit = this.MyFindElement(By.Name("login"));
            LoginSubmit.Click();

            IWebElement LogoutLink = this.WaitForElement(EC.ElementIsVisible(By.PartialLinkText("Logout")));
            Assert.IsTrue(LogoutLink.Displayed);
            Logger.endTest();

        }

        [STAThread]
        static void Main()
        {
        }

    }
}
