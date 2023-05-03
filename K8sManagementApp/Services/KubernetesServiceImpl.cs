using k8s.Models;
using System.Net;
using System.Text.Json;
using System.Text;

namespace K8sManagementApp.Services
{
    /// <summary>
    /// 不使用组件，直接自定义http请求
    /// </summary>
    public class KubernetesServiceImpl : IKubernetesService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public KubernetesServiceImpl(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<V1Pod>> GetPodsAsync()
        {
            var apiUrl = _configuration["KubernetesApiUrl"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/v1/namespaces/default/pods");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var pods = JsonSerializer.Deserialize<V1PodList>(content);
                return (pods?.Items)?.ToList();
            }
            else
            {
                throw new Exception($"Failed to get pods: {response.StatusCode}");
            }
        }

        public async Task<V1Pod> GetPodAsync(string podName)
        {
            var apiUrl = _configuration["KubernetesApiUrl"];
            var response = await _httpClient.GetAsync($"{apiUrl}/api/v1/namespaces/default/pods/{podName}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var pod = JsonSerializer.Deserialize<V1Pod>(content);
                return pod;
            }
            else
            {
                throw new Exception($"Failed to get pod {podName}: {response.StatusCode}");
            }
        }

        public async Task CreatePodAsync(V1Pod pod)
        {
            var apiUrl = _configuration["KubernetesApiUrl"];
            var json = JsonSerializer.Serialize(pod);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{apiUrl}/api/v1/namespaces/default/pods", content);
            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception($"Failed to create pod: {response.StatusCode}");
            }
        }

        public async Task UpdatePodAsync(string podName, V1Pod pod)
        {
            var apiUrl = _configuration["KubernetesApiUrl"];
            var json = JsonSerializer.Serialize(pod);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{apiUrl}/api/v1/namespaces/default/pods/{podName}", content);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Failed to update pod {podName}: {response.StatusCode}");
            }
        }

        public async Task DeletePodAsync(string podName)
        {
            var apiUrl = _configuration["KubernetesApiUrl"];
            var response = await _httpClient.DeleteAsync($"{apiUrl}/api/v1/namespaces/default/pods/{podName}");
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new Exception($"Failed to delete pod {podName}: {response.StatusCode}");
            }
        }
    }
}
