using Lab10_NagelyArpi.Domain.Entities;
using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.UserRoles.Queries;

public sealed record GetUserRolesByUserIdQuery(Guid UserId) : IRequest<IEnumerable<UserRole>>;

internal sealed class GetUserRolesByUserIdQueryHandler(IUserRoleRepository userRoles)
    : IRequestHandler<GetUserRolesByUserIdQuery, IEnumerable<UserRole>>
{
    public async Task<IEnumerable<UserRole>> Handle(GetUserRolesByUserIdQuery request, CancellationToken cancellationToken) =>
        await userRoles.GetByUserIdAsync(request.UserId, cancellationToken);
}
