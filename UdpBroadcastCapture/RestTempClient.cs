﻿using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; // NuGet Newtonsoft.Json
// Problem: Security permission ....
// Solution: manually update NuGet Newtonsoft from version 8.x to 12.x
// https://stackoverflow.com/questions/48766856/system-security-permissions-missing-when-invoking-jsonconvert-deserializeobject/48767718

namespace UdpBroadcastCapture
{
    class RESTClient
    {
        public static async Task<List<T>> GetList<T>(string uri)
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/generic-methods
        {
            using HttpClient client = new HttpClient();
            string content = await client.GetStringAsync(uri);
            // Console.WriteLine(content);
            List<T> data = JsonConvert.DeserializeObject<List<T>>(content);
            return data;
        }

        public static async Task<T> GetSingle<T>(string uri)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(uri);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) // all 2xx status codes
            {
                T data = JsonConvert.DeserializeObject<T>(content);
                return data;
            }
            throw new KeyNotFoundException($"Status code={response.StatusCode} Message={content}");
        }

        public static async Task<TOut> Post<TIn, TOut>(string uri, TIn item)
        {
            using HttpClient client = new HttpClient();
            string jsonStr = JsonConvert.SerializeObject(item);
            StringContent requestContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, requestContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) // all 2xx status codes
            {
                //Console.WriteLine(jsonString);
                TOut data = JsonConvert.DeserializeObject<TOut>(responseContent);
                return data;
            }
            throw new KeyNotFoundException($"Status code={response.StatusCode} Message={responseContent}");
        }

        public static async Task<T> Delete<T>(string uri)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(uri);
            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(jsonString);
                T data = JsonConvert.DeserializeObject<T>(jsonString);
                return data;
            }
            throw new KeyNotFoundException($"Status code={response.StatusCode} Message={responseContent}");
        }

        public static async Task<TOut> Put<TIn, TOut>(string uri, TIn newValues)
        {
            string jsonStr = JsonConvert.SerializeObject(newValues);
            StringContent requestContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            using HttpClient client = new HttpClient();
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