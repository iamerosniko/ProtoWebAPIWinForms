using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using MicroAPI;
using ProtoWebAPIWinForms.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace ProtoWebAPIWinForms
{
    public partial class Form1 : Form 
    {
        //string URI = "http://localhost:64502/api/Product";
        string URI = "http://localhost:61157/api/Teams";
        int id = 3;
        //string URI = "http://skillsetazureuat.azurewebsites.net/api/associates";
        List<PW_Teams> sample = new List<PW_Teams>();
        MainController API;
        public Form1()
        {
            InitializeComponent();
            API = new MainController(URI);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getAll();
            dataGridView1.DataSource = sample;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getOne(textBox1.Text);
        }

        private void btnInsertProduct_Click(object sender, EventArgs e)
        {
            //Product p = new Product();
            //p.Id = id;
            //p.Name = "Rolex";
            //p.Category = "Watch";
            //p.Price = 1299936;
            //var serializedProduct = JsonConvert.SerializeObject(p);
            //post(serializedProduct);
            ////increment
            //id += 1;
            PW_Teams team = new PW_Teams();
            team.TeamID = new Guid();
            team.TeamDesc = "new";
            team.IsActive = true;
            var serializedTeam = JsonConvert.SerializeObject(team);

            post(serializedTeam);
        }

        private void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            //gather all data to object
            //Product p = new Product();
            //p.Id = 3;
            //p.Name = "Rolex";
            //p.Category = "Watch";
            //p.Price = 1400000; //changed the price
            ////convert object to json string by serializing it
            //var serializedProduct = JsonConvert.SerializeObject(p);
            //put(serializedProduct, p.Id.ToString());

            PW_Teams team = new PW_Teams();
            team.TeamID = new Guid();
            team.TeamDesc = "newagagaga";
            team.IsActive = true;
            var serializedTeam = JsonConvert.SerializeObject(team);

            put(serializedTeam,team.TeamID.ToString());
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
            var products = JsonConvert.DeserializeObject<PW_Teams[]>(await API.get()).ToList();
            sample = products;
        }

        private async void post(string content)
        {
            var postData = await API.post(content);
            MessageBox.Show(postData.ToString());
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
