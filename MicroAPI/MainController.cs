using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicroAPI
{
    public class MainController
    {
        private string _URI = "";
        private string _Msg = "";
        public MainController(string URI)
        {
            this._URI = URI;
        }
        //working
        public async Task<string> get()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(this._URI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var productJsonString = await response.Content.ReadAsStringAsync();
                        return productJsonString;
                    }
                }
            }
            return "";
        }
        //working
        public async Task<string> get(string id)
        {
            this._Msg = "";
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(this._URI+"/"+id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var productJsonString = await response.Content.ReadAsStringAsync();
                        this._Msg = productJsonString;
                    }
                }
            }
            return _Msg;
        }
        //working
        public async Task<string> post(string content)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                var body = new StringContent(content, Encoding.UTF8, "application/json");
                httpResponseMessage = await client.PostAsync(this._URI, body);
            }
            return (int)httpResponseMessage.StatusCode==200 ? content : "";

        }
        public async Task<string> put(string content, string id)
        {
            this._Msg = "";
            using (var client = new HttpClient())
            {
                var body = new StringContent(content, Encoding.UTF8, "application/json");
                var apiResult = await client.PutAsync(String.Format("{0}/{1}", this._URI, id), body);
                this._Msg = apiResult.ToString();
            }

            return this._Msg;
        }
        public async Task<string> delete(string id)
        {
            this._Msg = "";
            using (var client = new HttpClient())
            {
                var apiResult = await client.DeleteAsync(String.Format("{0}/{1}", this._URI, id));
                this._Msg = apiResult.ToString();
            }
            return this._Msg;
        }
    }
}
