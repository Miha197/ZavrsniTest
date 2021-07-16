using TestLogger;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;
using Randomize;
using System.Collections.Generic;


namespace AutomatedTests
{
    [TestFixture]
    [Parallelizable]
    class Tests : BaseTest
    {
        private const string EtsyUrl = "https://www.etsy.com/";
        private const string EmailTemplate = "@gmail.com";
        private string RegisteredEmail = string.Empty;
        private string RegisteredUsername = string.Empty;
        private string RegisteredPassword = string.Empty;

       

        [SetUp]
        public void SetUp()
        {            
            Driver = new ChromeDriver();        
            Driver.Manage().Window.Maximize();
            Wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 10));
        }

        [TearDown]
        public void TearDown()
        {
                Driver.Close();
        }

        /// <summary>
        /// Functionality - Registering And Login
        /// TC 006
        /// Name - Register on Etsy Website
        /// </summary>
        [Test, Order(1)]
        [Category(EtsyUrl)]
        public void TestRegister()
        {
            Logger.BeginTest("TestRegister");
            GoToURL(EtsyUrl);

            IWebElement SignInLink = FindMyElement(By.XPath("/html/body/div[3]/header/div[4]/nav/ul/li[1]/button"));
            SignInLink.Click();
            ExplicitWait(500);

            RegisteredEmail = RandomizeHelper.GenerateRandomString(10) + EmailTemplate;
            PopulateInput(By.Name("email"), RegisteredEmail);
            ExplicitWait(250);

            IWebElement Submit = FindMyElement(By.Name("submit_attempt"));
            Submit.Click();

            IWebElement FirstNameCheck = WaitForElement(EC.ElementIsVisible(By.Id("join_neu_first_name_field")));
            Assert.IsTrue(FirstNameCheck.Displayed);
            Logger.EndTest();

            RegisteredUsername = RandomizeHelper.GenerateRandomString(15);
            PopulateInput(By.Id("join_neu_first_name_field"), RegisteredUsername);

            RegisteredPassword = RandomizeHelper.GenerateRandomAlphanumeric(15);
            PopulateInput(By.Id("join_neu_password_field"), RegisteredPassword);

            IWebElement RegisterBtn = FindMyElement(By.Name("submit_attempt"));
            RegisterBtn.Click();
        }

        /// <summary>
        /// Functionality - Registering And Login
        /// TC 001
        /// Name - Login with valid email and password
        /// </summary>
        [Test, Order(2)]
        [Category(EtsyUrl)]
        public void TestLogin()
        {
            Logger.BeginTest("TestLogin");
            GoToURL(EtsyUrl);

            IWebElement SignInLink = FindMyElement(By.XPath("/html/body/div[3]/header/div[4]/nav/ul/li[1]/button"));
            SignInLink.Click();
            ExplicitWait(500);

            PopulateInput(By.Name("email"), RegisteredEmail);
            ExplicitWait(250);

            IWebElement Submit = FindMyElement(By.Name("submit_attempt"));
            Submit.Click();

            IWebElement H1Check = WaitForElement(EC.ElementIsVisible(By.ClassName("wt-text-title-02")));
            Assert.IsTrue(H1Check.Displayed);
            Logger.EndTest();
            ExplicitWait(1000);

            PopulateInput(By.Id("join_neu_password_field"), RegisteredPassword);

            IWebElement SignInBtn = FindMyElement(By.Name("submit_attempt"));
            SignInBtn.Click();

            IWebElement WelcomeBackCheck = WaitForElement(EC.ElementIsVisible(By.ClassName("welcome-message-text")));
            Assert.IsTrue(WelcomeBackCheck.Displayed);
            Logger.EndTest();
        }

        /// <summary>
        /// Functionality - Browsing And Searching For Items
        /// TC 001
        /// Name - Browsing through categories
        /// </summary>
        [Test]
        [Category(EtsyUrl)]
        public void BrowsingThroughCategoriesTest()
        {
            GoToURL(EtsyUrl);

            IWebElement CategoryLink = FindMyElement(By.PartialLinkText("Jewelry"));
            CategoryLink.Click();

            ExplicitWait(500);

            IWebElement CategorySelect = FindMyElement(By.ClassName("display-block"));
            CategorySelect.Click();

            IWebElement SubcategorySelectLink = FindMyElement(By.PartialLinkText("Keychains"));
            SubcategorySelectLink.Click();

            IWebElement SubcategoryPageDisplayed = WaitForElement(EC.ElementIsVisible(By.XPath("//H1/self::H1")));
            Assert.IsTrue(SubcategoryPageDisplayed.Displayed);
        }

        /// <summary>
        /// Functionality - Browsing And Searching For Items
        /// TC 002, 003
        /// Name - Searching for items (Empty search and Good search)
        /// </summary>
        [Test]
        [Category(EtsyUrl)]
        public void TestEtsySearchBar()
        {
            Logger.BeginTest("TestEtsySearchBar");

            GoToURL(EtsyUrl);

            IWebElement SearchBar = FindMyElement(By.Name("search_query"));
            SearchBar.SendKeys("Handbags");

            IWebElement SubmitSearch = FindMyElement(By.XPath("/html/body/div[3]/header/div[2]/div/form/div/div[1]/button"));
            SubmitSearch.Click();

            IWebElement HandbagLink = WaitForElement(EC.ElementIsVisible(By.PartialLinkText("handbags")));
            Assert.IsTrue(HandbagLink.Displayed);

            Logger.EndTest();
        }

        /// <summary>
        /// Functionality - Filtering And Adding Items To Cart
        /// TC 001, 002 
        /// Name - Filter functionality
        /// </summary>
        [Test]
        [Category(EtsyUrl)]
        public void TestFilteringOnBrowser()
        {
            Logger.BeginTest("TestFilteringOnBrowser");

            BrowsingThroughCategoriesTest();

            ExplicitWait(7500);

            IWebElement FilterCheck = FindMyElement(By.PartialLinkText("1 business"));
            FilterCheck.Click();

            ExplicitWait(500);           

            IWebElement priceRange = FindMyElement(By.LinkText("Under USD 25"));
            priceRange.Click();

            IWebElement RadioBtnFilterCheck = FindMyElement(By.LinkText("At Most USD 25"));
            Assert.IsTrue(RadioBtnFilterCheck.Displayed);
            RadioBtnFilterCheck.Click();
            ExplicitWait(500);

            IWebElement ShipSelect = FindMyElement(By.Name("ship_to"));
            SelectElement DropDownShipSelect = new SelectElement(ShipSelect);
            DropDownShipSelect.SelectByValue("GR");

            IWebElement ShipSelectCheck = FindMyElement(By.LinkText("Ships to Greece"));
            Assert.IsTrue(ShipSelectCheck.Displayed);

            Logger.EndTest();
        }

        [STAThread]
        static void Main()
        { }
    }
}
