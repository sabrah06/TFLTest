using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyModels.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Configuration;

namespace RoadStatus
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                Console.WriteLine("Please supply the road information");
                return;
            }
            string RoadId = args[0];
            string MyApi = ConfigurationManager.AppSettings["MyApi"].ToString();
            string apiRef = $"{MyApi}?RoadId={RoadId}";
            ApiResponse apiResp = new ApiResponse();
            using (HttpClient httpClient = new HttpClient())
            {
                var response = httpClient.GetStringAsync(apiRef);
                apiResp = JsonConvert.DeserializeObject<ApiResponse>(response.Result);
            }
            if (string.IsNullOrEmpty(apiResp.httpStatus))
            {
                Console.WriteLine($"The status of the {RoadId} is as follows \n\r");
                Console.WriteLine($"Road Status is {apiResp.statusSeverity}");
                Console.WriteLine($"Road Status Description is {apiResp.statusSeverityDescription}");
            }
            else
            {
                Console.WriteLine(RoadId + " is not a valid road");
            }
            Console.ReadKey();
        }
    }
}
