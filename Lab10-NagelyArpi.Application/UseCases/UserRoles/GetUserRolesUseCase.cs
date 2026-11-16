using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Domain.Repositories;

namespace Lab10_NagelyArpi.Application.UseCases.UserRoles;

public sealed class GetUserRolesUseCase(IUserRoleRepository userRoles)
{
    public async Task<IReadOnlyList<UserRoleDto>> ExecuteAsync(Guid userId, CancellationToken cancellationToken = default) =>
        (await userRoles.GetByUserIdAsync(userId, cancellationToken))
            .Select(x => new UserRoleDto(x.UserId, x.RoleId, x.AssignedAt)).ToList();
}
