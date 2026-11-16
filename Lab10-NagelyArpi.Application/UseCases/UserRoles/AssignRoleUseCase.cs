using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Domain.Entities;
using Lab10_NagelyArpi.Domain.Repositories;
using Lab10_NagelyArpi.Application.Ports.Services;

namespace Lab10_NagelyArpi.Application.UseCases.UserRoles;

public sealed class AssignRoleUseCase(IUserRoleRepository userRoles, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
{
    public async Task<UserRoleDto> ExecuteAsync(AssignRoleRequest request, CancellationToken cancellationToken = default)
    {
        if (request.UserId == Guid.Empty || request.RoleId == Guid.Empty)
            throw new ArgumentException("Se requieren identificadores válidos de usuario y rol.", nameof(request));
        var assignment = await userRoles.AddAsync(new UserRole
        {
            UserId = request.UserId, RoleId = request.RoleId, AssignedAt = dateTimeProvider.UtcNow
        }, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new UserRoleDto(assignment.UserId, assignment.RoleId, assignment.AssignedAt);
    }
}
