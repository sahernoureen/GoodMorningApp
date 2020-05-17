using Microsoft.Win32;
using Newtonsoft.Json.Linq;
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

namespace GoodMorningApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FindIpaddressInfo();
            FindTemperatureInfo();
            FindCurrencyExchangeRate();
           
            RegisterInStartup();
        }


        private  void FindIpaddressInfo()
        {
            HttpClient httpClient1 = new HttpClient();
            HttpResponseMessage response1 = httpClient1.GetAsync("https://api.ipdata.co?api-key=18480ad3f417a36564ebff9853dbf2d5d9130d4b1543627fad0d8563").Result;
            if (response1.IsSuccessStatusCode)
            {
                var responseResult1 = response1.Content.ReadAsStringAsync().Result;

                dynamic JsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(responseResult1);
                Ipadd.Text = JsonResult.ip;
                CountryName.Text = JsonResult.country_name;
                City.Text = JsonResult.city;
                Province.Text = JsonResult.region;
            }
            else
            {
                MessageBox.Show("An error Message Occured" + response1.StatusCode + "with reason " + response1.ReasonPhrase);
            
            }

        }


        public void FindTemperatureInfo()
        {
            HttpClient httpClient1 = new HttpClient();
            HttpResponseMessage response1 = httpClient1.GetAsync("https://api.openweathermap.org/data/2.5/weather?q=Winnipeg,Canada&appid=6a2bc05c86367dc11cb3fef1d07e6cb8&units=metric").Result;
            if (response1.IsSuccessStatusCode)
            {
                var responseResult1 = response1.Content.ReadAsStringAsync().Result;
            
                dynamic JsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(responseResult1);
                Curr_Temp.Text = JsonResult.main.temp;
            }
            else
            {
               MessageBox.Show("An error Message Occured" + response1.StatusCode + "with reason " + response1.ReasonPhrase);
            }
        }


        public void FindCurrencyExchangeRate()
        {
            HttpClient httpClient1 = new HttpClient();
            HttpResponseMessage response1 = httpClient1.GetAsync(" https://api.exchangeratesapi.io/latest?symbols=CAD,USD").Result;
            if (response1.IsSuccessStatusCode)
            {
                var responseResult1 = response1.Content.ReadAsStringAsync().Result;

                dynamic JsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(responseResult1);
                canad.Text = JsonResult.rates.CAD;
                USD.Text = JsonResult.rates.USD;
            }
            else
            {
                MessageBox.Show("An error Message Occured" + response1.StatusCode + "with reason " + response1.ReasonPhrase);
            }
        }


        private void RegisterInStartup()
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
           
                registryKey.SetValue("GoodMorningApp", Application.ExecutablePath);
            
         
        }

    }
}
