using Lab10_NagelyArpi.Application.Ports.Services;
using Lab10_NagelyArpi.Domain.Entities;
using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.UserRoles.Commands;

public sealed record AssignRoleToUserCommand(Guid UserId, Guid RoleId) : IRequest<bool>;

internal sealed class AssignRoleToUserCommandHandler(
    IUserRoleRepository userRoles,
    IUserRepository users,
    IRoleRepository roles,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : IRequestHandler<AssignRoleToUserCommand, bool>
{
    public async Task<bool> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId == Guid.Empty || request.RoleId == Guid.Empty)
            throw new ArgumentException("Se requieren identificadores válidos de usuario y rol.");
        if (await users.GetByIdAsync(request.UserId, cancellationToken) is null)
            throw new KeyNotFoundException("No se encontró el usuario indicado.");
        if (await roles.GetByIdAsync(request.RoleId, cancellationToken) is null)
            throw new KeyNotFoundException("No se encontró el rol indicado.");
        if ((await userRoles.GetByUserIdAsync(request.UserId, cancellationToken)).Any(x => x.RoleId == request.RoleId))
            return false;
        await userRoles.AddAsync(new UserRole
        {
            UserId = request.UserId,
            RoleId = request.RoleId,
            AssignedAt = dateTimeProvider.UtcNow
        }, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
