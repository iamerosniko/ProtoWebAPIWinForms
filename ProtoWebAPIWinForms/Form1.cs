using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using MicroAPI;
namespace ProtoWebAPIWinForms
{
    public partial class Form1 : Form
    {
        //string URI = "http://localhost:64502/api/Product";
        string URI = "http://skillsetazureuat.azurewebsites.net/api/associates";
        MainController API;
        public Form1()
        {
            InitializeComponent();
            API = new MainController(URI);
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            
            var b = JsonConvert.DeserializeObject<SS_Associates[]>(await API.get()).ToList();
            //filter
            
            
            //MessageBox.Show(c.ToString());

            dataGridView1.DataSource = b.ToList();
            //GetAllProducts();
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

        public class Product
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public decimal Price { get; set; }
        }
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

        private async void getOne(string id)
        {
            var a =(id==null || id=="")?new SS_Associates(): JsonConvert.DeserializeObject<SS_Associates>(await API.get(id));
            var b = JsonConvert.DeserializeObject<SS_Associates[]>(await API.get()).ToList();
            //filter
            var c = b.Where(x => x.AssociateID == a.AssociateID);

            //MessageBox.Show(c.ToString());

            dataGridView1.DataSource = (id == null || id == "") ? b : c.ToList();
        }
    }
}
