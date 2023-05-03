using k8s.Models;
using k8s;
using System.Xml.Linq;

namespace K8sManagementApp.Services
{
    public class K8sService
    {
        private Kubernetes _kubernetes;

        public K8sService(Kubernetes kubernetes)
        {
            _kubernetes = kubernetes;
        }

        // 创建 Pod
        public async Task<V1Pod> CreatePodAsync(V1Pod pod)
        {
            return await _kubernetes.CreateNamespacedPodAsync(pod,pod.Namespace());
        }

        // 更新 Pod
        public async Task<V1Pod> UpdatePodAsync(V1Pod pod)
        {
            return await _kubernetes.ReplaceNamespacedPodAsync(pod, pod.Name(), pod.Namespace());
        }

        // 删除 Pod
        public async Task DeletePod(string podName, string namespaceName)
        {
            _ =await _kubernetes.DeleteNamespacedPodAsync(podName, namespaceName);
        }

        // 创建 Service
        public async Task<V1Service> CreateService(V1Service service)
        {
            return await _kubernetes.CreateNamespacedServiceAsync(service,service.Namespace());
        }

        // 更新 Service
        public async Task<V1Service> UpdateService(V1Service service)
        {
            return  await _kubernetes.ReplaceNamespacedServiceAsync(service,service.Name(),service.Namespace());
        }

        // 删除 Service
        public async Task DeleteServiceAsync(string serviceName, string namespaceName)
        {
            await _kubernetes.DeleteNamespacedServiceAsync(serviceName, namespaceName);
        }
    }
}
