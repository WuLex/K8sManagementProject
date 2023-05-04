namespace K8sManagementApp.Extensions
{
    public static class DictionaryExtensions
    {
        public static string GetString<K, V>(this IDictionary<K, V> dict)
        {
            var items = dict.Select(kvp => kvp.ToString());
            return string.Join(", ", items);
        }
    }
}
