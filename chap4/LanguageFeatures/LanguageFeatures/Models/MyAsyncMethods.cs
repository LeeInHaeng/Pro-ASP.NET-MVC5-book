using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;

namespace LanguageFeatures.Models
{
    public class MyAsyncMethods
    {
        public async static Task<long?> GetPageLength()
        {
            HttpClient clinet = new HttpClient();

            var httpMessage = await clinet.GetAsync("http://apress.com");

            // HTTP 요청이 완료되기를 기다리는 동안
            // 여기서 다른 작업을 처리할 수 있다.

            return httpMessage.Content.Headers.ContentLength;
        }
    }
}