using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;


namespace HT_5_1
{
    [TestFixture]
    public class HT_5_1
    {

        IWebDriver driver;
        public static ExtentTest test;
        public static ExtentReports extent;

        [OneTimeSetUp]

        public void BeforeTest()
        {

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            extent = new ExtentReports();
            var extendReportHtml = new ExtentHtmlReporter(@"D:\study\code\DataArt\.vs\HT_5.1\ExtendReportsHtml\");
            extent.AttachReporter(extendReportHtml);
            extent.AddSystemInfo("Host Name", "Vlad");
            extent.AddSystemInfo("User Name", "Vladyslav Shreder");
        }

        [OneTimeTearDown]

        public void AfterTest()
        {
           // driver.Quit();
            extent.Flush();
        }
        
        [Test]
        public void LoginTest()
        {
            test = null;
            test = extent.CreateTest("HT_5_1").Info("LogInTest_1 starts");
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            string loginTestUrl = "https://www.demoblaze.com/index.html";
            
            driver.Navigate().GoToUrl(loginTestUrl);
            wait.Until(x => x.FindElement(By.Id("login2")).Displayed);
            test.Log(Status.Info, "Page Displays");

            IWebElement loginButtonId = driver.FindElement(By.Id("login2"));
            IWebElement loginButtonXPath = driver.FindElement(By.XPath("//a[text() = 'Log in']"));
            IWebElement loginButtonCss = driver.FindElement(By.CssSelector("#login2"));

            loginButtonId.Click();
            wait.Until(x => x.FindElement(By.XPath("//input[@id = 'loginusername']")).Displayed);
            test.Log(Status.Info, "'Log In' overlay was opend");

            IWebElement usernameBoxId = driver.FindElement(By.Id("loginusername"));
            IWebElement usernameBoxXPath = driver.FindElement(By.XPath("//input[@id = 'loginusername']"));
            IWebElement usernameBoxCss = driver.FindElement(By.CssSelector("#loginusername"));
            usernameBoxId.Click();
            usernameBoxId.Clear();
            usernameBoxId.SendKeys("TesterVlad");

            IWebElement passwordBoxId = driver.FindElement(By.Id("loginpassword"));
            IWebElement passwordBoxXPath = driver.FindElement(By.XPath("//input[@id = 'loginpassword' and @type = 'password']"));
            IWebElement passwordBoxCss = driver.FindElement(By.CssSelector("#loginpassword"));
            passwordBoxId.Click();
            passwordBoxId.Clear();
            passwordBoxCss.SendKeys("Tester123");

            IWebElement loginBoxXPath = driver.FindElement(By.XPath("//button[@onclick = 'logIn()']"));
            IWebElement loginBoxCss = driver.FindElement(By.CssSelector("button[onclick = 'logIn()']"));
            loginBoxXPath.Click();
            test.Log(Status.Info, "Product page was opened");

            try
            {
                wait.Until(x => x.FindElement(By.Id("nameofuser")).Displayed);
                string nameOfUser = driver.FindElement(By.CssSelector("#nameofuser")).Text;
                Assert.True(driver.FindElement(By.XPath("//*[@id = 'nameofuser']")).Displayed, "Name of user is not displayed");
                Assert.AreEqual(nameOfUser, "Welcome TesterVlad", "Username is different");

                wait.Until(x => x.FindElement(By.Id("logout2")).Displayed);
                Assert.True(driver.FindElement(By.XPath("//a[@onclick = 'logOut()']")).Displayed, "Logout button is not displayed");
                test.Log(Status.Pass, "'Log out' button and username are present on the page");
            }
            catch
            {
                test.Log(Status.Fail, "Username is different or 'Log out' button missed");

                throw;

            }
            

        }


    }
        
  
}