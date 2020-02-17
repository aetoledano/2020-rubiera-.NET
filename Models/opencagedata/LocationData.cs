namespace rubiera.opencagedata
{
    public class LocationData
    {
        string formatted;
        Components components;

        public LocationData()
        {
        }

        public string Formatted
        {
            get => formatted;
            set => formatted = value;
        }

        public Components Components
        {
            get => components;
            set => components = value;
        }
    }
}