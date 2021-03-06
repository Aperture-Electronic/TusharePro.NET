using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TusharePro.ShanghaiShenzhenStock;

namespace TusharePro.UTest
{
    [TestClass]
    public class ShanghaiShenzhenStockTest
    {
        private static string TestToken;

        [TestInitialize]
        public async Task GetTestToken()
        {
            FileStream fs = File.OpenRead("../../../testToken.txt");
            StreamReader sr = new StreamReader(fs);

            TestToken = await sr.ReadLineAsync();

            sr.Close();
        }

        [TestMethod]
        public async Task ShSzStockClassInstanseAndGetBasicInfo()
        {
            TushareProApi pro = new TushareProApi(TestToken);
            ShanghaiShenzhenStockApi shSzApi = new ShanghaiShenzhenStockApi(pro);

            Stock stockInfo = await shSzApi.BasicInformation("603986.SH");
            Assert.AreEqual(stockInfo.Code, "603986");
            Assert.AreEqual(stockInfo.Market, Market.Main);
        }

        [TestMethod]
        public async Task ShSzStockClassGetMultiBasicInfo()
        {
            TushareProApi pro = new TushareProApi(TestToken);
            ShanghaiShenzhenStockApi shSzApi = new ShanghaiShenzhenStockApi(pro);

            List<Stock> stocks = await shSzApi.BasicInformation(ListStatus.Delisting);
            Assert.IsTrue(stocks.Count > 0);
            Console.WriteLine($"There are {stocks.Count} delisted stocks");
        }

        [TestMethod]
        public async Task ShSzStockClassGetExchangeBasicInfo()
        {
            TushareProApi pro = new TushareProApi(TestToken);
            ShanghaiShenzhenStockApi shSzApi = new ShanghaiShenzhenStockApi(pro);

            List<Stock> stocks = await shSzApi.BasicInformation(Exchange.SZSE);
            Assert.IsTrue(stocks.Count > 0);
            Console.WriteLine($"SZSE has {stocks.Count} stocks");
        }

        [TestMethod]
        public async Task ShSzStockClassGetShSzHkBasicInfo()
        {
            TushareProApi pro = new TushareProApi(TestToken);
            ShanghaiShenzhenStockApi shSzApi = new ShanghaiShenzhenStockApi(pro);

            List<Stock> stocks = await shSzApi.BasicInformation(ShSzHkConnect.Shenzhen);
            Assert.IsTrue(stocks.Count > 0);
            Console.WriteLine($"Shenzhen-Hongkong connect has {stocks.Count} stocks");
        }

        [TestMethod]
        public async Task ShSzStockClassGetTradeDaysInRange()
        {
            TushareProApi pro = new TushareProApi(TestToken);
            ShanghaiShenzhenStockApi shSzApi = new ShanghaiShenzhenStockApi(pro);
            List<TradeDay> tradeDays = await shSzApi.TradeCalendar(Exchange.SSE, new DateTime(2021, 1, 1), DateTime.Now);
            Assert.IsTrue(tradeDays.Count > 0);
            foreach (TradeDay day in tradeDays)
            {
                Console.WriteLine(day);
            }
        }

        [TestMethod]
        public async Task ShSzStockClassGetTradeDaysInRangeAndSpecificStatus()
        {
            TushareProApi pro = new TushareProApi(TestToken);
            ShanghaiShenzhenStockApi shSzApi = new ShanghaiShenzhenStockApi(pro);
            List<TradeDay> tradeDays = await shSzApi.TradeCalendar(Exchange.SZSE, new DateTime(2021, 1, 1), DateTime.Now, TradeStatus.Closing);
            Assert.IsTrue(tradeDays.Count > 0);
            foreach (TradeDay day in tradeDays)
            {
                Console.WriteLine(day);
            }
        }

        [TestMethod]
        public async Task ShSzStockClassGetNameChangeHistory()
        {
            TushareProApi pro = new TushareProApi(TestToken);
            ShanghaiShenzhenStockApi shSzApi = new ShanghaiShenzhenStockApi(pro);
            List<NameHistory> histories = await shSzApi.NameChange("600848.SH");
            Assert.IsTrue(histories.Count > 0);
            foreach(NameHistory history in histories)
            {
                Console.WriteLine(history);
            }
        }

        [TestMethod]
        public async Task ShSzStockClassGetShSzHkConnectConstituent()
        {
            TushareProApi pro = new TushareProApi(TestToken);
            ShanghaiShenzhenStockApi shSzApi = new ShanghaiShenzhenStockApi(pro);
            List<ShSzHkConnectConstituent> constituents = await shSzApi.ShSzHkConnectConstituents(ShSzHkConnect.Shanghai);
            Assert.IsTrue(constituents.Count > 0);
            foreach (ShSzHkConnectConstituent constituent in constituents)
            {
                Console.WriteLine(constituent);
            }
        }

        [TestMethod]
        public async Task ShSzStockClassGetStockCompanyInfo()
        {
            TushareProApi pro = new TushareProApi(TestToken);
            ShanghaiShenzhenStockApi shSzApi = new ShanghaiShenzhenStockApi(pro);
            List<StockCompany> infos = await shSzApi.StockCompanyInformations(new string[] { "603986.SH", "000725.SZ"});
            Assert.IsTrue(infos.Count > 0);
            foreach (var info in infos)
            {
                Console.WriteLine(info);
            }
        }

        [TestMethod]
        public async Task ShSzStockClassGetStockCompanyManagers()
        {
            TushareProApi pro = new TushareProApi(TestToken);
            ShanghaiShenzhenStockApi shSzApi = new ShanghaiShenzhenStockApi(pro);
            List<StockCompanyManager> managers = await shSzApi.StockCompanyManagers(new string[] { "603986.SH", "000725.SZ" });
            Assert.IsTrue(managers.Count > 0);
            foreach (var man in managers)
            {
                Console.WriteLine(man);
            }
        }

        [TestMethod]
        public async Task ShSzStockClassGetStockCompanyManagerRewards()
        {
            TushareProApi pro = new TushareProApi(TestToken);
            ShanghaiShenzhenStockApi shSzApi = new ShanghaiShenzhenStockApi(pro);
            List<StockCompanyManagerRewards> managers = await shSzApi.StockCompanyManagerRewards(new string[] { "603986.SH", "000725.SZ" });
            Assert.IsTrue(managers.Count > 0);
            foreach (var man in managers)
            {
                Console.WriteLine(man);
            }
        }

        [TestMethod]
        public async Task ShSzStockClassGetStockCompanyManagerAndRewards()
        {
            TushareProApi pro = new TushareProApi(TestToken);
            ShanghaiShenzhenStockApi shSzApi = new ShanghaiShenzhenStockApi(pro);
            var managers = await shSzApi.StockCompanyManagerAndRewards(new string[] { "603986.SH", "000725.SZ" });
            Assert.IsTrue(managers.Count > 0);
            foreach ((StockCompanyManager, StockCompanyManagerRewards) man in managers)
            {
                StockCompanyManager manager;
                StockCompanyManagerRewards rewards;
                (manager, rewards) = man;
                Console.WriteLine(manager);
                Console.WriteLine(rewards);
            }
        }
    }
}
