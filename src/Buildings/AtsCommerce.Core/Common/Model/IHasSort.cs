namespace AtsCommerce.Core.Common.Model
{
    public interface IHasSort
    {
        /// <summary>
        /// SortBy
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// SortOrder
        /// </summary>
        public string? SortOrder { get; set; }
    }
}
