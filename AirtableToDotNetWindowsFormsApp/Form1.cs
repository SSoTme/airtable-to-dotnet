using airtabletodotnet.Lib.DataClasses;
using AirtableToDotNet.APIWrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirtableToDotNetWindowsFormsApp
{
    public partial class Form1 : Form
    {
        private HelloWorldAirtableAPIWrapper hwaaw;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.hwaaw = new HelloWorldAirtableAPIWrapper(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["baseId"]);
            dataGridView1.DataSource = hwaaw.GetResellers("");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var reseller = dataGridView1.SelectedCells[0].OwningRow.DataBoundItem as Reseller;
            hwaaw.Update(reseller);
        }
    }
}
