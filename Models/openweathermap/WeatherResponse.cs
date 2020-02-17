using System.Collections.Generic;

namespace rubiera.openweathermap
{
    public class WeatherResponse
    {
        List<Weather> weather;
        Main main;
        Clouds clouds;

        string name;
        int cod;
        long dt;

        public List<Weather> Weather
        {
            get => weather;
            set => weather = value;
        }

        public Main Main
        {
            get => main;
            set => main = value;
        }

        public Clouds Clouds
        {
            get => clouds;
            set => clouds = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Cod
        {
            get => cod;
            set => cod = value;
        }

        public long Dt
        {
            get => dt;
            set => dt = value;
        }
    }
}