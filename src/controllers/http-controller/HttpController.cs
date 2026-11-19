using System.Collections.Generic;
using System;
using System.Net.Http;
using common.controllers;
 
namespace controllers{ 

    public class HttpController : BaseController{

        private Dictionary<string, HttpResponseMessage> cache {get;} = new Dictionary<string, HttpResponseMessage>();

        private HttpClient ClientInstance {get;}


        public HttpController(HttpClient httpClientInstance) {
            this.ClientInstance = httpClientInstance;
            this.ClientInstance.Timeout = TimeSpan.FromMilliseconds(2000);
        }


        

        public async Task<bool> TestFunctionality() {
            Console.WriteLine("RUNNING FUNCTIONALITY CHECK ON HTTPCONTROLLER -----");
            if(this.ClientInstance != null) {

                try {

                   HttpResponseMessage response = await this.ClientInstance.GetAsync("https://jsonplaceholder.typicode.com/posts");
                   response.EnsureSuccessStatusCode();

                   Console.WriteLine("Http Controller Confirmed working order.");
                    //    string responseBody = await response.Content.ReadAsStringAsync();
                    //    Console.WriteLine(responseBody);
                   return true;

                } catch (Exception e){
                    Console.WriteLine($"An Error Occured, HTTP Controller isn't working.: {e.Message}");
                    return false;

                }

            } else {
                Console.WriteLine("Http Controller isn't working, HttpClient instance is null.");
                return false;
            }
        }
        


    }


}