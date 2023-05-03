namespace K8sManagementApp.Models
{
    public class KubernetesConfig
    {
        public string ApiServerUrl { get; set; }
        public string AccessToken { get; set; }
        public string ClientCertificateData { get; set; }
        public string ClientKeyData { get; set; }
        public bool SkipTlsVerify { get; set; }
        public string Namespace { get; set; }
    }
}
