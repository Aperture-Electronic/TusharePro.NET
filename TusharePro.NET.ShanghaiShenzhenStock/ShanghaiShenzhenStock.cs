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
        [DataField(NameInJson = "list_date", DateConvert = true)]
        public DateTime? ListDate { get; set; }

        /// <summary>
        /// 退市日期
        /// </summary>
        [DataField(NameInJson = "delist_date", DateConvert = true)]
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
        [DataField(NameInJson = "cal_date", DateConvert = true)]
        public DateTime Date { get; set; }

        /// <summary>
        /// 交易状态
        /// </summary>
        [DataField(NameInJson = "is_open", EnumNameMap = TradeStatusNamesMap)]
        public TradeStatus TradeStatus { get; set; }

        /// <summary>
        /// 上一交易日
        /// </summary>
        [DataField(NameInJson = "pretrade_date", DateConvert = true)]
        public DateTime? PretradeDate { get; set; }

        /// <summary>
        /// 获得表示该交易日的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"[{Enum.GetName(typeof(Exchange), Exchange)}]{Date.ToString("yyyy-MM-dd")}({(TradeStatus == TradeStatus.Opening ? "交易" : "休市")})";
    }

    /// <summary>
    /// 股票曾用名
    /// </summary>
    public class NameHistory
    {
        /// <summary>
        /// 股票TS代码（股票代码.市场）
        /// </summary>
        [DataField(NameInJson = "ts_code")]
        public string CodeTS { get; set; }

        /// <summary>
        /// 股票名称
        /// </summary>
        [DataField(NameInJson = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 该股票自此日期起启用此名称
        /// </summary>
        [DataField(NameInJson = "start_date", DateConvert = true)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 该股票自此日期起不再使用此名称
        /// </summary>
        [DataField(NameInJson = "end_date", DateConvert = true)]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 该股票在此日公告了此名称
        /// </summary>
        [DataField(NameInJson = "ann_date", DateConvert = true)]
        public DateTime? AnnouncementDate { get; set; }

        /// <summary>
        /// 更改名称的原因
        /// </summary>
        [DataField(NameInJson = "change_reason")]
        public string ChangeReason { get; set; }

        /// <summary>
        /// 获取表示股票名称变更的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{CodeTS} [{StartDate?.ToString("yyyy-MM-dd")}至{StartDate?.ToString("yyyy-MM-dd") ?? "今"}]{Name}, " +
                $"因{ChangeReason}原因在{AnnouncementDate?.ToString("yyyy-MM-dd") ?? "未知日期"}公布。";
    }

    /// <summary>
    /// 沪深股通成分股
    /// </summary>
    public class ShSzHkConnectConstituent
    {
        /// <summary>
        /// 股票TS代码（股票代码.市场）
        /// </summary>
        [DataField(NameInJson = "ts_code")]
        public string CodeTS { get; set; }

        /// <summary>
        /// 沪深港通类型
        /// </summary>
        [DataField(NameInJson = "hs_type", EnumNameMap = ShSzHkConnectTypeNamesMap)]
        public ShSzHkConnect ConnectType { get; set; }

        /// <summary>
        /// 纳入日期
        /// </summary>
        [DataField(NameInJson = "in_date", DateConvert = true)]
        public DateTime? InclusionDate { get; set; }

        /// <summary>
        /// 剔除日期
        /// </summary>
        [DataField(NameInJson = "out_date", DateConvert = true)]
        public DateTime? ExclusionDate { get; set; }

        /// <summary>
        /// 获得沪深股通成分股的字符串表示
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"[{(ConnectType == ShSzHkConnect.Shanghai ? "沪港通" : "深港通")}]{CodeTS}, " +
            $"收录时间{InclusionDate?.ToString("yyyy-MM-dd")}, 剔除时间{ExclusionDate?.ToString("yyyy-MM-dd")}";
    }

    /// <summary>
    /// 上市公司
    /// </summary>
    public class StockCompany
    {
        /// <summary>
        /// 股票TS代码（股票代码.市场）
        /// </summary>
        [DataField(NameInJson = "ts_code")]
        public string CodeTS { get; set; }

        /// <summary>
        /// 交易所
        /// </summary>
        [DataField(NameInJson = "exchange", EnumNameMap = ExchangeNamesMap)]
        public Exchange Exchange { get; set; }

        /// <summary>
        /// 法人代表
        /// </summary>
        [DataField(NameInJson = "chairman")]
        public string Chairman { get; set; }

        /// <summary>
        /// 总经理
        /// </summary>
        [DataField(NameInJson = "manager")]
        public string Manager { get; set; }

        /// <summary>
        /// 董秘
        /// </summary>
        [DataField(NameInJson = "secretary")]
        public string Secratary { get; set; }

        /// <summary>
        /// 注册资本（万元）
        /// </summary>
        [DataField(NameInJson = "reg_capital", FloatConvert = true)]
        public float RegistedCapital { get; set; }

        /// <summary>
        /// 注册日期
        /// </summary>
        [DataField(NameInJson = "setup_date", DateConvert = true)]
        public DateTime? SetupDate { get; set; }

        /// <summary>
        /// 所在省份
        /// </summary>
        [DataField(NameInJson = "province")]
        public string Province { get; set; }

        /// <summary>
        /// 所在城市
        /// </summary>
        [DataField(NameInJson = "city")]
        public string City { get; set; }

        /// <summary>
        /// 公司介绍
        /// </summary>
        [DataField(NameInJson = "introduction")]
        public string Introduction { get; set; }

        /// <summary>
        /// 公司主页
        /// </summary>
        [DataField(NameInJson = "website")]
        public string Website { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        [DataField(NameInJson = "email")]
        public string Email { get; set; }

        /// <summary>
        /// 办公地址
        /// </summary>
        [DataField(NameInJson = "office")]
        public string OfficeAddress { get; set; }

        /// <summary>
        /// 员工人数
        /// </summary>
        [DataField(NameInJson = "employees", IntegerConvert = true)]
        public int Employees { get; set; }

        /// <summary>
        /// 主营业务
        /// </summary>
        [DataField(NameInJson = "main_business")]
        public string MainBusiness { get; set; }

        /// <summary>
        /// 经营范围
        /// </summary>
        [DataField(NameInJson = "business_scope")]
        public string BusinessScope { get; set; }

        /// <summary>
        /// 获得上市公司基础信息字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = $"[{CodeTS}]\n";
            str += $"交易所: {Enum.GetName(typeof(Exchange), Exchange)}\n";
            str += $"法人：{Chairman}, 总经理: {Manager}, 董秘: {Secratary}\n";
            str += $"注册资本: {RegistedCapital} 万元\n";
            str += $"注册日期: {SetupDate?.ToString("yyyy-MM-dd")}\n";
            str += $"所在地: {Province} {City}\n";
            str += $"公司主页: {Website}\n";
            str += $"电子邮件: {Email}\n";
            str += $"办公地址: {OfficeAddress}\n";
            str += $"员工人数: {Employees}\n";
            str += $"经营范围: {BusinessScope}\n";
            str += $"主营业务及主要产品: {MainBusiness}\n";
            str += $"公司介绍: {Introduction}\n";
            
            return str;
        }
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// 男性
        /// </summary>
        Male,
        /// <summary>
        /// 女性
        /// </summary>
        Female,
        /// <summary>
        /// 未知
        /// </summary>
        Unknown
    }

    /// <summary>
    /// 上市公司高管
    /// </summary>
    public class StockCompanyManager
    {
        /// <summary>
        /// 股票TS代码（股票代码.市场）
        /// </summary>
        [DataField(NameInJson = "ts_code")]
        public string CodeTS { get; set; }

        /// <summary>
        /// 该股票在此日公告了此高管
        /// </summary>
        [DataField(NameInJson = "ann_date", DateConvert = true)]
        public DateTime? AnnouncementDate { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [DataField(NameInJson = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DataField(NameInJson = "gender", EnumNameMap = GenderNamesMap)]
        public Gender Gender { get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        [DataField(NameInJson = "lev")]
        public string PositionType { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        [DataField(NameInJson = "title")]
        public string Position { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        [DataField(NameInJson = "edu")]
        public string Education { get; set; }

        /// <summary>
        /// 国籍
        /// </summary>
        [DataField(NameInJson = "national")]
        public string Nationality { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        [DataField(NameInJson = "birthday", DateConvert = true)]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 上任日期
        /// </summary>
        [DataField(NameInJson = "begin_date", DateConvert = true)]
        public DateTime? AppointmentDate { get; set; }

        /// <summary>
        /// 离任日期
        /// </summary>
        [DataField(NameInJson = "end_date", DateConvert = true)]
        public DateTime? DepartureDate { get; set; }

        /// <summary>
        /// 个人简历
        /// </summary>
        [DataField(NameInJson = "resume")]
        public string Resume { get; set; }

        /// <summary>
        /// 获得上市公司高管信息字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = $"[{CodeTS}]{Name}\n";
            str += $"公告日期: {AnnouncementDate?.ToString("yyyy-MM-dd")}\n";
            str += $"性别: {Enum.GetName(typeof(Gender), Gender)}\n";
            str += $"职务：{Position}({PositionType})\n";
            str += $"学历: {Education}\n";
            str += $"国籍: {Nationality}\n";
            str += $"出生年月: {Birthday?.ToString("yyyy-MM")}({(int)((DateTime.Today - (Birthday??DateTime.Now)).TotalDays / 365)})\n";
            str += $"上任日期: {AppointmentDate?.ToString("yyyy-MM-dd")}\n";
            str += $"离任日期: {DepartureDate?.ToString("yyyy-MM-dd")}\n";
            str += $"个人简历: {Resume}\n";

            return str;
        }
    }

    /// <summary>
    /// 上市公司高管薪酬及持股
    /// </summary>
    public class StockCompanyManagerRewards
    {
        /// <summary>
        /// 股票TS代码（股票代码.市场）
        /// </summary>
        [DataField(NameInJson = "ts_code")]
        public string CodeTS { get; set; }

        /// <summary>
        /// 该股票在此日公告了此高管的薪酬及持股
        /// </summary>
        [DataField(NameInJson = "ann_date", DateConvert = true)]
        public DateTime? AnnouncementDate { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        [DataField(NameInJson = "end_date", DateConvert = true)]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [DataField(NameInJson = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        [DataField(NameInJson = "title")]
        public string Position { get; set; }

        /// <summary>
        /// 薪酬（元）
        /// </summary>
        [DataField(NameInJson = "reward", FloatConvert = true)]
        public float Rewards { get; set; }

        /// <summary>
        /// 持股数
        /// </summary>
        [DataField(NameInJson = "hold_vol", FloatConvert = true)]
        public float Shareholding { get; set; }

        /// <summary>
        /// 获得上市公司高管薪酬及持股信息字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = $"[{CodeTS}]{Name}\n";
            str += $"公告日期: {AnnouncementDate?.ToString("yyyy-MM-dd")}\n";
            str += $"截止日期: {EndDate?.ToString("yyyy-MM-dd")}\n";
            str += $"职务：{Position}\n";
            str += $"薪酬: {Rewards} 元\n";
            str += $"持股: {Shareholding}\n";

            return str;
        }
    }
}
