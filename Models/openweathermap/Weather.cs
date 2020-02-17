namespace rubiera.openweathermap
{
    public class Weather
    {
        string main, description, icon;

        public string Main
        {
            get => main;
            set => main = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public string Icon
        {
            get => icon;
            set => icon = value;
        }
    }
}