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
