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
        /// <param name="api"></param>
        public ShanghaiShenzhenStockApi(TushareProApi api) => this.api = api;

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

            APIRequest request = new APIRequest()
            {
                InterfaceName = "stock_basic",
                Parameters = requestParam,
                Fields = APIInterface.GetAllDataFieldName(typeof(Stock))
            };

            // Try to request and get response
            APIResponse response = await api.Request(request);

            if (response.ResponseCode != ResponseCode.OK)
            {
                throw new Exception(response.ErrorMessage);
            }

            // Try to deserialize the stock information
            return response.GetDataSet<Stock>().First();
        }
    }
}
