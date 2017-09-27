using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using MicroAPI;
using ProtoWebAPIWinForms.Model;
namespace ProtoWebAPIWinForms
{
    public partial class Form1 : Form
    {
        string URI = "http://localhost:64502/api/Product";
        int id = 3;
        //string URI = "http://skillsetazureuat.azurewebsites.net/api/associates";
        MainController API;
        public Form1()
        {
            InitializeComponent();
            API = new MainController(URI);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getAll();
            
        }

        #region Methods

        private async void GetAllProducts()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var productJsonString = await response.Content.ReadAsStringAsync();

                        dataGridView1.DataSource = JsonConvert.DeserializeObject<Product[]>(productJsonString).ToList();

                    }
                }
            }
        }

        private async void AddProduct()
        {
            Product p = new Product();
            p.Id = 3;
            p.Name = "Rolex";
            p.Category = "Watch";
            p.Price = 1299936;
            using (var client = new HttpClient())
            {
                var serializedProduct = JsonConvert.SerializeObject(p);
                var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                var result = await client.PostAsync(URI, content);
            }
            GetAllProducts();
        }

        private async void UpdateProduct()
        {
            Product p = new Product();
            p.Id = 3;
            p.Name = "Rolex";
            p.Category = "Watch";
            p.Price = 1400000; //changed the price

            using (var client = new HttpClient())
            {
                var serializedProduct = JsonConvert.SerializeObject(p);
                var content = new StringContent(serializedProduct, Encoding.UTF8, "application/json");
                var result = await client.PutAsync(String.Format("{0}/{1}", URI, p.Id), content);
            }
            GetAllProducts();
        }


        private async void DeleteProduct()
        {
            using (var client = new HttpClient())
            {
                var result = await client.DeleteAsync(String.Format("{0}/{1}", URI, 3));
            }
            GetAllProducts();
        }

        #endregion

        
        public class SS_Associates
        {
            public int AssociateID { get; set; }
            public string UserName { get; set; }
            public string PhoneNumber { get; set; }
            public bool VPN { get; set; }
            public short DepartmentID { get; set; }
            public int LocationID { get; set; }
            public System.DateTime UpdatedOn { get; set; }
            public bool IsActive { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getOne(textBox1.Text);
        }

        private void btnInsertProduct_Click(object sender, EventArgs e)
        {
            Product p = new Product();
            p.Id = id;
            p.Name = "Rolex";
            p.Category = "Watch";
            p.Price = 1299936;
            var serializedProduct = JsonConvert.SerializeObject(p);
            post(serializedProduct);
            //increment
            id += 1;
            //refresh
        }

        

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            //gather all data to object
            Product p = new Product();
            p.Id = 3;
            p.Name = "Rolex";
            p.Category = "Watch";
            p.Price = 1400000; //changed the price
            //convert object to json string by serializing it
            var serializedProduct = JsonConvert.SerializeObject(p);
            put(serializedProduct, p.Id.ToString());
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            //call delete and give id only
            if(textBox1.Text!=null||textBox1.Text!="")
            {
                delete(textBox1.Text);
            }

        }
        #region api_acalls

        private async void getOne(string id)
        {
            var a = (id == null || id == "") ? new Product() : JsonConvert.DeserializeObject<Product>(await API.get(id));
            var b = JsonConvert.DeserializeObject<Product[]>(await API.get()).ToList();
            //filter
            var c = b.Where(x => x.Id == a.Id);
            dataGridView1.DataSource = (id == null || id == "") ? b : c.ToList();
        }

        private async void getAll()
        {
            var products = JsonConvert.DeserializeObject<Product[]>(await API.get()).ToList();
            dataGridView1.DataSource = products.ToList();
        }

        private async void post(string content)
        {
            var postData = await API.post(content);
            MessageBox.Show(postData);
            getAll();
        }

        private async void put(string content, string id)
        {
            var putData = await API.put(content, id);
            MessageBox.Show(putData);
            getAll();
        }

        private async void delete(string id)
        {
            var deleteData = await API.delete(id);
            MessageBox.Show(deleteData);
            getAll();
        }


        #endregion
    }
}
