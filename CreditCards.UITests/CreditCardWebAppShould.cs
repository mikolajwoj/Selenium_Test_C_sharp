using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {   
        private const string homeUrl = "http://localhost:44108/";
        private const string AboutUrl = "http://localhost:44108/Home/About";
        private const string homeTitle = "Home Page - Credit Cards";
        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadApplicationPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                
                driver.Navigate().GoToUrl(homeUrl);

                DemoHelper.Pause();

                Assert.Equal(homeTitle, driver.Title);
                Assert.Equal(homeUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);

                DemoHelper.Pause();

                driver.Navigate().Refresh();
                Assert.Equal(homeTitle, driver.Title);
                Assert.Equal(homeUrl, driver.Url);


            }

        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnBack()
        {
            using(IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                IWebElement generationTokenElement =
                    driver.FindElement(By.Id("GenerationToken"));
                string initialToken = generationTokenElement.Text; 
                DemoHelper.Pause(); 
                driver.Navigate().GoToUrl(AboutUrl);
                DemoHelper.Pause(); 
                driver.Navigate().Back();
                DemoHelper.Pause();

                Assert.Equal(homeTitle, driver.Title);
                Assert.Equal(homeUrl, driver.Url);
                //TODO: assert that page was reloaded 
                string reloadedToken = driver.FindElement(By.Id("GenerationToken")).Text;
                Assert.NotEqual(initialToken, reloadedToken); 

            }
        }
        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnForwad()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(AboutUrl);
                DemoHelper.Pause();
                
                driver.Navigate().GoToUrl(homeUrl);
                DemoHelper.Pause();
                string initialToken = driver.FindElement(By.Id("GenerationToken")).Text;
                driver.Navigate().Back();
                DemoHelper.Pause();

                driver.Navigate().Forward();
                DemoHelper.Pause();
                string reloadedToken = driver.FindElement(By.Id("GenerationToken")).Text;

                Assert.Equal(homeTitle, driver.Title);
                Assert.Equal(homeUrl, driver.Url);
                Assert.NotEqual(initialToken, reloadedToken);
                //TODO: assert that page was reloaded 


            }
        }
        [Fact]
        [Trait("Category", "Smoke")]
        public void DisplayProductsAndRates()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(homeUrl);
                DemoHelper.Pause();

                ReadOnlyCollection<IWebElement> tableCells = driver.FindElements(By.TagName("td"));
                
                Assert.Equal("Easy Credit Card", tableCells[0].Text);
                Assert.Equal("20% APR", tableCells[1].Text);


                Assert.Equal("Silver Credit Card", tableCells[2].Text);
                Assert.Equal("18% APR", tableCells[3].Text);

                Assert.Equal("Gold Credit Card", tableCells[4].Text);
                Assert.Equal("17% APR", tableCells[5].Text);
                //TODO: Verify corectness of another element ;) 
            }
        }

    }
}
