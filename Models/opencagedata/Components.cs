namespace rubiera.opencagedata
{
    public class Components
    {
        public string state, country_code, postcode;

        public Components()
        {
        }

        public string State
        {
            get => state;
            set => state = value;
        }

        public string CountryCode
        {
            get => country_code;
            set => country_code = value;
        }

        public string Postcode
        {
            get => postcode;
            set => postcode = value;
        }
    }
}