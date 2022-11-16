using Phidget22;
using System.IO;
using System.Windows.Forms;

namespace _270_GeoLocBox
{
    public partial class Form1 : Form
    {
        DigitalInput btnGreen = null;

        //Port 0 == green
        //Port 1 == red

        private SqlLiteDataLayer dl = new("Data source=C:/GeoBox/GeoBox.db");

        public Form1()
        {
            InitializeComponent();

            //Demo code from button quiz thing
            btnGreen = new DigitalInput();
            btnGreen.Channel = 0;
            btnGreen.HubPort = 0;
            btnGreen.IsHubPortDevice = true;
            btnGreen.StateChange += btnGreen_StateChange;
            btnGreen.Open(1000);
        }

        private void btnGreen_StateChange(object sender, Phidget22.Events.DigitalInputStateChangeEventArgs e)
        {
            
            dl.InsertSensorData();
            dl.InsertGeoData();
        }

        private static void Gps0_PositionChange(object sender, Phidget22.Events.GPSPositionChangeEventArgs e)
        {
            List<string> Location = new();
            Location.Add("Latitude: " + e.Latitude);
            Location.Add("Longitude: " + e.Longitude);
            Location.Add("Altitude: " + e.Altitude);
            //return Location;
        }

        static void Main(string[] args)
        {
            GPS gps0 = new GPS();
            gps0.DateAndTime.Date.ToLocalTime();
            gps0.PositionChange += Gps0_PositionChange;

            gps0.Open(5000);

            //Wait until Enter has been pressed before exiting
            Console.ReadLine();

            gps0.Close();
        }
    }
}