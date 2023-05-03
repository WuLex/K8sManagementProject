namespace K8sManagementApp.ViewModels
{
    public class ReplicaSetViewModel
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Labels { get; set; }
        public string Selector { get; set; }
        public string Template { get; set; }
        public int Replicas { get; set; }
        public List<ContainerViewModel> Containers { get; set; }
        public List<DeploymentViewModel> Deployments { get; set; }
    }
}
