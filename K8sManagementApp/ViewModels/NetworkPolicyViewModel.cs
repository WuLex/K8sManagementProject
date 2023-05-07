using K8sManagementApp.Models;

namespace K8sManagementApp.ViewModels
{
    public class NetworkPolicyViewModel
    {
        public string Name { get; set; }
        public Dictionary<string, string> Labels { get; set; }
        public List<NetworkPolicyRule> Rules { get; set; }
        public List<string> AvailableApps { get; set; }
    }
}