namespace K8sManagementApp.Models
{
    public class NetworkPolicyRule
    {
        public List<string> Ports { get; set; }
        public List<string> Sources { get; set; }
        public List<string> Destinations { get; set; }
    }
}
