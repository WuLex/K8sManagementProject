namespace K8sManagementApp.Models
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class PageQueryParams
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 每页数据量
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// 排序字段加asc/desc
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string Order { get; set; }
    }
}
