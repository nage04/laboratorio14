using Lab10_NagelyArpi.Domain.Repositories;

namespace Lab10_NagelyArpi.Application.UseCases.UserRoles;

public sealed class RemoveRoleUseCase(IUserRoleRepository userRoles, IUnitOfWork unitOfWork)
{
    public async Task<bool> ExecuteAsync(Guid userId, Guid roleId, CancellationToken cancellationToken = default)
    {
        var removed = await userRoles.RemoveAsync(userId, roleId, cancellationToken);
        if (removed) await unitOfWork.SaveChangesAsync(cancellationToken);
        return removed;
    }
}
