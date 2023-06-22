# K8sManagementProject
基于NET6+K8S API 的资源管理项目


## 运行结果如图：

### pod展示一
<table>
    <tr>
        <td><img src="https://raw.githubusercontent.com/WuLex/UsefulPicture/main/k8smanagerment/podlist.png"/></td>
        <td><img src="https://raw.githubusercontent.com/WuLex/UsefulPicture/main/k8smanagerment/editpod.png"/></td>
    </tr>
</table>

### pod展示二
<table>
    <tr>
        <td><img src="https://raw.githubusercontent.com/WuLex/UsefulPicture/main/k8smanagerment/PodList_2.png"/></td>
        <td><img src="https://raw.githubusercontent.com/WuLex/UsefulPicture/main/k8smanagerment/UpdatePod.png"/></td>
    </tr>
</table>

### service
<table>
    <tr>
        <td><img src="https://raw.githubusercontent.com/WuLex/UsefulPicture/main/k8smanagerment/ServiceList.png"/></td>
        <td><img src="https://raw.githubusercontent.com/WuLex/UsefulPicture/main/k8smanagerment/UpdateService.png"/></td>
    </tr>
</table>

### kubectl proxy
<table>
    <tr>
        <td><img src="https://raw.githubusercontent.com/WuLex/UsefulPicture/main/k8smanagerment/k8sapi/kubectl_proxy.png"/></td>
    </tr>
</table>

### K8s Api
<table>
    <tr>
        <td><img src="https://raw.githubusercontent.com/WuLex/UsefulPicture/main/k8smanagerment/k8sapi/all_API_1.png"/></td>
        <td><img src="https://raw.githubusercontent.com/WuLex/UsefulPicture/main/k8smanagerment/k8sapi/api_v1_namespaces.png"/></td>
    </tr>
    <tr>
        <td><img src="https://raw.githubusercontent.com/WuLex/UsefulPicture/main/k8smanagerment/k8sapi/api_v1_services.png"/></td>
        <td><img src="https://raw.githubusercontent.com/WuLex/UsefulPicture/main/k8smanagerment/k8sapi/api_v1_pods.png"/></td>
    </tr>
      <tr>
        <td><img src="https://raw.githubusercontent.com/WuLex/UsefulPicture/main/k8smanagerment/k8sapi/apis_apps_v1.png"/></td>
        <td><img src="https://raw.githubusercontent.com/WuLex/UsefulPicture/main/k8smanagerment/k8sapi/apis_apps_v1_repcli.png"/></td>
    </tr>
      <tr>
        <td><img src="https://raw.githubusercontent.com/WuLex/UsefulPicture/main/k8smanagerment/k8sapi/apis_apps_v1_statleful.png"/></td>
        <td><img src="https://raw.githubusercontent.com/WuLex/UsefulPicture/main/k8smanagerment/k8sapi/metrics.png"/></td>
    </tr>
      <tr>
        <td><img src="https://raw.githubusercontent.com/WuLex/UsefulPicture/main/k8smanagerment/k8sapi/version.png"/></td>
    </tr>
</table>

## 常用kubectl命令

### 查看资源状态
```shell
kubectl config view  #查看当前kubectl配置
kubectl version      #查看kubectl版本,查看Kubernetes集群和客户端版本
kubectl cluster-info #查看master和集群服务的地址
kubectl get ns       #查看集群所有命名空间

kubectl get svc      #查看服务的详细信息，显示了服务名称，类型，集群ip，端口，时间等信息
kubectl get svc -n kube-system
kubectl get svc --all-namespaces

kubectl get csr      #获取 CSR 列表
kubectl get nodes              #查看集群节点信息
kubectl get svc -n my-space    #查看指定命名空间的服务
kubectl get namespaces         #查看命名空间
kubectl get all --all-namespaces #查看所有的命名空间
kubectl get all                #查看所有的资源信息
kubectl get deploy -o wide     #
kubectl get deployment         #查看当前命名空间(默认是default)下已经部署了的所有应用，可以看到容器，以及容器所用的镜像，标签等信息
kubectl get services           #列出当前NS中所有service资源,默认是default下

#查看已经创建的pod,-A会显示所有namespace下的pods
kubectl get pods -A   #查看资源对象，查看所有Pod列表
kubectl get pods --all-namespaces #列出集群所有NS中所有的Pod
kubectl get pods --all-namespaces --output wide  #-o wide也比较常用，可以显示更多资源信息，比如pod的IP等

#n-指定namespace，否则默认是default下的pod；-o wide让显示信息更详细，包含所属node、pod的IP等
kubectl get pods -n kube-system  -o wide 
kubectl get pods --namespace=my-space -o wide
kubectl get pod  #查看集群中的pod,默认是default下的pod
kubectl get pod <pod-name> -o wide  #查看Pod详细信息
kubectl get pod <pod-name> -o yaml  #以yaml格式查看Pod详细信息
kubectl get pods --show-labels      #显示pod节点的标签信息

kubectl get pod,svc,ep --show-labels  #查看pod,svc,ep能及标签信息
kubectl get pods --selector=app=cassandra rc -o jsonpath='{.items[*].metadata.labels.version}' # 获取所有具有 app=cassandra 的 pod 中的 version 标签

#获取 ReplicaSets 列表,查看目前所有的replica set，显示了所有的pod的副本数，以及他们的可用数量以及状态等信息
kubectl get replicaset
kubectl get rs
```
### run 命令：在集群中创建并运行一个或多个容器镜像。
```shell
# 示例，运行一个名称为nginx，副本数为3，标签为app=example，镜像为nginx:1.10，端口为80的容器实例
kubectl run nginx --replicas=3 --labels="app=example" --image=nginx:1.10 --port=80

# 示例，运行一个名称为nginx，副本数为3，标签为app=example，镜像为nginx:1.10，端口为80的容器实例，并绑定到k8s-node1上
kubectl run nginx --image=nginx:1.10 --replicas=3 --labels="app=example" --port=80 --overrides='{"apiVersion":"apps/v1","spec":{"template":{"spec":{"nodeSelector":{"kubernetes.io/hostname":"k8s-node1"}}}}}'
```
### expose 命令：创建一个service服务，并且暴露端口让外部可以访问
```shell
#给deployname发布一个服务，-port为暴露出去的端口，-type为服务类型，-target-port为容器端口，port通过clusterip加端口访问，target-port通过节点ip加端口访问。
kubectl expose deployment nginx --port=88 --type=NodePort --target-port=80 --name=nginx-service

kubectl expose deployment web --port=80 --type=NodePort
```

### 创建资源
```shell
kubectl create namespace my-space
kubectl create namespace zerozone

# 创建Deployment和Service资源
kubectl create -f demo-deployment.yaml
kubectl create -f demo-service.yaml
kubectl create -f k8s-web-pod.yaml

kubectl apply -f nginx-service-deployment.yaml

kubectl create deployment my-dep --image=busybox        # 创建一个deplpyme
kubectl expose rc nginx --port=80 --target-port=8000    # 创建一个svc，暴露 nginx 这个rc

```
### 更新资源
```shell
#编辑名为nginx的服务,会直接打开了一个 vim 的编辑界面,修改编辑 保存与 vim 的操作 完全一样
kubectl edit svc nginx

#在YAML中编辑部署nginx-deployment,并将修改的配置保存在其注释中
kubectl edit deployment nginx-deployment -o yaml --save-config

# Edit a pod:
kubectl edit pod/pod_name
# Edit a deployment:
kubectl edit deployment/deployment_name
# Edit a service:
kubectl edit svc/service_name
# Edit a resource using a specific editor:
KUBE_EDITOR=nano kubectl edit resource/resource_name
# Edit a resource in JSON format:
kubectl edit resource/resource_name --output json

```
### 删除资源
```shell
#删除
kubectl delete csr myuser  #删除名为myuser的CSR

kubectl delete -f demo-deployment.yaml       #根据yaml文件删除对应的资源，但是yaml文件并不会被删除，这样更加高效
kubectl delete -f demo-service.yaml

kubectl delete -f xxx.yaml                      # 删除一个配置文件对应的资源对象  
kubectl delete pod,service baz foo              # 删除名字为baz或foo的pod和service  
kubectl delete pods,services -l name=myLabel    # -l 参数可以删除包含指定label的资源对象                            
kubectl delete pod foo --grace-period=0 --force # 强制删除一个pod，在各种原因pod一直terminate不掉的时候很有用



kubectl delete pods --all  #删除所有pods
```

### describe
```shell
#查看svc/pod描述kubectl describe 类型 资源名称 命名空间
kubectl describe svc kubernetes-dashboard -n kube-system
kubectl describe pod kubernetes-dashboard -n kube-system
```

### run
```shell
kubectl run deployname --image=nginx:latest
```

### 文件传输
```shell
# Usage:
kubectl cp <file-spec-src> <file-spec-dest> [options]
# Examples:  
kubectl cp /tmp/foo_dir <some-pod>:/tmp/bar_dir                 # 拷贝宿主机本地文件夹到pod
kubectl cp <some-namespace>/<some-pod>:/tmp/foo /tmp/bar        # 指定namespace的拷贝pod文件到宿主机本地目录
kubectl cp /tmp/foo <some-pod>:/tmp/bar -c <specific-container> # 对于多容器pod，用-c指定容器名

kubectl cp <pod-name>:/etc/hosts /tmp
kubectl cp <pod-name>:/tmp/java.out /root/java.out -c 容器名 -n <namespace>	#必须指定拷贝到本地的文件名,如/root/java.out
kubectl cp /tmp/dir <pod-name>:/tmp/ -c 容器名 -n <namespace>	#拷贝目录为增量覆盖，同名的文件覆盖

```

### kubectl scale pod扩容与缩容
```shell
kubectl scale deployment nginx --replicas 5    # 扩容
kubectl scale deployment nginx --replicas 3    # 缩容
```
### 将Pod的开放端口映射到本地
```shell
kubectl port-forward --address 0.0.0.0 <pod-name> 8888:80 #把Pod的80端口映射到本地的8888端口
kubectl port-forward k8s-net-pod 8090:80
kubectl port-forward my-nginx-df787d567-ng7rm 8090:80 --namespace my-space --address 0.0.0.0
kubectl proxy --address=0.0.0.0 --port=8001 --disable-filter=true &
```

### 执行容器的命令
```shell
kubectl exec <pod-name> date		#执行Pod的date命令，默认使用Pod中的第一个容器执行
kubectl exec <pod-name> -c <container-name> date		#指定Pod中的某个容器执行date命令
kubectl exec -it <pod-name> -c <container-name> /bin/bash		#通过bash获得Pod中某个容器的TTY，相当于登陆容器
```

### kubectl命令中的简写
```shell
certificatesigningrequests (缩写 csr)
componentstatuses (缩写 cs)
configmaps (缩写 cm)
customresourcedefinition (缩写 crd)
daemonsets (缩写 ds)
deployments (缩写 deploy)
endpoints (缩写 ep)
events (缩写 ev)
horizontalpodautoscalers (缩写 hpa)
ingresses (缩写 ing)
limitranges (缩写 limits)
namespaces (缩写 ns)
networkpolicies (缩写 netpol)
nodes (缩写 no)
persistentvolumeclaims (缩写 pvc)
persistentvolumes (缩写 pv)
poddisruptionbudgets (缩写 pdb)
pods (缩写 po)
podsecuritypolicies (缩写 psp)
replicasets (缩写 rs)
replicationcontrollers (缩写 rc)
resourcequotas (缩写 quota)
serviceaccounts (缩写 sa)
services (缩写 svc)
statefulsets (缩写 sts)
storageclasses (缩写 sc)
```
