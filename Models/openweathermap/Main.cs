namespace rubiera.openweathermap
{
    public class Main
    {

        double temp, feels_like;
        int humidity;

        public double Temp
        {
            get => temp;
            set => temp = value;
        }

        public double Feels_Like
        {
            get => feels_like;
            set => feels_like = value;
        }

        public int Humidity
        {
            get => humidity;
            set => humidity = value;
        }
    }
}