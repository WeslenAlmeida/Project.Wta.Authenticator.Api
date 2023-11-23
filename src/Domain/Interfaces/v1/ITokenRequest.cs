using MediatR;

namespace Domain.Interfaces.v1
{
    public interface ITokenRequest<out TRespone> : IRequest<TRespone>
    {
    }
}