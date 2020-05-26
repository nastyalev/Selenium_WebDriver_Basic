using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Lab
{
    public class Tests
    {
        public static IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:5000");
            driver.Manage().Window.Maximize();
        }

        [SetUp]
        public void Test1_Login()
        {
            driver.FindElement(By.XPath("//*[@id=\"Name\"]")).SendKeys("user");
            driver.FindElement(By.XPath("//*[@id=\"Password\"]")).SendKeys("user");
            driver.FindElement(By.XPath("//*[@type=\"submit\"]")).Click();

            Assert.True(driver.FindElement(By.XPath("//*[text()=\"Home page\"]")).Displayed);
        }

        [Test]
        public void Test2_AddProduct()
        {
            driver.FindElement(By.XPath("//*[@href=\"/Product\"]")).Click();
            driver.FindElement(By.XPath("//*[@href=\"/Product/Create\"]")).Click();

            //input

            driver.FindElement(By.XPath("//input[@id=\"ProductName\"]")).SendKeys("Fortune cookie");
            driver.FindElement(By.XPath("//*[@id=\"CategoryId\"]/*[@value][text()=\"Confections\"]")).Click();
            driver.FindElement(By.XPath("//*[@id=\"SupplierId\"]/*[@value][text()=\"Specialty Biscuits, Ltd.\"]")).Click();
            driver.FindElement(By.XPath("//input[@id=\"UnitPrice\"]")).SendKeys("3");
            driver.FindElement(By.XPath("//input[@id=\"QuantityPerUnit\"]")).SendKeys("10 boxes x 15 pieces");
            driver.FindElement(By.XPath("//input[@id=\"UnitsInStock\"]")).SendKeys("1");
            driver.FindElement(By.XPath("//input[@id=\"UnitsOnOrder\"]")).SendKeys("3");
            driver.FindElement(By.XPath("//input[@id=\"ReorderLevel\"]")).SendKeys("0");

            driver.FindElement(By.XPath("//*[@type=\"submit\"]")).Click();

            Assert.True(driver.FindElement(By.XPath("//*[text()=\"Create new\"]")).Displayed);
            Assert.True(driver.FindElement(By.XPath("//*[text()=\"Fortune cookie\"]")).Displayed);
        }

        [Test]
        public void Test3_OpenAndCheck()
        {
            driver.FindElement(By.XPath("//*[@href=\"/Product\"]")).Click();
            driver.FindElement(By.XPath("//a[@href][text()=\"Fortune cookie\"]")).Click();

            Assert.True(driver.FindElement(By.XPath("//input[@id=\"ProductName\"][@value=\"Fortune cookie\"]")).Displayed);
            Assert.True(driver.FindElement(By.XPath("//*[@id=\"CategoryId\"]/*[@selected][text()=\"Confections\"]")).Displayed);
            Assert.True(driver.FindElement(By.XPath("//*[@id=\"SupplierId\"]/*[@selected][text()=\"Specialty Biscuits, Ltd.\"]")).Displayed);
            Assert.True(driver.FindElement(By.XPath("//input[@id=\"UnitPrice\"][@value=\"3,0000\"]")).Displayed);
            Assert.True(driver.FindElement(By.XPath("//input[@id=\"QuantityPerUnit\"][@value=\"10 boxes x 15 pieces\"]")).Displayed);
            Assert.True(driver.FindElement(By.XPath("//input[@id=\"UnitsInStock\"][@value=\"1\"]")).Displayed);
            Assert.True(driver.FindElement(By.XPath("//input[@id=\"UnitsOnOrder\"][@value=\"3\"]")).Displayed);
            Assert.True(driver.FindElement(By.XPath("//input[@id=\"ReorderLevel\"][@value=\"0\"]")).Displayed);
        }

        [Test]
        public void Test4_DeleteProduct()
        {
            driver.FindElement(By.XPath("//*[@href=\"/Product\"]")).Click();
            driver.FindElement(By.XPath("//*[text()=\"Fortune cookie\"]//..//..//a[text()=\"Remove\"]")).Click();
            driver.SwitchTo().Alert().Accept();

            isElementPresent(By.XPath("//*[text()=\"Fortune cookie\"]"));
        }

        [Test]
        public void Test5_Logout()
        {
            driver.FindElement(By.XPath("//*[text()=\"Logout\"]")).Click();
            Assert.True(driver.FindElement(By.XPath("//*[text()=\"Name\"]")).Displayed);
            Assert.True(driver.FindElement(By.XPath("//*[text()=\"Password\"]")).Displayed);
        }

        public static Boolean isElementPresent(By locator)
        {
            try
            {
                return driver.FindElement(locator).Displayed;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
        }

        [TearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}