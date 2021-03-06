using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using static TusharePro.Interface.APIInterface;
using static TusharePro.ShanghaiShenzhenStock.EnumerationNames;

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


    /// <summary>
    /// 市场板块
    /// </summary>
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
        /// 上海证券交易所
        /// </summary>
        SSE,
        /// <summary>
        /// 深圳证券交易所
        /// </summary>
        SZSE,
        /// <summary>
        /// 中金所
        /// </summary>
        CFFEX,
        /// <summary>
        /// 上海期货交易所
        /// </summary>
        SHFE,
        /// <summary>
        /// 郑商所
        /// </summary>
        CZCE,
        /// <summary>
        /// 大商所
        /// </summary>
        DCE,
        /// <summary>
        /// 上能源
        /// </summary>
        INE
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

    /// <summary>
    /// 沪深股票
    /// </summary>
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
        [DataField(NameInJson = "market", EnumNameMap = MarketNamesMap)]
        public Market Market { get; set; }

        /// <summary>
        /// 交易所
        /// </summary>
        [DataField(NameInJson = "exchange", EnumNameMap = ExchangeNamesMap)]
        public Exchange Exchange { get; set; }

        /// <summary>
        /// 交易货币
        /// </summary>
        [DataField(NameInJson = "curr_type")]
        public string CurrencyType { get; set; }

        /// <summary>
        /// 上市状态
        /// </summary>
        [DataField(NameInJson = "list_status", EnumNameMap = ListStatusNamesMap)]
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
        [DataField(NameInJson = "is_hs", EnumNameMap = ShSzHkConnectNamesMap)]
        public ShSzHkConnect ShSzHkConnect { get; set; }

        /// <summary>
        /// 获得表示该支股票的简略字符串
        /// </summary>
        public override string ToString() => $"{Name}({Code})";
    }

    /// <summary>
    /// 交易状态
    /// </summary>
    public enum TradeStatus
    {
        /// <summary>
        /// 休市
        /// </summary>
        Closing,

        /// <summary>
        /// 交易
        /// </summary>
        Opening,
    }

    /// <summary>
    /// 交易日
    /// </summary>
    public class TradeDay
    {
        /// <summary>
        /// 交易所
        /// </summary>
        [DataField(NameInJson = "exchange", EnumNameMap = ExchangeNamesMap)]
        public Exchange Exchange { get; set; }

        /// <summary>
        /// 日历日期
        /// </summary>
        [DataField(NameInJson = "cal_date", DateTimeConvert = true)]
        public DateTime Date { get; set; }

        /// <summary>
        /// 交易状态
        /// </summary>
        [DataField(NameInJson = "is_open", EnumNameMap = TradeStatusNamesMap)]
        public TradeStatus TradeStatus { get; set; }

        /// <summary>
        /// 上一交易日
        /// </summary>
        [DataField(NameInJson = "pretrade_date", DateTimeConvert = true)]
        public DateTime? PretradeDate { get; set; }

        /// <summary>
        /// 获得表示该交易日的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"[{Enum.GetName(typeof(Exchange), Exchange)}]{Date.ToString("yyyy-MM-dd")}({(TradeStatus == TradeStatus.Opening ? "交易" : "休市")})";
    }
}
