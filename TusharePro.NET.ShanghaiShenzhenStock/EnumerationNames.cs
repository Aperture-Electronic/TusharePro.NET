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
using System.Text;

namespace TusharePro.ShanghaiShenzhenStock
{
    /// <summary>
    /// 枚举字符串映射
    /// </summary>
    internal static class EnumerationNames
    {
        /// <summary>
        /// 交易所字符串映射
        /// </summary>
        public const string ExchangeNamesMap = "SSE,SZSE,CFFEX,SHFE,CZCE,DCE,INE";
        
        /// <summary>
        /// 市场板块字符串映射
        /// </summary>
        public const string MarketNamesMap = "主板,中小板,创业板,科创板,CDR,";
        
        /// <summary>
        /// 上市状态字符串映射
        /// </summary>
        public const string ListStatusNamesMap = "L,D,P";

        /// <summary>
        /// 沪深港通标的字符串映射
        /// </summary>
        public const string ShSzHkConnectNamesMap = "N,H,S";

        /// <summary>
        /// 交易状态字符串映射
        /// </summary>
        public const string TradeStatusNamesMap = "0,1";
    }
}
