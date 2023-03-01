namespace MicroserviceMail.Domain
{
    public class ConfigurationSettingsMail
    {
        public string SMTP { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string User { get; set; }
        public string Passwd { get; set; }
    }
}
