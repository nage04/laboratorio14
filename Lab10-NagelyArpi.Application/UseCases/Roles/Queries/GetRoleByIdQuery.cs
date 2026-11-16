using Lab10_NagelyArpi.Domain.Entities;
using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Roles.Queries;

public sealed record GetRoleByIdQuery(Guid RoleId) : IRequest<Role?>;

internal sealed class GetRoleByIdQueryHandler(IRoleRepository roles)
    : IRequestHandler<GetRoleByIdQuery, Role?>
{
    public Task<Role?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken) =>
        roles.GetByIdAsync(request.RoleId, cancellationToken);
}
