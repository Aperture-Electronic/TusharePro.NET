using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TusharePro.Interface;

namespace TusharePro.UTest
{
    [TestClass]
    public class CoreFunctionsTest
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
        public void CoreClassInstanseAndSetToken()
        {
            TushareProApi pro = new TushareProApi(TestToken);

            Assert.IsTrue(pro.UserTokenValid);
        }


        [TestMethod]
        public async Task CoreClassRequest()
        {
            TushareProApi pro = new TushareProApi(TestToken);

            APIRequest request = new APIRequest()
            {
                InterfaceName = "daily",
                Parameters = new Dictionary<string, string>()
                {
                    {"ts_code", "603986.SH" },
                    {"trade_date", "20210305" },
                },
                Fields = new List<string>()
                {
                    "open",
                    "high",
                    "low",
                    "close"
                }
            };

            APIResponse response = await pro.Request(request);

            Assert.AreEqual(response.ResponseCode, ResponseCode.OK);

            Console.WriteLine($"Get {response.DataSet.DataSet.Count} data, have {response.DataSet.DataColumn.Count} columns.");
            foreach (string col in response.DataSet.DataColumn)
            {
                Console.WriteLine($"Column: {col}");
            }

            foreach (List<string> row in response.DataSet.DataSet)
            {
                Console.Write("[");
                foreach (string field in row)
                {
                    Console.Write($"{field},");
                }
                Console.WriteLine("]");
            }
        }

        [TestMethod]
        public async Task CoreClassRequestWithoutToken()
        {
            TushareProApi pro = new TushareProApi();

            try
            {
                APIRequest request = new APIRequest()
                {
                    InterfaceName = "daily",
                    Parameters = new Dictionary<string, string>()
                    {
                        {"ts_code", "603986.SH" },
                        {"trade_date", "20210305" },
                    },
                    Fields = new List<string>()
                    {
                        "open",
                        "high",
                        "low",
                        "close"
                    }
                };

                APIResponse response = await pro.Request(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception throwed: {ex.Message}");
                return;
            }

            Assert.Fail("Method does not throw a exception.");
        }

        [TestMethod]
        public async Task CoreClassRequestInvalidField()
        {
            TushareProApi pro = new TushareProApi(TestToken);

            APIRequest request = new APIRequest()
            {
                InterfaceName = "daily",
                Parameters = new Dictionary<string, string>()
                    {
                        {"ts_code", "603986.SH" },
                        {"trade_date", "20210305" },
                    },
                Fields = new List<string>()
                    {
                        "invalid",
                        "open"
                    }
            };

            APIResponse response = await pro.Request(request);

            Assert.AreNotEqual(response.ResponseCode, ResponseCode.OK);
            Console.WriteLine($"Error message got: {response.ErrorMessage}");
        }

        [TestMethod]
        public async Task CoreClassRequestInvalidInterface()
        {
            TushareProApi pro = new TushareProApi(TestToken);

            APIRequest request = new APIRequest()
            {
                InterfaceName = "validInterface",
                Parameters = new Dictionary<string, string>()
                    {
                        {"ts_code", "603986.SH" },
                        {"trade_date", "20210305" },
                    },
                Fields = new List<string>()
                    {
                        "invalid",
                        "open"
                    }
            };

            APIResponse response = await pro.Request(request);

            Assert.AreNotEqual(response.ResponseCode, ResponseCode.OK);
            Console.WriteLine($"Error message got: {response.ErrorMessage}");
        }
    }
}
