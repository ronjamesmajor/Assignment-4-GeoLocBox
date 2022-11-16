using Phidget22;
using System.IO;
using System.Windows.Forms;

namespace _270_GeoLocBox
{
    public partial class Form1 : Form
    {
        public static List<string> Location = new();
        public static string Illuminance;
        public static string Humidty;
        public static string Temperature;
        public DigitalInput btnGreen = null;
        public DigitalInput btnRed = null;
        public GPS gps0 = new GPS();
        public LightSensor ls0 = new LightSensor();
        public HumiditySensor hs0 = new HumiditySensor();
        public TemperatureSensor ts0 = new TemperatureSensor();

        //Port 0 == green
        //Port 1 == red

        //private SqlLiteDataLayer dl = new("Data source=C:/GeoBox/GeoBox.db");
        private static SqlLiteDataLayer dl = new("C:/Users/kayla.purcha/Documents/GeoBox.db");

        public Form1()
        {
            InitializeComponent();

            //Green Button
            btnGreen = new DigitalInput();
            btnGreen.Channel = 0;
            btnGreen.HubPort = 0;
            btnGreen.IsHubPortDevice = true;
            btnGreen.StateChange += btnGreen_StateChange;
            btnGreen.Open(1000);

            //Red Button
            btnRed = new DigitalInput();
            btnRed.Channel = 0;
            btnRed.HubPort = 1;
            btnRed.IsHubPortDevice = true;
            btnRed.StateChange += BtnRed_StateChange;
            btnRed.Open();

            //GPS Sensor
            gps0.Channel = 0;
            gps0.DeviceSerialNumber = 286115;
            gps0.IsHubPortDevice = false;
            gps0.PositionChange += Gps0_PositionChange;
            gps0.Open();

            //Light Sensor
            ls0.Channel = 0;
            ls0.HubPort = 3;
            //ls0.IsHubPortDevice = true;
            ls0.IlluminanceChange += Ls0_IlluminanceChange;
            ls0.Open();

            //Humidity Sensor
            hs0.Channel = 0;
            hs0.HubPort = 4;
            //hs0.IsHubPortDevice = true;
            hs0.HumidityChange += Hs0_HumidityChange;
            hs0.Open();

            //TemperatureSensor
            ts0.Channel = 0;
            ts0.HubPort = 4;
            //ts0.IsHubPortDevice = true;
            ts0.TemperatureChange += Ts0_TemperatureChange;
            ts0.Open();
        }

        private void BtnRed_StateChange(object sender, Phidget22.Events.DigitalInputStateChangeEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Ts0_TemperatureChange(object sender, Phidget22.Events.TemperatureSensorTemperatureChangeEventArgs e)
        {
            Temperature = e.Temperature.ToString();
        }

        private void Hs0_HumidityChange(object sender, Phidget22.Events.HumiditySensorHumidityChangeEventArgs e)
        {
            Humidty = e.Humidity.ToString();
        }

        private void Ls0_IlluminanceChange(object sender, Phidget22.Events.LightSensorIlluminanceChangeEventArgs e)
        {
            Illuminance = e.Illuminance.ToString(); //PERFECT!
        }

        private void btnGreen_StateChange(object sender, Phidget22.Events.DigitalInputStateChangeEventArgs e)
        {
            if (btnGreen.State)
            {
            dl.InsertSensorData(gps0.DateAndTime.Date.ToLocalTime(), Temperature, Humidty, Illuminance);
            
            }
            
            //dl.InsertGeoData(DateTime.Now, "latitude","longitude","altitude"); FOR RED!!!!
        }

        private static void Gps0_PositionChange(object sender, Phidget22.Events.GPSPositionChangeEventArgs e)
        {
            
            Location.Clear();
            Location.Add("Latitude: " + e.Latitude);
            Location.Add("Longitude: " + e.Longitude);
            Location.Add("Altitude: " + e.Altitude);
            //return Location;
        }



       
    }
}