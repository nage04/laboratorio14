using Lab10_NagelyArpi.Domain.Entities;
using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Roles.Queries;

public sealed record GetAllRolesQuery : IRequest<IEnumerable<Role>>;

internal sealed class GetAllRolesQueryHandler(IRoleRepository roles)
    : IRequestHandler<GetAllRolesQuery, IEnumerable<Role>>
{
    public async Task<IEnumerable<Role>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken) =>
        await roles.GetAllAsync(cancellationToken);
}
