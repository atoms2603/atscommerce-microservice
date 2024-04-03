using MediatR;

namespace AtsCommerce.Core.CQRS;

// Command dùng để tương tác chỉnh sửa dữ liệu trong database
public interface ICommand : ICommand<Unit>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}
