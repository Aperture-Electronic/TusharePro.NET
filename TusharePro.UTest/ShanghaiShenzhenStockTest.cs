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
    }
}
