using Phidget22;
using System.IO;
using System.Windows.Forms;

namespace _270_GeoLocBox
{
    public partial class Form1 : Form
    {
        private static List<string> Location = new();
        private string Illuminance;
        private string Humidty;
        private string Temperature;
        private DigitalInput btnGreen = null;
        private DigitalInput btnRed = null;
        private GPS gps0 = new GPS();
        private LightSensor ls0 = new LightSensor();
        private HumiditySensor hs0 = new HumiditySensor();
        private TemperatureSensor ts0 = new TemperatureSensor();

        private SqlLiteDataLayer dl = new("data source=C:/DataBaseThings/GeoLocation.db");
        //private SqlLiteDataLayer dl = new("data source=C:/Users/kayla.purcha/Documents/GeoBox.db");

        public Form1()
        {
            InitializeComponent();
            SensorSetup();
        }

        private void SensorSetup()
        {
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
            btnRed.Open(1000);

            //GPS Sensor
            gps0.Channel = 0;
            gps0.DeviceSerialNumber = 286115;
            gps0.IsHubPortDevice = false;
            gps0.PositionChange += Gps0_PositionChange;
            gps0.Open();

            //Light Sensor
            ls0.Channel = 0;
            ls0.HubPort = 3;
            ls0.IlluminanceChange += Ls0_IlluminanceChange;
            ls0.Open();

            //Humidity Sensor
            hs0.Channel = 0;
            hs0.HubPort = 4;
            hs0.HumidityChange += Hs0_HumidityChange;
            hs0.Open();

            //TemperatureSensor
            ts0.Channel = 0;
            ts0.HubPort = 4;
            ts0.TemperatureChange += Ts0_TemperatureChange;
            ts0.Open();
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
                try
                {
                    dl.InsertSensorData(DateTime.Now, Temperature, Humidty, Illuminance);
                    DisplayLabels();
                }
                catch
                {
                    dl.InsertSensorData(DateTime.Now, Temperature, Humidty, Illuminance);
                    DisplayLabels();
                }
        }

        private void BtnRed_StateChange(object sender, Phidget22.Events.DigitalInputStateChangeEventArgs e)
        {
            if(btnRed.State)
                try
                {
                    dl.InsertGeoData(DateTime.Now, Location[0], Location[1], Location[2]);
                    DisplayLabels();
                }
                catch
                {
                    if (Location.Count > 0)
                        DisplayLabels();
                    else
                        dl.InsertGeoData(DateTime.Now, "No location", "No location", "No location");
                }
        }

        private void DisplayLabels()
        {
            lblTemp.Text = $"Tempurature: {Temperature}";
            lblHumidity.Text = $"Humidity: {Humidty}";
            lblLight.Text = $"Illuminance: {Illuminance}";
            lblLat.Text = $"Latitiude: {Location[0]}";
            lblLong.Text = $"Longitude: {Location[1]}";
            lblAlt.Text = $"Altitude: {Location[2]}";
        }

        private static void Gps0_PositionChange(object sender, Phidget22.Events.GPSPositionChangeEventArgs e)
        {            
            Location.Clear();
            Location.Add("Latitude: " + e.Latitude);
            Location.Add("Longitude: " + e.Longitude);
            Location.Add("Altitude: " + e.Altitude);
        }       
    }
}