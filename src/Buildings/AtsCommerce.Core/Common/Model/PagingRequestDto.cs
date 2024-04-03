using System.ComponentModel.DataAnnotations;

namespace AtsCommerce.Core.Common.Model
{
    public class PagingRequestDto : IHasPaging
    {
        [Range(1, 2147483647)]
        public virtual int PageSize { get; set; } = 10;

        [Range(1, 2147483647)]
        public virtual int PageIndex { get; set; } = 1;

        public int Offset // 1 - 0
        {
            get
            {
                return (PageIndex - 1) * PageSize;
            }
        }

    }
}
