using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Users.Queries;

public sealed record GetUserByIdQuery(Guid UserId) : IRequest<UserDto?>;

internal sealed class GetUserByIdQueryHandler(IUserRepository users)
    : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await users.GetByIdAsync(request.UserId, cancellationToken);
        return user is null ? null : new UserDto(user.Id, user.Username, user.Email);
    }
}
