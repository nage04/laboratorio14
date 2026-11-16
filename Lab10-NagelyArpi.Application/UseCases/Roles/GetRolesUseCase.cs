using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Domain.Repositories;

namespace Lab10_NagelyArpi.Application.UseCases.Roles;

public sealed class GetRolesUseCase(IRoleRepository roles)
{
    public async Task<IReadOnlyList<RoleDto>> ExecuteAsync(CancellationToken cancellationToken = default) =>
        (await roles.GetAllAsync(cancellationToken)).Select(role => new RoleDto(role.Id, role.Name)).ToList();
}
