namespace e_commerce_website.Configs
    {
    public class ServerConfig
        {
        public HttpConfig Http { get; set; }
        }

    public class HttpConfig
        {
        public string Server { get; set; }
        public int Port { get; set; }
        public string RootUrl { get; set; }
        }
    }
