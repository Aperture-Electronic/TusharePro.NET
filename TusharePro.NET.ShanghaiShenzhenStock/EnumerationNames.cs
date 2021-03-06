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
