using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Users.Queries;

public sealed record GetAllUsersQuery : IRequest<IEnumerable<UserDto>>;

internal sealed class GetAllUsersQueryHandler(IUserRepository users)
    : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken) =>
        (await users.GetAllAsync(cancellationToken))
            .Select(user => new UserDto(user.Id, user.Username, user.Email));
}
