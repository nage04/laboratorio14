using Lab10_NagelyArpi.Domain.Entities;
using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Roles.Commands;

public sealed record CreateRoleCommand(string RoleName) : IRequest<Guid>;

internal sealed class CreateRoleCommandHandler(IRoleRepository roles, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateRoleCommand, Guid>
{
    public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var name = request.RoleName?.Trim();
        if (string.IsNullOrWhiteSpace(name) || name.Length > 50)
            throw new ArgumentException("El nombre del rol es obligatorio y no puede superar 50 caracteres.");
        var role = new Role { Id = Guid.NewGuid(), Name = name };
        await roles.AddAsync(role, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return role.Id;
    }
}
