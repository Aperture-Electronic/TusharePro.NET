// TusharePro.NET
// Copyright(C) 2021 Aperture Electronic
// API Copyright(C) Tushare which is following BSD-3-Clause License.

//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or any later version.

//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TusharePro.Interface;
using static TusharePro.ShanghaiShenzhenStock.EnumerationNames;

/// <summary>
/// 沪深股票数据接口命名空间
/// </summary>
namespace TusharePro.ShanghaiShenzhenStock
{
    /// <summary>
    /// 沪深股票数据接口
    /// </summary>
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
        public async Task<Stock> BasicInformation(string tsCode) => 
            (await BasicInfomation(new Dictionary<string, string>()
            {
                {"ts_code", tsCode }
            })).First();

        /// <summary>
        /// 获取相应上市状态的所有沪深股票的基本信息
        /// </summary>
        /// <param name="status">上市状态</param>
        /// <returns>股票的基本信息</returns>
        public async Task<List<Stock>> BasicInformation(ListStatus status) =>
            await BasicInfomation(new Dictionary<string, string>()
            {
                {"list_status", ListStatusNamesMap.Split(',')[(int)status]}
            });

        /// <summary>
        /// 获取相应交易所的所有沪深股票的基本信息
        /// </summary>
        /// <param name="exchange">交易所</param>
        /// <returns>股票的基本信息</returns>
        public async Task<List<Stock>> BasicInformation(Exchange exchange) =>
            await BasicInfomation(new Dictionary<string, string>()
            {
                {"exchange", ExchangeNamesMap.Split(',')[(int)exchange]}
            });

        /// <summary>
        /// 获取具有相应沪深港通标的的所有沪深股票的基本信息
        /// </summary>
        /// <param name="shSzHkConnect">沪深港通标的状态</param>
        /// <returns>股票的基本信息</returns>
        public async Task<List<Stock>> BasicInformation(ShSzHkConnect shSzHkConnect) => 
            await BasicInfomation(new Dictionary<string, string>()
            {
                {"is_hs", ShSzHkConnectNamesMap.Split(',')[(int)shSzHkConnect]}
            });

        /// <summary>
        /// 获取某交易所某时段内的交易日历
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <returns>交易日历</returns>
        private async Task<List<TradeDay>> TradeCalendar(Dictionary<string, string> parameters)
        {
            APIRequest request = new APIRequest()
            {
                InterfaceName = "trade_cal",
                Parameters = parameters,
                Fields = APIInterface.GetAllDataFieldName(typeof(TradeDay))
            };

            // Try to request and get response
            APIResponse response = await api.Request(request);

            if (response.ResponseCode != ResponseCode.OK)
            {
                throw new Exception(response.ErrorMessage);
            }

            // Try to deserialize the stock information
            return response.GetDataSet<TradeDay>();
        }

        /// <summary>
        /// 获取某交易所某时段内的交易日历
        /// </summary>
        /// <param name="exchange">交易所</param>
        /// <param name="startDate">时段开始日期</param>
        /// <param name="endDate">时段结束日期</param>
        /// <returns>交易日历</returns>
        public async Task<List<TradeDay>> TradeCalendar(Exchange exchange, DateTime startDate, DateTime endDate) =>
            await TradeCalendar(new Dictionary<string, string>()
            {
                {"exchange", ExchangeNamesMap.Split(',')[(int)exchange]},
                {"start_date", startDate.ToString("yyyyMMdd") },
                {"end_date", endDate.ToString("yyyyMMdd") },
            });

        /// <summary>
        /// 获取某交易所某时段内对应交易状态的交易日历
        /// </summary>
        /// <param name="exchange">交易所</param>
        /// <param name="startDate">时段开始日期</param>
        /// <param name="endDate">时段结束日期</param>
        /// <param name="tradeStatus">交易状态</param>
        /// <returns>交易日历</returns>
        public async Task<List<TradeDay>> TradeCalendar(Exchange exchange, DateTime startDate, DateTime endDate, TradeStatus tradeStatus) =>
            await TradeCalendar(new Dictionary<string, string>()
            {
                {"exchange", ExchangeNamesMap.Split(',')[(int)exchange]},
                {"start_date", startDate.ToString("yyyyMMdd") },
                {"end_date", endDate.ToString("yyyyMMdd") },
                {"is_open", TradeStatusNamesMap.Split(',')[(int)tradeStatus]}
            });

        /// <summary>
        /// 历史名称变更记录
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <returns>股票的历史名称变更记录</returns>
        private async Task<List<NameHistory>> NameChange(Dictionary<string, string> parameters)
        {
            APIRequest request = new APIRequest()
            {
                InterfaceName = "namechange",
                Parameters = parameters,
                Fields = APIInterface.GetAllDataFieldName(typeof(NameHistory))
            };

            // Try to request and get response
            APIResponse response = await api.Request(request);

            if (response.ResponseCode != ResponseCode.OK)
            {
                throw new Exception(response.ErrorMessage);
            }

            // Try to deserialize the stock information
            return response.GetDataSet<NameHistory>();
        }

        /// <summary>
        /// 历史名称变更记录
        /// </summary>
        /// <param name="tsCode">股票TS代码（股票代码.市场）</param>
        /// <returns>股票的历史名称变更记录</returns>
        public async Task<List<NameHistory>> NameChange(string tsCode) =>
        await NameChange(new Dictionary<string, string>()
            {
                {"ts_code", tsCode }
            });


        /// <summary>
        /// 沪股通、深股通成分数据
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <returns>成分数据</returns>
        private async Task<List<ShSzHkConnectConstituent>> ShSzHkConnectConstituents(Dictionary<string, string> parameters)
        {
            APIRequest request = new APIRequest()
            {
                InterfaceName = "hs_const",
                Parameters = parameters,
                Fields = APIInterface.GetAllDataFieldName(typeof(ShSzHkConnectConstituent))
            };

            // Try to request and get response
            APIResponse response = await api.Request(request);

            if (response.ResponseCode != ResponseCode.OK)
            {
                throw new Exception(response.ErrorMessage);
            }

            // Try to deserialize the stock information
            return response.GetDataSet<ShSzHkConnectConstituent>();
        }

        /// <summary>
        /// 沪股通、深股通成分数据
        /// </summary>
        /// <param name="connect">需要请求的标的</param>
        /// <returns>成分数据</returns>
        public async Task<List<ShSzHkConnectConstituent>> ShSzHkConnectConstituents(ShSzHkConnect connect) =>
            await ShSzHkConnectConstituents(new Dictionary<string, string>()
            {
                {"hs_type", ShSzHkConnectTypeNamesMap.Split(',')[(int)connect]}
            });

        /// <summary>
        /// 获取上市公司基础信息
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <returns>基础信息表</returns>
        private async Task<List<StockCompany>> StockCompanyInformations(Dictionary<string, string> parameters)
        {
            APIRequest request = new APIRequest()
            {
                InterfaceName = "stock_company",
                Parameters = parameters,
                Fields = APIInterface.GetAllDataFieldName(typeof(StockCompany))
            };

            // Try to request and get response
            APIResponse response = await api.Request(request);

            if (response.ResponseCode != ResponseCode.OK)
            {
                throw new Exception(response.ErrorMessage);
            }

            // Try to deserialize the stock information
            return response.GetDataSet<StockCompany>();
        }

        /// <summary>
        /// 获取上市公司基础信息
        /// </summary>
        /// <param name="tsCode">股票TS代码（股票代码.市场）</param>
        /// <returns>基础信息表</returns>
        public async Task<List<StockCompany>> StockCompanyInformations(string tsCode) => await StockCompanyInformations(new string[] { tsCode });

        /// <summary>
        /// 获取上市公司基础信息
        /// </summary>
        /// <param name="tsCodes">股票TS代码列表（股票代码.市场）</param>
        /// <returns>基础信息表</returns>
        public async Task<List<StockCompany>> StockCompanyInformations(IEnumerable<string> tsCodes) =>
            await StockCompanyInformations(new Dictionary<string, string>()
            {
                {"ts_code", string.Join(',', tsCodes) }
            });

        /// <summary>
        /// 获取对应交易所所有上市公司的基础信息
        /// </summary>
        /// <param name="tsCode">股票TS代码列表（股票代码.市场）</param>
        /// <returns>基础信息表</returns>
        public async Task<List<StockCompany>> StockCompanyInformations(Exchange exchange) =>
            await StockCompanyInformations(new Dictionary<string, string>()
            {
                {"exchange", ExchangeNamesMap.Split(',')[(int)exchange]},
            });

        /// <summary>
        /// 获得上市公司高管信息
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <returns>高管信息表</returns>
        public async Task<List<StockCompanyManager>> StockCompanyManagers(Dictionary<string, string> parameters)
        {
            APIRequest request = new APIRequest()
            {
                InterfaceName = "stk_managers",
                Parameters = parameters,
                Fields = APIInterface.GetAllDataFieldName(typeof(StockCompanyManager))
            };

            // Try to request and get response
            APIResponse response = await api.Request(request);

            if (response.ResponseCode != ResponseCode.OK)
            {
                throw new Exception(response.ErrorMessage);
            }

            // Try to deserialize the stock information
            return response.GetDataSet<StockCompanyManager>();
        }

        /// <summary>
        /// 获得上市公司高管信息
        /// </summary>
        /// <param name="tsCodes">股票TS代码（股票代码.市场）</param>
        /// <returns>高管信息表</returns>
        public async Task<List<StockCompanyManager>> StockCompanyManagers(string tsCode) => await StockCompanyManagers(new string[] { tsCode});

        /// <summary>
        /// 获得上市公司高管信息
        /// </summary>
        /// <param name="tsCodes">股票TS代码列表（股票代码.市场）</param>
        /// <returns>高管信息表</returns>
        public async Task<List<StockCompanyManager>> StockCompanyManagers(IEnumerable<string> tsCodes) =>
            await StockCompanyManagers(new Dictionary<string, string>()
            {
                {"ts_code", string.Join(',', tsCodes) }
            });

        /// <summary>
        /// 获得上市公司高管薪酬及持股信息
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <returns>高管薪酬及持股信息表</returns>
        public async Task<List<StockCompanyManagerRewards>> StockCompanyManagerRewards(Dictionary<string, string> parameters)
        {
            APIRequest request = new APIRequest()
            {
                InterfaceName = "stk_rewards",
                Parameters = parameters,
                Fields = APIInterface.GetAllDataFieldName(typeof(StockCompanyManagerRewards))
            };

            // Try to request and get response
            APIResponse response = await api.Request(request);

            if (response.ResponseCode != ResponseCode.OK)
            {
                throw new Exception(response.ErrorMessage);
            }

            // Try to deserialize the stock information
            return response.GetDataSet<StockCompanyManagerRewards>();
        }

        /// <summary>
        /// 获得上市公司高管薪酬及持股信息
        /// </summary>
        /// <param name="tsCode">股票TS代码（股票代码.市场）</param>
        /// <returns>高管薪酬及持股信息表</returns>
        public async Task<List<StockCompanyManagerRewards>> StockCompanyManagerRewards(string tsCode) => await StockCompanyManagerRewards(new string[] { tsCode });

        /// <summary>
        /// 获得上市公司高管薪酬及持股信息
        /// </summary>
        /// <param name="tsCodes">股票TS代码列表（股票代码.市场）</param>
        /// <returns>高管薪酬及持股信息表</returns>
        public async Task<List<StockCompanyManagerRewards>> StockCompanyManagerRewards(IEnumerable<string> tsCodes) =>
            await StockCompanyManagerRewards(new Dictionary<string, string>()
            {
                {"ts_code", string.Join(',', tsCodes) }
            });

        /// <summary>
        /// 获得上市公司高管的基本信息及其薪酬和持股信息
        /// </summary>
        /// <param name="tsCode">股票TS代码（股票代码.市场）</param>
        /// <returns>包含有高管信息和其薪酬持股信息组成的元组的信息表</returns>
        public async Task<List<(StockCompanyManager, StockCompanyManagerRewards)>> StockCompanyManagerAndRewards(string tsCode) =>
            await StockCompanyManagerAndRewards(new string[] { tsCode });

        /// <summary>
        /// 获得上市公司高管的基本信息及其薪酬和持股信息
        /// </summary>
        /// <param name="tsCodes">股票TS代码列表（股票代码.市场）</param>
        /// <returns>包含有高管信息和其薪酬持股信息组成的元组的信息表</returns>
        public async Task<List<(StockCompanyManager, StockCompanyManagerRewards)>> StockCompanyManagerAndRewards(IEnumerable<string> tsCodes)
        {
            List<StockCompanyManager> stockManagers = await StockCompanyManagers(tsCodes);
            List<StockCompanyManagerRewards> stockManagerRewards = await StockCompanyManagerRewards(tsCodes);
            List<(StockCompanyManager, StockCompanyManagerRewards)> managerList = new List<(StockCompanyManager, StockCompanyManagerRewards)>(); 

            foreach (StockCompanyManager manager in stockManagers)
            {
                IEnumerable<StockCompanyManagerRewards> rewardsQuery = from man in stockManagerRewards where string.Equals(man.Name, manager.Name) select man;
                if (rewardsQuery.Count() <= 0) continue;
                else
                {
                    StockCompanyManagerRewards rewards = rewardsQuery.First();
                    managerList.Add((manager, rewards));
                }
            }

            return managerList;
        }
    }
}
