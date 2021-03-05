using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TusharePro.Interface;
using TusharePro.ShanghaiShenzhenStock;

namespace TusharePro.ShanghaiShenzhenStock
{
    public class ShanghaiShenzhenStockApi
    {
        /// <summary>
        /// 引用API接口
        /// </summary>
        private readonly TushareProApi api;

        /// <summary>
        /// 由TusharePro API接口创建访问沪深股票信息的API子接口
        /// </summary>
        /// <param name="api">TusharePro API接口</param>
        public ShanghaiShenzhenStockApi(TushareProApi api) => this.api = api;

        /// <summary>
        /// 根据参数获取沪深股票的基本信息
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <returns>股票的基本信息</returns>
        private async Task<List<Stock>> BasicInfomation(Dictionary<string, string> parameters)
        {
            APIRequest request = new APIRequest()
            {
                InterfaceName = "stock_basic",
                Parameters = parameters,
                Fields = APIInterface.GetAllDataFieldName(typeof(Stock))
            };

            // Try to request and get response
            APIResponse response = await api.Request(request);

            if (response.ResponseCode != ResponseCode.OK)
            {
                throw new Exception(response.ErrorMessage);
            }

            // Try to deserialize the stock information
            return response.GetDataSet<Stock>();
        }

        /// <summary>
        /// 获取一只沪深股票的基本信息
        /// </summary>
        /// <param name="tsCode">股票TS代码（股票代码.市场）</param>
        /// <returns>股票的基本信息</returns>
        public async Task<Stock> BasicInformation(string tsCode)
        {
            string userToken = api.UserToken;

            // Generate the request
            Dictionary<string, string> requestParam = new Dictionary<string, string>()
            {
                {"ts_code", tsCode }
            };

            return (await BasicInfomation(requestParam)).First();
        }

        /// <summary>
        /// 获取相应上市状态的所有沪深股票的基本信息
        /// </summary>
        /// <param name="status">上市状态</param>
        /// <returns>股票的基本信息</returns>
        public async Task<List<Stock>> BasicInformation(ListStatus status)
        {
            string userToken = api.UserToken;

            // Generate the request
            string[] statusCode = {"L", "D", "P" };
            Dictionary<string, string> requestParam = new Dictionary<string, string>()
            {
                {"list_status", statusCode[(int)status]}
            };

            return await BasicInfomation(requestParam);
        }

        /// <summary>
        /// 获取相应交易所的所有沪深股票的基本信息
        /// </summary>
        /// <param name="exchange">交易所</param>
        /// <returns>股票的基本信息</returns>
        public async Task<List<Stock>> BasicInformation(Exchange exchange)
        {
            string userToken = api.UserToken;

            // Generate the request
            string[] exchangeCode = { "SSE", "SZSE"};
            Dictionary<string, string> requestParam = new Dictionary<string, string>()
            {
                {"exchange", exchangeCode[(int)exchange]}
            };

            return await BasicInfomation(requestParam);
        }

        /// <summary>
        /// 获取具有相应沪深港通标的的所有沪深股票的基本信息
        /// </summary>
        /// <param name="shSzHkConnect">沪深港通标的状态</param>
        /// <returns>股票的基本信息</returns>
        public async Task<List<Stock>> BasicInformation(ShSzHkConnect shSzHkConnect)
        {
            string userToken = api.UserToken;

            // Generate the request
            string[] exchangeCode = { "N", "H", "S" };
            Dictionary<string, string> requestParam = new Dictionary<string, string>()
            {
                {"is_hs", exchangeCode[(int)shSzHkConnect]}
            };

            return await BasicInfomation(requestParam);
        }
    }
}
