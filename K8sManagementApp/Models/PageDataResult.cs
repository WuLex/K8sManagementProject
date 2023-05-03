namespace K8sManagementApp.Models
{
    public class PageDataResult<T> where T : class
    {
        public PageDataResult()
        {
        }

        public PageDataResult(List<T> data, int totalCount)
        {
            Data = data;
            Count = totalCount;
        }

        public List<T> Data { get; set; }

        public int Count { get; set; }

        /// <summary>
        /// 成功状态（200表示成功）
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 结果消息
        /// </summary>
        public string Msg { get; set; }
    }
}
