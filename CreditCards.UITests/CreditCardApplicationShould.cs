using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Xunit.Abstractions;

namespace CreditCards.UITests
{   
    [Trait("Category", "Applications")]
    public class CreditCardApplicationShould
    {
        private const string homeUrl = "http://localhost:44108/";
        private const string ApplyUrl = "http://localhost:44108/Apply";
        private const string homeTitle = "Home Page - Credit Cards";
        private readonly ITestOutputHelper output; 

        public CreditCardApplicationShould(ITestOutputHelper output)
        {
            this.output = output; 
        } 

        [Fact]
        public void BeInitiatedFromHomePage_NewLowRate()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(homeUrl);

                DemoHelper.Pause();

                IWebElement applyLink = driver.FindElement(By.Name("ApplyLowRate"));
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url); 

            }
        }
        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(homeUrl);

                DemoHelper.Pause();

                IWebElement carouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));
                carouselNext.Click();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1)); //Possible to remove upper carousel clik when set appopriete time this TimeSpan.FromSeconds 
                IWebElement applyLink =
                    wait.Until((d) => d.FindElement(By.LinkText("Easy: Apply Now!")));
                applyLink.Click(); 

                //IWebElement applyLink = driver.FindElement(By.LinkText("Easy: Apply Now!"));
                //applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);

            }
        }
        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication_Prebuilt_Conditions()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(homeUrl);

                DemoHelper.Pause();

                WebDriverWait wait =
                    new WebDriverWait(driver, TimeSpan.FromSeconds(11));
                IWebElement applyLink =
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Easy: Apply Now!")));
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);

            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_CustomerService()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to {homeUrl}");
                driver.Navigate().GoToUrl(homeUrl);
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Setting implicit wait");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(35);

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(35));
                Func<IWebDriver, IWebElement> findEnabledAndVisible = delegate (IWebDriver d)
                {
                    var e = d.FindElement(By.ClassName("customer-service-apply-now"));

                    if (e is null)
                    {
                        throw new NotFoundException();

                    }
                    if (e.Enabled && e.Displayed)
                    {
                        return e;
                    }
                    throw new NotFoundException(); 
                };

                IWebElement applyLink = 
                    wait.Until(findEnabledAndVisible); 




                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to {homeUrl}");
                driver.Navigate().GoToUrl(homeUrl);


                /*IWebElement carouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));
                carouselNext.Click();
                DemoHelper.Pause(1000);
                carouselNext.Click();
                DemoHelper.Pause(1000);

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding element");
                IWebElement applyLink = driver.FindElement(By.ClassName("customer-service-apply-now"));*/
                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element Displayed={applyLink.Displayed} Enabled={applyLink.Enabled}");
                applyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);

            }
        }
        [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(homeUrl);

                DemoHelper.Pause();

                IWebElement randomGreetingApplyLink = driver.FindElement(By.PartialLinkText("- Apply Now!"));
                randomGreetingApplyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);

            }
        }
        [Fact]
        public void BeInitiatedFromHomePage_BySelectingXPath()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(homeUrl);

                DemoHelper.Pause();

                IWebElement randomGreetingApplyLink = driver.FindElement(By.XPath("/html/body/div/div[4]/div/p/a")); // RELATIVE XPATH //a[text()[contains(.,'-Apply Now!')]]
                randomGreetingApplyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);

            }
        }
        [Fact]
        public void BeInitiatedFromHomePage_BySelectingXPath_Relative()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(homeUrl);

                DemoHelper.Pause();

                IWebElement randomGreetingApplyLink = driver.FindElement(By.XPath("//a[text()[contains(.,'-Apply Now!')]]")); // RELATIVE XPATH //a[text()[contains(.,'-Apply Now!')]]
                randomGreetingApplyLink.Click();

                DemoHelper.Pause();

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal(ApplyUrl, driver.Url);

            }
        }
        [Fact]
        public void BeSubmittedWhenValid()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(ApplyUrl);

                /* First method to write IWebElement firstNameField = driver.FindElement(By.Id("FirstName"));
                firstNameField.SendKeys("Sarah"); */
                driver.FindElement(By.Id("FirstName")).SendKeys("Sarah");
                DemoHelper.Pause();
                driver.FindElement(By.Id("LastName")).SendKeys("Wooojcik");
                DemoHelper.Pause();
                driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("123456-A");
                DemoHelper.Pause();
                driver.FindElement(By.Id("Age")).SendKeys("22");
                DemoHelper.Pause();
                driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("50000");
                DemoHelper.Pause();
                driver.FindElement(By.Id("Single")).Click();
                

                IWebElement businessSourceSelectElement =
                    driver.FindElement(By.Id("BusinessSource"));
                SelectElement businessSource = new SelectElement(businessSourceSelectElement);
                //Check default selected option is correct 
                Assert.Equal("I'd Rather Not Say", businessSource.SelectedOption.Text);

                foreach(IWebElement option in businessSource.Options)
                {
                    output.WriteLine($"Vaule: {option.GetAttribute("value")} Text: {option.Text}");
                }
                //Select an option 
                businessSource.SelectByValue("Email");
                DemoHelper.Pause();
                businessSource.SelectByText("Internet Search"); 
                DemoHelper.Pause();
                businessSource.SelectByIndex(4); //Zero Based indexing 
                DemoHelper.Pause();
                driver.FindElement(By.Id("TermsAccepted")).Click();

                //driver.FindElement(By.Id("SubmitApplication")).Click(); First the most basic application submit
                driver.FindElement(By.Id("GrossAnnualIncome")).Submit(); 

                Assert.StartsWith("RefferedToHuman",driver.FindElement(By.Id))

                
            }
        }

    }
}
