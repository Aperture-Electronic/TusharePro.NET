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
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.ComponentModel;

namespace TusharePro.Interface
{
    public class APIRequest
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        public string InterfaceName { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        [JsonProperty(propertyName: "params")]
        [JsonRequired()]
        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// 需返回的字段
        /// </summary>
        [JsonProperty(propertyName: "fields", DefaultValueHandling = DefaultValueHandling.Include)]
        [DefaultValue("")]
        public List<string> Fields { get; set; }
    }


    /// <summary>
    /// API请求
    /// </summary>
    internal class APIRequestInternal
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        [JsonProperty(propertyName: "api_name")]
        [JsonRequired()]
        public string InterfaceName { get; set; }

        /// <summary>
        /// 数据凭证
        /// </summary>
        [JsonProperty(propertyName: "token")]
        [JsonRequired()]
        public string UserToken { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        [JsonProperty(propertyName: "params")]
        [JsonRequired()]
        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// 需返回的字段
        /// </summary>
        [JsonProperty(propertyName: "fields", DefaultValueHandling = DefaultValueHandling.Include)]
        [DefaultValue("")]
        public List<string> Fields { get; set; }

        public APIRequestInternal()
        {

        }

        /// <summary>
        /// 利用API Request生成带数据凭证的请求
        /// </summary>
        /// <param name="request"></param>
        public APIRequestInternal(APIRequest request, string userToken)
        {
            InterfaceName = request.InterfaceName;
            Parameters = request.Parameters;
            Fields = request.Fields;
            UserToken = userToken;
        }
    }

    /// <summary>
    /// 返回代码
    /// </summary>
    public enum ResponseCode
    {
        /// <summary>
        /// 正常访问
        /// </summary>
        OK = 0,

        /// <summary>
        /// 访问错误
        /// </summary>
        Error = default,

        /// <summary>
        /// 无权限
        /// </summary>
        NoPermission = 2002
    }

    /// <summary>
    /// 返回数据集
    /// </summary>
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ResponseDataSet
    {
        /// <summary>
        /// 列名列表
        /// </summary>
        [JsonProperty(propertyName: "fields")]
        public List<string> DataColumn { get; set; }

        /// <summary>
        /// 数据集
        /// </summary>
        [JsonProperty(propertyName: "items")]
        public List<List<string>> DataSet { get; set; }
    }

    /// <summary>
    /// API回馈
    /// </summary>
    public class APIResponse
    {
        /// <summary>
        /// 返回代码
        /// </summary>
        [JsonProperty(propertyName: "code")]
        public ResponseCode ResponseCode { get; set; }
        
        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty(propertyName: "msg", DefaultValueHandling = DefaultValueHandling.Ignore)]
        [DefaultValue("")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        [JsonProperty(propertyName: "data")]
        public ResponseDataSet DataSet { get; set; }
    }
}
