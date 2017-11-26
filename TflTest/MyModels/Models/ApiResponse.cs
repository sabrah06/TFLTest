using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyModels.Models
{
    public class ApiResponse
    {
        public string id { get; set; }
        public string displayName { get; set; }
        public string statusSeverity { get; set; }
        public string statusSeverityDescription { get; set; }
        public string bounds { get; set; }
        public string envelope { get; set; }
        public string url { get; set; }
        public string timestampUtc { get; set; }
        public string exceptionType { get; set; }
        public string httpStatusCode { get; set; }
        public string httpStatus { get; set; }
        public string relativeUri { get; set; }
        public string message { get; set; }
    }
}