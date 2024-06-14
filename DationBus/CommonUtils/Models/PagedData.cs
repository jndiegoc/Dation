namespace CommonUtils.Models
{
    public class PagedData<T>
    {
        public List<T> Data { get; set; } = new List<T>();
        public int TotalRecords { get; set; }
    }
}
