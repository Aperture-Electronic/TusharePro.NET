using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static TusharePro.Interface.APIInterface;

namespace TusharePro.ShanghaiShenzhenStock
{
    /// <summary>
    /// 股票上市状态
    /// </summary>
    public enum ListStatus
    {
        /// <summary>
        /// 上市
        /// </summary>
        Listing,

        /// <summary>
        /// 退市
        /// </summary>
        Delisting,

        /// <summary>
        /// 暂停上市
        /// </summary>
        Suspension
    }

    public enum Market
    {
        /// <summary>
        /// 主板
        /// </summary>
        Main,
        /// <summary>
        /// 中小板
        /// </summary>
        SME,
        /// <summary>
        /// 创业板
        /// </summary>
        GEM,
        /// <summary>
        /// 科创板
        /// </summary>
        STB,
        /// <summary>
        /// CDR
        /// </summary>
        CDR,
        /// <summary>
        /// 没有所属市场板块
        /// </summary>
        NA
    }

    /// <summary>
    /// 交易所
    /// </summary>
    public enum Exchange
    {
        /// <summary>
        /// 上证交易所
        /// </summary>
        SSE,
        /// <summary>
        /// 深圳交易所
        /// </summary>
        SZSE,
    }

    /// <summary>
    /// 沪深港通标的状态
    /// </summary>
    public enum ShSzHkConnect
    {
        /// <summary>
        /// 非沪深港通标的
        /// </summary>
        No,
        /// <summary>
        /// 沪股通
        /// </summary>
        Shanghai,
        /// <summary>
        /// 深股通
        /// </summary>
        Shenzhen
    }

    public class Stock
    {
        /// <summary>
        /// 股票TS代码（股票代码.市场）
        /// </summary>
        [DataField(NameInJson = "ts_code")]
        public string CodeTS { get; set; }

        /// <summary>
        /// 股票代码
        /// </summary>
        [DataField(NameInJson = "symbol")]
        public string Code { get; set; }

        /// <summary>
        /// 股票名称
        /// </summary>
        [DataField(NameInJson = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 所在地域
        /// </summary>
        [DataField(NameInJson = "area")]
        public string Area { get; set; }

        /// <summary>
        /// 所属行业
        /// </summary>
        [DataField(NameInJson = "industry")]
        public string Industry { get; set; }

        /// <summary>
        /// 股票全称
        /// </summary>
        [DataField(NameInJson = "fullname")]
        public string FullName { get; set; }

        /// <summary>
        /// 英文全称
        /// </summary>
        [DataField(NameInJson = "enname")]
        public string EnglishName { get; set; }

        /// <summary>
        /// 市场类型（大板块）
        /// </summary>
        [DataField(NameInJson = "market", EnumNameMap = new string[] { "主板", "中小板", "创业板", "科创板", "CDR", null })]
        public Market Market { get; set; }

        /// <summary>
        /// 交易所
        /// </summary>
        [DataField(NameInJson = "exchange", EnumNameMap = new string[] { "SSE", "SZSE"})]
        public Exchange Exchange { get; set; }

        /// <summary>
        /// 交易货币
        /// </summary>
        [DataField(NameInJson = "curr_type")]
        public string CurrencyType { get; set; }

        /// <summary>
        /// 上市状态
        /// </summary>
        [DataField(NameInJson = "list_status", EnumNameMap = new string[] { "L", "D", "P" })]
        public ListStatus ListStatus { get; set; }

        /// <summary>
        /// 上市日期
        /// </summary>
        [DataField(NameInJson = "list_date", DateTimeConvert = true)]
        public DateTime? ListDate { get; set; }

        /// <summary>
        /// 退市日期
        /// </summary>
        [DataField(NameInJson = "delist_date", DateTimeConvert = true)]
        public DateTime? DelistDate { get; set; }

        /// <summary>
        /// 是否沪深港通标的
        /// </summary>
        [DataField(NameInJson = "is_hs", EnumNameMap = new string[] { "N", "H", "S" })]
        public ShSzHkConnect ShSzHkConnect { get; set; }

        public override string ToString() => $"{Name}({Code})";
    }
}
