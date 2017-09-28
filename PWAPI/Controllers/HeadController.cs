using PWAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MicroAPI;
using System.Threading.Tasks;
namespace PWAPI.Controllers
{
    public class HeadController : ApiController
    {
        MainController mc;
        string result;
        // POST: api/Head
        public string Post(BodyTemplate bodyTemplate)
        {
            mc = new MainController(bodyTemplate.URI);
            //get
            switch (bodyTemplate.ControlType.ToLower())
            {
                case "get":
                    get(bodyTemplate);
                    return result;
                case "post":
                    return "post";
                case "put":
                    return "put";
                case "delete":
                    return "delete";
                default:
                    return "";
            }

        }
        private async void get(BodyTemplate bodyTemplate)
        {
            if (bodyTemplate.ID.Trim().Equals(""))
            {
                var a = Task.Run(() => mc.get()).Result;
                result = a;
            }
        }
    }
}
