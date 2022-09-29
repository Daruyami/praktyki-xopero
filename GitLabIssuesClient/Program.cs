using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace GitLabIssuesClient
{
    internal static class Program
    {
        private static readonly HttpClient Client = new HttpClient();
        
        public static Dictionary<string,string> PostParams = new Dictionary<string, string>
        {
            {"grant_type", "password"},
            { "username", "" },
            { "password", "" }
        };

        public static Uri RequestUri;

        private static string _token;

        public static async Task<string> GetToken()
        {
            var content = new FormUrlEncodedContent(PostParams);
            var response = await Client.PostAsync("https://"+RequestUri.Host+"/oauth/token", content);
            return await response.Content.ReadAsStringAsync();
        }
        
        public static async Task<string> GetResponse(string path, Dictionary<string,string> parameters)
        {
            var content = new FormUrlEncodedContent(parameters);
            var response = await Client.PostAsync("https://"+RequestUri.Host+path, content);
            Console.Out.WriteLine("response.IsSuccessStatusCode: " + response.IsSuccessStatusCode);
            return await response.Content.ReadAsStringAsync();
        }
        
        public static string GetUserInput(string message=null)
        {
            if (message != null)
                Console.Out.WriteLine(message);
            string input;
            do
                input = Console.ReadLine();
            while (string.IsNullOrEmpty(input));
            return input;
        }

        public static void Main(string[] args)
        {
            string input = GetUserInput("Input gitlab uri: ");
            while (!Uri.IsWellFormedUriString(input, UriKind.Absolute))
            {

                input = GetUserInput("Bad uri! Try again: ");
            }

            RequestUri = new Uri(input);
            PostParams["username"] = GetUserInput("Input username: ");
            PostParams["password"] = GetUserInput("Input password: ");
            foreach (string key in PostParams.Keys)
            {
                Console.Out.WriteLine(key + ": " + PostParams[key]);
            }

            var response = GetToken().Result;
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,string>>(response);
            Console.Out.WriteLine("response: \n" + response);
            Console.Out.WriteLine("obj: " + obj);
            _token = obj["access_token"];
            Console.Out.WriteLine("obj[\"access_token\"]: "+_token);

            /*
            //nie wiem czemu nie działa, nie mam zielonego pojęcia
            response = GetResponse("/oauth/token/info", new Dictionary<string, string>() { { "access_token", _token } }).Result;
            Console.Out.WriteLine("response2: \n"+response);*/

            response = GetResponse("/oauth/revoke", new Dictionary<string, string>() { { "access_token", _token } }).Result;
            Console.Out.WriteLine("response3: \n"+response);


        }
    }
}