using System.Collections.Generic;
using System;
using System.Net.Http;
using common.controllers;
using System.Net;

namespace controllers{ 

    public class HttpController : BaseController{

        private Dictionary<string, HttpResponseMessage> cache {get;} = new Dictionary<string, HttpResponseMessage>();

        private HttpClient ClientInstance {get;}


        public HttpController(HttpClient httpClientInstance) {
            this.ClientInstance = httpClientInstance;
            this.ClientInstance.Timeout = TimeSpan.FromMilliseconds(2000);
        }


        public async Task<HttpResponseMessage> request(string url, HttpContent? body) 
        {

            if (body != null)
            {
                try
                {
                  HttpResponseMessage response = this.ClientInstance.PostAsync(url, body);
                  response.EnsureSuccessStatusCode();

                  return response;

                } catch (Exception e)
                {
                    
                    return new HttpResponseMessage(HttpStatusCode.BadRequest) {Content = new StringContent($"Something had gone wrong issuing this request {e.Message}")};
                              
                }
                
            } 

        


            
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