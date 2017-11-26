using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;
using TflTest.Models;
using System.Web.Http;
using MyModels.Models;
using System.Net.Http;

namespace TflTest.Controllers
{
    public class QueryController : ApiController
    {
        [System.Web.Http.HttpGet]
        public ApiResponse Search(string RoadId)
        {
            ApiResponse apiresp = new ApiResponse();
            //Collect the Application setting from the config
            string APIUrl = ConfigurationManager.AppSettings["ApiEndPoint"].ToString();
            string AppId = ConfigurationManager.AppSettings["AppId"].ToString();
            string AppKey = ConfigurationManager.AppSettings["AppKey"].ToString();
            string apiQuery = $"{APIUrl}Road/{RoadId}?app_id={AppId}&app_key={AppKey}";
            // To resolve the SSL/TSL exception
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                    | SecurityProtocolType.Tls11
                    | SecurityProtocolType.Tls12
                    | SecurityProtocolType.Ssl3;
            using (var client = new WebClient())
            {
                try
                {
                    var json = client.DownloadString(apiQuery);
                    json = HttpUtility.HtmlDecode(json);
                    dynamic data = JsonConvert.DeserializeObject(json);
                    apiresp = ProcessResponse(data);
                }
                catch (WebException ex)
                {
                    if (ex.Message.Contains("404")) // in 404 return httpstatus
                    {
                        apiresp.timestampUtc = DateTime.Now.ToLongDateString();
                        apiresp.exceptionType = "EntityNotFoundException";
                        apiresp.httpStatusCode = "404";
                        apiresp.httpStatus = "NotFound";
                        apiresp.relativeUri = $"/Road/{RoadId}";
                        apiresp.message = $"The following road id is not recognised: {RoadId}";
                    }
                }
            }
            return apiresp;
        }

        public IHttpActionResult SearchResponse(string RoadId)
        {
            ApiResponse apiresp = ProcessRequest(RoadId);
            if (apiresp.httpStatusCode == "404")
            {
                return NotFound();
            }
            return Ok(apiresp);
        }

        private ApiResponse ProcessRequest(string RoadId)
        {
            ApiResponse apiresp = new ApiResponse();
            //Collect the Application setting from the config
            string APIUrl = ConfigurationManager.AppSettings["ApiEndPoint"].ToString();
            string AppId = ConfigurationManager.AppSettings["AppId"].ToString();
            string AppKey = ConfigurationManager.AppSettings["AppKey"].ToString();
            string apiQuery = $"{APIUrl}Road/{RoadId}?app_id={AppId}&app_key={AppKey}";
            // To resolve the SSL/TSL exception
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                    | SecurityProtocolType.Tls11
                    | SecurityProtocolType.Tls12
                    | SecurityProtocolType.Ssl3;
            using (var client = new WebClient())
            {
                try
                {
                    var json = client.DownloadString(apiQuery);
                    json = HttpUtility.HtmlDecode(json);
                    dynamic data = JsonConvert.DeserializeObject(json);
                    apiresp = ProcessResponse(data);
                }
                catch (WebException ex)
                {
                    if (ex.Message.Contains("404")) // in 404 return httpstatus
                    {
                        apiresp.timestampUtc = DateTime.Now.ToLongDateString();
                        apiresp.exceptionType = "EntityNotFoundException";
                        apiresp.httpStatusCode = "404";
                        apiresp.httpStatus = "NotFound";
                        apiresp.relativeUri = $"/Road/{RoadId}";
                        apiresp.message = $"The following road id is not recognised: {RoadId}";
                    }
                }
            }
            return apiresp;
        }
        /***
         Process the Response to the Api response model
         ***/
        private ApiResponse ProcessResponse(dynamic data)
        {
            ApiResponse apiResp = new ApiResponse();
            foreach (dynamic values in data)
            {
                apiResp.displayName = values.displayName.ToString();
                apiResp.id = values.id;
                apiResp.statusSeverity = values.statusSeverity;
                apiResp.statusSeverityDescription = values.statusSeverityDescription;
                apiResp.bounds = values.bounds;
                apiResp.envelope = values.envelope;
                apiResp.url = values.url;
            }
            return apiResp;
        }
    }
}