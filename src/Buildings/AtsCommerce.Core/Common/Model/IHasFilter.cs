namespace AtsCommerce.Core.Common.Model
{
    public interface IHasFilter
    {
        public string FilterBy { get; set; }

        public string FilterValue { get; set; }
    }
}
