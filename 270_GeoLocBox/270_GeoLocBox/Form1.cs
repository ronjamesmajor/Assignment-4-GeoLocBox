using Phidget22;
using System.IO;
using System.Windows.Forms;

namespace _270_GeoLocBox
{
    public partial class Form1 : Form
    {
        //DigitalInput button1 = null;

        //Allows pathing to database file
        //SQLDataLayer dl = new("Data source=D:/testDB.db");

        public Form1()
        {
            InitializeComponent();

            //Demo code from button quiz thing
            //button1 = new DigitalInput();
            //button1.Channel = 0;
            //button1.HubPort = 0;
            //button1.IsHubPortDevice = true;
            //button1.StateChange += Button1_StateChange;
            //button1.StateChange += Button1_StateChange1;
            //button1.Open(1000);
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    dl.InsertRecord("Test", DateTime.Now);
        //}

        //private void btnLoad_Click(object sender, EventArgs e)
        //{
        //    dataGridView1.DataSource = dl.GetRecords();
        //}

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    var x = dataGridView1.SelectedRows[0];
        //    var z = x.Cells[0].Value;
        //    dl.DeleteRecord(int.Parse((string)z));

        //    dataGridView1.DataSource = dl.GetRecords();
        //}

        //private void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    var x = dataGridView1.SelectedRows[0];
        //    var z = x.Cells[0].Value;
        //    dl.UpdateRecord(int.Parse((string)z), txtTestData.Text, DateTime.Now);

        //    dataGridView1.DataSource = dl.GetRecords();
        //}
    }
}