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
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TusharePro
{
    internal static class HttpInterface
    {

        /// <summary>
        /// 发出异步HTTP请求
        /// </summary>
        /// <param name="postBody">请求体</param>
        /// <param name="url">请求目标</param>
        /// <returns></returns>
        public static async Task<string> HttpPost(string postBody, string url)
        {
            using HttpClient httpClient = new HttpClient();
            using HttpContent httpContent = new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(postBody)));
            using HttpResponseMessage response = await httpClient.PostAsync(url, httpContent);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
