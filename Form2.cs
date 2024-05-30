using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpApi
{
    public partial class Form2 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void btnGet_Click(object sender, EventArgs e)
        {
            try
            {
                txtOutput.Clear();
                HttpResponseMessage response = await client.GetAsync("http://localhost/myapi/phpapi/apis.php?table=transactions");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                txtOutput.Text = responseBody;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void btnPost_Click(object sender, EventArgs e)
        {
            var transactionData = new
            {
                table = "transactions",
                food_item = txtFoodItem.Text,
                price = txtPrice.Text,
                paid_amount = txtPaidAmount.Text,
                balance = txtBalance.Text
            };
            string json = JsonConvert.SerializeObject(transactionData);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync("http://localhost/myapi/phpapi/apis.php", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                txtOutput.Text = responseBody;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private async void recordbtn_Click(object sender, EventArgs e)
        {
            try
            {
                txtOutput.Clear();

                // Fetch accounts data
                HttpResponseMessage responseAccounts = await client.GetAsync("http://localhost/myapi/phpapi/apis.php?table=accounts");
                responseAccounts.EnsureSuccessStatusCode();
                string responseBodyAccounts = await responseAccounts.Content.ReadAsStringAsync();

                // Fetch transactions data
                HttpResponseMessage responseTransactions = await client.GetAsync("http://localhost/myapi/phpapi/apis.php?table=transactions");
                responseTransactions.EnsureSuccessStatusCode();
                string responseBodyTransactions = await responseTransactions.Content.ReadAsStringAsync();

                // Combine the data
                string combinedResponse = "Accounts:\n" + responseBodyAccounts + "\n\nTransactions:\n" + responseBodyTransactions;
                txtOutput.Text = combinedResponse;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
