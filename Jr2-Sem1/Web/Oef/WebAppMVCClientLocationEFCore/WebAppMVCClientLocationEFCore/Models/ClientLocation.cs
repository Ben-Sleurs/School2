namespace WebAppMVCClientLocationEFCore.Models
{
    public class ClientLocation
    {
        public ClientLocation(string clientName, string city)
        {
            this.ClientName=clientName;
            this.City=city;
        }
        public string ClientName { get; set; }
        public string City { get; set; }
    }
}
