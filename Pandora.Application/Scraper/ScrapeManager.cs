using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pandora.Application.Scraper
{
    public static class ScrapeManager
    {
        private static IWebDriver driver;

        private static readonly string CoinMarketCapUrl = "https://coinmarketcap.com/";
        private static readonly string TradingViewUrl = "https://www.tradingview.com/markets/cryptocurrencies/prices-all/";

        private static decimal bitcoin;
        private static decimal etherium;
        private static decimal litecoin;
        private static decimal zcash;

        public static decimal BitcoinPrice
        {
            get
            {
                return bitcoin;
            }
        }
        public static decimal EtheriumPrice
        {
            get
            {
                return etherium;
            }
        }
        public static decimal LitecoinPrice
        {
            get
            {
                return litecoin;
            }
        }
        public static decimal ZCashPrice
        {
            get
            {
                return zcash;
            }
        }

        public static void Start()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("no-sandbox");
            options.AddArgument("headless");

            driver = new ChromeDriver(Environment.CurrentDirectory, options, TimeSpan.FromMinutes(3));
        }
        public static async Task UpdatePrices()
        {
            try
            {
                await UpdateWithCoinMarketCap();
            }
            catch (Exception)
            {
                try
                {
                    await UpdateWithTradingView();
                }
                catch (Exception)
                {

                }
            }
        }
        private static async Task UpdateWithCoinMarketCap()
        {
            await Task.Run(() =>
            {
                try
                {
                     driver.Navigate().GoToUrl(CoinMarketCapUrl);
                IWebElement tableElement = driver.FindElement(By.ClassName("cmc-table"));
                IWebElement tableBodyElement = tableElement.FindElement(By.TagName("tbody"));
                ReadOnlyCollection<IWebElement> allRows = tableBodyElement.FindElements(By.TagName("tr"));

                foreach (IWebElement row in allRows)
                {
                    ReadOnlyCollection<IWebElement> cells = row.FindElements(By.TagName("td"));
                    var decision = cells[2].Text;
                    switch (decision)
                    {
                        case string a when Regex.IsMatch(a, @"\bBTC\b", RegexOptions.IgnoreCase):
                            bitcoin = decimal.Parse(cells[3].Text.Replace("$", string.Empty));
                            break;
                        case string a when Regex.IsMatch(a, @"\bETH\b", RegexOptions.IgnoreCase):
                            etherium = decimal.Parse(cells[3].Text.Replace("$", string.Empty));
                            break;
                        case string a when Regex.IsMatch(a, @"\bLTC\b", RegexOptions.IgnoreCase):
                            litecoin = decimal.Parse(cells[3].Text.Replace("$", string.Empty));
                            break;
                        case string a when a.Contains("Zcash"):
                            zcash = decimal.Parse(cells[3].Text.Replace("$", string.Empty));
                            break;
                    }
                }
                }
                catch (System.Exception e)
                {
                }
            });
        }
        private static async Task UpdateWithTradingView()
        {
            await Task.Run(() =>
            {
                driver.Navigate().GoToUrl(TradingViewUrl);
                IWebElement divElement = driver.FindElement(By.ClassName("tv-screener__content-pane"));
                IWebElement tableElement = divElement.FindElement(By.TagName("table"));
                IWebElement tableBodyElement = tableElement.FindElement(By.TagName("tbody"));
                ReadOnlyCollection<IWebElement> allRows = tableBodyElement.FindElements(By.TagName("tr"));

                foreach (IWebElement row in allRows)
                {
                    ReadOnlyCollection<IWebElement> cells = row.FindElements(By.TagName("td"));
                    var decision = cells[0].Text;
                    switch (decision)
                    {
                        case "Bitcoin":
                            bitcoin = decimal.Parse(cells[3].Text.Replace("$", string.Empty));
                            break;
                        case "Ethereum":
                            etherium = decimal.Parse(cells[3].Text.Replace("$", string.Empty));
                            break;
                        case "Litecoin":
                            litecoin = decimal.Parse(cells[3].Text.Replace("$", string.Empty));
                            break;
                        case "Zcash":
                            zcash = decimal.Parse(cells[3].Text.Replace("$", string.Empty));
                            break;
                    }
                }
            });
        }
        public static void Close()
        {
            driver.Close();
        }
    }
}
