namespace rubiera.openweathermap
{
    public class City
    {
        string id, name, country;

        public string Id
        {
            get => id;
            set => id = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Country
        {
            get => country;
            set => country = value;
        }
    }
}