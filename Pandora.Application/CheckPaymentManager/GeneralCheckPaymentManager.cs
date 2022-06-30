using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Pandora.Application.CheckPaymentManager
{
    public class GeneralCheckPaymentManager : ICheckPaymentManager
    {
        private string TransactionId { get; }
        private static IWebDriver driver;

        private static readonly string url = "https://blockchair.com/";

        public GeneralCheckPaymentManager(string transactionId)
        {
            TransactionId = transactionId;
        }

        public ResultCheckPaymentModel CheckResult()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("no-sandbox");
            options.AddArgument("headless");

            driver = new ChromeDriver(Environment.CurrentDirectory, options, TimeSpan.FromMinutes(3));
            driver.Navigate().GoToUrl(url);
            
            IWebElement divElement = driver.FindElement(By.CssSelector("input[id*='searchbar__input']"));
            divElement.SendKeys(TransactionId);
            divElement.Submit();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(90));
            IWebElement el = wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div[id='tx-status']")));
            var elClass = el.GetAttribute("class");
            if (elClass== "__complete")
            {
                IWebElement spanPaymentAmount = driver.FindElements(By.CssSelector("span[class='wb-ba']")).First();
                ReadOnlyCollection<IWebElement> addresses = driver.FindElements(By.CssSelector("a[class='hash mr-5 d-inline va-mid']"));

                return new ResultCheckPaymentModel
                {
                    PaymentAmount = Convert.ToDecimal(spanPaymentAmount.Text),
                    FromAddress = addresses.First().Text,
                    ToAddress = addresses.Last().Text,
                    IsConfirmed = true
                };
            }

            return new ResultCheckPaymentModel()
            {
                IsConfirmed = false
            };
        }
    }
}