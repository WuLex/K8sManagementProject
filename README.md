# K8sManagementProject
基于NET6+K8S API 的资源管理项目


## 运行结果如图：


## 常用kubectl命令
```shell
#查看当前kubectl配置
kubectl config view
#查看kubectl版本
kubectl version
kubectl cluster-info
 kubectl get csr
#删除
kubectl delete csr myuser
kubectl get ns
kubectl get svc --all-namespaces
#查看集群节点
kubectl get nodes
kubectl create namespace my-space
kubectl create namespace zerozone
kubectl get svc -n my-space

kubectl apply -f nginx-service-deployment.yaml
kubectl create -f k8s-web-pod.yaml
#查看命名空间
kubectl get namespaces
kubectl get all
kubectl get deployment
kubectl get services

#查看已经创建的pod,-A会显示所有namespace下的pods
kubectl  get pods -A

#n-指定namespace，否则默认是default下的pod；-o wide让显示信息更详细，包含所属node、pod的IP等
kubectl get pods -n kube-system  -o wide 
kubectl get pods --namespace=my-space -o wide
kubectl  get pod


kubectl get pods --show-labels  


#获取 ReplicaSets 列表
kubectl get replicaset
kubectl get rs
```


```shell
#查看svc/pod描述kubectl describe 类型 资源名称 命名空间
kubectl describe svc kubernetes-dashboard -n kube-system
kubectl describe pod kubernetes-dashboard -n kube-system
```

```shell
kubectl port-forward k8s-net-pod 8090:80
kubectl port-forward my-nginx-df787d567-ng7rm 8090:80 --namespace my-space --address 0.0.0.0
kubectl proxy --address=0.0.0.0 --port=8001 --disable-filter=true &
```
