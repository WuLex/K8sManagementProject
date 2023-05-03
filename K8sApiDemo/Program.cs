//// 配置 KubernetesClient
//using k8s;
//using k8s.Models;

////https://github.com/idawnlight/GZCTF/blob/46eeeaee8eb657c750534bb2f27f84d950850bc0/GZCTF/Services/K8sService.cs
////https://github.com/GZTimeWalker/GZCTF/blob/167381aab8674f80659684e917d70c09bc6fbc3e/src/GZCTF/Services/K8sService.cs


const string Namespace = "gzctf-challenges";
const string NetworkPolicy = "gzctf-policy";

//var config = new KubernetesClientConfiguration
//{
//    // 设置集群地址
//    Host = "https://<kubernetes-api-url>",
//    // 设置认证信息，例如使用 token 进行身份验证
//    AccessToken = "<access-token>",
//    // 可选：设置客户端证书和私钥
//    ClientCertificateData = "<base64-encoded-client-certificate>",
//    ClientCertificateKeyData = "<base64-encoded-client-key>",
//    // 可选：设置 CA 证书
//     SslCaCerts= "<base64-encoded-ca-certificate>"
//    //CaCertificateData = "<base64-encoded-ca-certificate>"
//};

//// 创建 KubernetesClient 实例
//var kubernetesClient = new Kubernetes(config);


//// 创建 Pod
//var pod = new V1Pod
//{
//    Metadata = new V1ObjectMeta
//    {
//        Name = "my-pod",
//        NamespaceProperty = "default" // 设置 Pod 所在的命名空间
//    },
//    Spec = new V1PodSpec
//    {
//        Containers = new List<V1Container>
//        {
//            new V1Container
//            {
//                Name = "my-container",
//                Image = "nginx:latest", // 设置容器的镜像
//                Ports = new List<V1ContainerPort>
//                {
//                    new V1ContainerPort
//                    {
//                        ContainerPort = 80 // 设置容器的端口
//                    }
//                }
//            }
//        }
//    }
//};

//var createdPod = await kubernetesClient.CreateNamespacedPodAsync(pod,""); // 创建 Pod

//// 更新 Pod
//createdPod.Metadata.Labels = new Dictionary<string, string>
//{
//    { "app", "nginx" }
//};
//var updatedPod = await kubernetesClient.POD(createdPod); // 更新 Pod

//// 删除 Pod
//await kubernetesClient.DeleteNamespacedPodAsync("my-pod", "default"); // 删除 Pod

//// 获取 Pod 列表
//var podList = await kubernetesClient.GetPodsAsync(); // 获取 Pod 列表