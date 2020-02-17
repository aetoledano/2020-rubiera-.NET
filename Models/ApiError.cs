namespace rubiera
{
    public class ApiError
    {
        private int code;
        private string msg;

        public int Code
        {
            get => code;
            set => code = value;
        }

        public string Msg
        {
            get => msg;
            set => msg = value;
        }
    }
}