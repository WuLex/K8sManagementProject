using k8s.Models;

namespace K8sManagementApp.Services
{
    public interface IKubernetesService
    {
        Task<List<V1Pod>> GetPodsAsync();
        Task<V1Pod> GetPodAsync(string podName);
        Task CreatePodAsync(V1Pod pod);
        Task UpdatePodAsync(string podName, V1Pod pod);
        Task DeletePodAsync(string podName);
        // 其他方法...
    }
}
