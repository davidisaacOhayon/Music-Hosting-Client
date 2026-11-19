using controllers;
using System.Net.Http;


namespace MainExecution {

    public class Program {

        static HttpClient ClientInstance;

        static HttpController MainHttpController;

        private string LastPlayedSong;




        public static void Init() {

            InitializeEssentials();
            InitializeControllers();

        }

        public static async Task InitializeControllers() {
            Console.WriteLine("Initializing Controllers."); 
            Console.WriteLine("Creating Http Controller instance."); 
            MainHttpController = new HttpController(ClientInstance);
            

        }

        public static void InitializeEssentials() {
            // Creation of essential elements before construction of controllers
            Console.WriteLine("Initializing Essentials."); 
            Console.WriteLine("Creating Http Client instance."); 

            ClientInstance = new HttpClient();
       
    
            Console.WriteLine("Essentials Iniitialized."); 


        }



    }


}