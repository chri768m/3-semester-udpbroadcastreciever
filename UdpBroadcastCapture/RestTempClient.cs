using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

// NuGet Newtonsoft.Json
// Problem: Security permission ....
// Solution: manually update NuGet Newtonsoft from version 8.x to 12.x
// https://stackoverflow.com/questions/48766856/system-security-permissions-missing-when-invoking-jsonconvert-deserializeobject/48767718

namespace UdpBroadcastCapture
{
    class RESTClient
    {
        public static async Task<TOut>  Post<TIn, TOut>(string uri, TIn item)
        {
            using HttpClient client = new HttpClient();
            string jsonStr = JsonConvert.SerializeObject(item);
            Console.WriteLine(jsonStr);
            StringContent requestContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpResponseMessage response = await client.PostAsync(uri, requestContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) // all 2xx status codes
            {
                //Console.WriteLine(jsonString);
                TOut data = JsonConvert.DeserializeObject<TOut>(responseContent);
            }
            throw new KeyNotFoundException($"Status code={response.StatusCode} Message={responseContent}");
        }

        public static async Task<TOut> Put<TIn, TOut>(string uri, TIn newValues)
        {
            string jsonStr = JsonConvert.SerializeObject(newValues);
            Console.WriteLine(jsonStr);
            StringContent requestContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            using HttpClient client = new HttpClient();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HttpResponseMessage response = await client.PutAsync(uri, requestContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) // all 2xx status codes
            {
                TOut data = JsonConvert.DeserializeObject<TOut>(responseContent);
                return data;
            }
            throw new KeyNotFoundException($"Status code={response.StatusCode} Message={responseContent}");
        }
    }
}