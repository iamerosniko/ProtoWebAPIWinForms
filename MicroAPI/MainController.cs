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
            return null;
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
            return null;
        }
        //working
        public async Task<string> post(string content)
        {
            string resultContent;
            using (var client = new HttpClient())
            {
                var body = new StringContent(content, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(this._URI, body);
                resultContent = await result.Content.ReadAsStringAsync();
            }
            return resultContent;

        }
        public async Task<string> put(string content, string id)
        {
            string resultContent;
            using (var client = new HttpClient())
            {
                var body = new StringContent(content, Encoding.UTF8, "application/json");
                var result = await client.PutAsync(String.Format("{0}/{1}", this._URI, id), body);
                resultContent = await result.Content.ReadAsStringAsync();
            }
            return resultContent;
        }
        public async Task<string> delete(string id)
        {
            string resultContent;
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync(String.Format("{0}/{1}", this._URI, id));
                resultContent = await result.Content.ReadAsStringAsync();
            }
            return resultContent;
        }
    }
}
