using MediatR;

namespace AtsCommerce.Core.CQRS;

// Query dùng để lấy dữ liệu nhưng không chỉnh sửa database
public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}
