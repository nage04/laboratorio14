using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.UserRoles.Commands;

public sealed record RemoveRoleFromUserCommand(Guid UserId, Guid RoleId) : IRequest<bool>;

internal sealed class RemoveRoleFromUserCommandHandler(IUserRoleRepository userRoles, IUnitOfWork unitOfWork)
    : IRequestHandler<RemoveRoleFromUserCommand, bool>
{
    public async Task<bool> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
    {
        var removed = await userRoles.RemoveAsync(request.UserId, request.RoleId, cancellationToken);
        if (removed) await unitOfWork.SaveChangesAsync(cancellationToken);
        return removed;
    }
}
