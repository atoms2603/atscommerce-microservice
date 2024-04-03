namespace AtsCommerce.Core.Common.Model
{
    public class PagingResult<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long TotalPages { get; set; }
        public int TotalCount { get; set; }
        public List<T> Data { get; set; } = new List<T>();
        public PagingResult(List<T> data, int totalCount, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            Data = data;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public PagingResult(int totalCount, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            Data = new List<T>();
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
