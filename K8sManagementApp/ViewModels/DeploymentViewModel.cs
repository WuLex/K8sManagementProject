using k8s.Models;

namespace K8sManagementApp.ViewModels
{
    public class DeploymentViewModel
    {
        public MetadataViewModel Metadata { get; set; }
        public List<ContainerViewModel> Containers { get; set; }
        public string Name { get; set; }
        public int Replicas { get; set; }
        public string Namespace { get; set; }
        public string Image { get; set; }
        public int ContainerPort { get; set; }
        public string Status { get; set; }
        public V1DeploymentSpec Spec { get; set; }

        //public static DeploymentViewModel FromV1Deployment(V1Deployment deployment)
        //{
        //    var vm = new DeploymentViewModel
        //    {
        //        Name = deployment.Metadata.Name,
        //        Namespace = deployment.Metadata.NamespaceProperty,
        //        Replicas = deployment.Spec.Replicas ?? 0,
        //        Image = deployment.Spec.Template.Spec.Containers.First().Image,
        //        ContainerPort = deployment.Spec.Template.Spec.Containers.First().Ports.First().ContainerPort,
        //        Status = deployment.Status?.Conditions?.FirstOrDefault()?.Type
        //    };

        //    return vm;
        //}

    }

}
