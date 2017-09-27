using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroAPI
{
    public class MainController
    {
        private string _URI = "";
        public MainController(string URI,string Controller)
        {
            this._URI = URI+"/"+Controller;
        }

        public string get()
        {
            return "";
        }
        public string get(string id)
        {
            return "";
        }
        public string post(string content)
        {
            return "";
        }
        public string put(string content,string id)
        {
            return "";
        }
        public string delete(string id)
        {
            return "";
        }
    }
}
