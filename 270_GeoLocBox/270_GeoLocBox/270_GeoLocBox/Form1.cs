using Phidget22;
using System.IO;
using System.Windows.Forms;

namespace _270_GeoLocBox
{
    public partial class Form1 : Form
    {
        private static List<string> _Location = new();
        private string Illuminance;
        private string Humidty;
        private string Temperature;
        private DigitalInput? btnGreen = null;
        private DigitalInput? btnRed = null;
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
            //_Location.Add("");
            //_Location.Add("");
            //_Location.Add("");
        }

        private void SensorSetup()
        {
            //Green Button
            btnGreen = new DigitalInput();
            btnGreen.Channel = 0;
            btnGreen.HubPort = 0;
            btnGreen.IsHubPortDevice = true;
            btnGreen.StateChange += btnGreen_StateChange;

            //Red Button
            btnRed = new DigitalInput();
            btnRed.Channel = 0;
            btnRed.HubPort = 1;
            btnRed.IsHubPortDevice = true;
            btnRed.StateChange += BtnRed_StateChange;

            //GPS Sensor
            gps0.Channel = 0;
            gps0.DeviceSerialNumber = 286115;
            gps0.IsHubPortDevice = false;
            gps0.PositionChange += Gps0_PositionChange;

            //Light Sensor
            ls0.Channel = 0;
            ls0.HubPort = 3;
            ls0.IlluminanceChange += Ls0_IlluminanceChange;

            //Humidity Sensor
            hs0.Channel = 0;
            hs0.HubPort = 4;
            hs0.HumidityChange += Hs0_HumidityChange;

            //TemperatureSensor
            ts0.Channel = 0;
            ts0.HubPort = 4;
            ts0.TemperatureChange += Ts0_TemperatureChange;

            OpenConnections();
            btnGreen.Open();
            btnRed.Open();
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
                try
                {
                    dl.InsertSensorData(DateTime.Now, Temperature, Humidty, Illuminance);
                }
                catch
                {
                    dl.InsertSensorData(DateTime.Now, Temperature, Humidty, Illuminance);
                }
                DisplayLabels();
            }
        }

        private void BtnRed_StateChange(object sender, Phidget22.Events.DigitalInputStateChangeEventArgs e)
        {
            if (btnRed.State)
            {
                try
                {
                    dl.InsertGeoData(DateTime.Now, _Location[0], _Location[1], _Location[2]);
                }
                catch
                {

                }
                DisplayLabels();
            }
        }

        private void CloseConnections()
        {
            gps0.Close();
            ts0.Close();
            hs0.Close();
            ls0.Close();
        }

        private void OpenConnections()
        {
            gps0.Open();
            ts0.Open();
            hs0.Open();
            ls0.Open();
        }

        private void DisplayLabels()
        {
            lblTime.Invoke(new Action(() => lblTime.Text = $"Time: {DateTime.Now.ToString()}"));
            lblTemp.Invoke(new Action(() => lblTemp.Text = $"Tempurature: {Temperature}"));
            lblHumidity.Invoke(new Action(() => lblHumidity.Text = $"Humidity: {Humidty}"));
            lblLight.Invoke(new Action(() => lblLight.Text = $"Illuminance: {Illuminance}"));
            if (_Location.Count > 0)
            {
                lblLat.Invoke(new Action(() => lblLat.Text = $"Latitiude: {_Location[0]}"));
                lblLong.Invoke(new Action(() => lblLong.Text = $"Longitude: {_Location[1]}"));
                lblAlt.Invoke(new Action(() => lblAlt.Text = $"Altitude: {_Location[2]}"));
            }
            else
            {
                lblLat.Invoke(new Action(() => lblLat.Text = $"Latitiude: No data"));
                lblLong.Invoke(new Action(() => lblLong.Text = $"Longitude: No data"));
                lblAlt.Invoke(new Action(() => lblAlt.Text = $"Altitude: No data"));
            }
        }

        private static void Gps0_PositionChange(object sender, Phidget22.Events.GPSPositionChangeEventArgs e)
        {
            _Location.Clear();
            _Location.Add("Latitude: " + e.Latitude);
            _Location.Add("Longitude: " + e.Longitude);
            _Location.Add("Altitude: " + e.Altitude);
        }
    }
}