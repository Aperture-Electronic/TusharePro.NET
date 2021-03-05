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
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TusharePro.Interface;

namespace TusharePro
{
    public partial class TushareProApi
    {
        private const string ApiURL = "http://api.tushare.pro";

        /// <summary>
        /// 当前用户凭证
        /// </summary>
        private protected string UserToken { get; set; }

        /// <summary>
        /// Tushare Pro接口实例（不初始化数据凭证，您需要稍后初始化数据凭证）
        /// </summary>
        public TushareProApi() => UserToken = null;

        /// <summary>
        /// Tushare Pro接口实例
        /// </summary>
        /// <param name="userToken">数据凭证</param>
        public TushareProApi(string userToken) => UserToken = userToken;

        /// <summary>
        /// 数据凭证十分为空
        /// </summary>
        public bool UserTokenValid => !string.IsNullOrWhiteSpace(UserToken);

        /// <summary>
        /// 向TusharePro服务器发出数据请求
        /// </summary>
        /// <param name="request">请求内容</param>
        /// <returns>返回内容</returns>
        public async Task<APIResponse> Request(APIRequest request)
        {
            if (!UserTokenValid) throw new Exception("请求失败！没有有效的数据凭据");
            APIRequestInternal requestInternal = new APIRequestInternal(request, UserToken);
            string requestJson = JsonConvert.SerializeObject(requestInternal);
            string responseJson = await HttpInterface.HttpPost(requestJson, ApiURL);
            return JsonConvert.DeserializeObject<APIResponse>(responseJson);
        }
    }
}
