namespace rubiera
{
    public class WeatherInfo
    {
        string name, skyImageUri, sky, skyDesc;
        double temp, actualFeel;
        int humidityPercentage, cloudsPercentage;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string SkyImageUri
        {
            get => skyImageUri;
            set => skyImageUri = value;
        }

        public string Sky
        {
            get => sky;
            set => sky = value;
        }

        public string SkyDesc
        {
            get => skyDesc;
            set => skyDesc = value;
        }

        public double Temp
        {
            get => temp;
            set => temp = value;
        }

        public double ActualFeel
        {
            get => actualFeel;
            set => actualFeel = value;
        }

        public int HumidityPercentage
        {
            get => humidityPercentage;
            set => humidityPercentage = value;
        }

        public int CloudsPercentage
        {
            get => cloudsPercentage;
            set => cloudsPercentage = value;
        }
    }
}