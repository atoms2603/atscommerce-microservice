namespace AtsCommerce.Core.Common.Model
{
    public interface IHasPaging
    {
        /// <summary>
        /// PageSize
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// PageNumber
        /// </summary>
        public int PageIndex { get; set; }
    }
}
