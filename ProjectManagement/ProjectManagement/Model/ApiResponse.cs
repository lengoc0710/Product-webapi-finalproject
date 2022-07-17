using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Model
{
    public class ApiResponse
    {
        public string statusCode { get; set; }
        public string message { get; set; }
        public object? developerMessage { get; set; }
        public object? data { get; set; }

        public ApiResponse(string message, object developerMessage = null, object data = null)
        {
            this.statusCode = "201";
            this.message = message;
            this.developerMessage = developerMessage;
            this.data = data;
        }
    }
}
