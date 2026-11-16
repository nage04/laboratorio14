using Lab10_NagelyArpi.Domain.Entities;
using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Users.Commands;

public sealed record UpdateUserCommand(Guid UserId, string Username, string? Email) : IRequest<bool>;

internal sealed class UpdateUserCommandHandler(IUserRepository users, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateUserCommand, bool>
{
    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var username = request.Username?.Trim();
        if (string.IsNullOrWhiteSpace(username) || username.Length is < 3 or > 100)
            throw new ArgumentException("El usuario debe tener entre 3 y 100 caracteres.");
        var current = await users.GetByIdAsync(request.UserId, cancellationToken);
        if (current is null) return false;
        var existingName = await users.GetByUsernameAsync(username, cancellationToken);
        if (existingName is not null && existingName.Id != request.UserId)
            throw new InvalidOperationException("Ese nombre de usuario ya está registrado.");
        var email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim();
        if (email is { Length: > 150 }) throw new ArgumentException("El correo no puede superar 150 caracteres.");
        if (email is not null)
        {
            var existingEmail = await users.GetByEmailAsync(email, cancellationToken);
            if (existingEmail is not null && existingEmail.Id != request.UserId)
                throw new InvalidOperationException("Ese correo ya está registrado.");
        }
        var updated = await users.UpdateAsync(new User
        {
            Id = request.UserId,
            Username = username,
            Email = email,
            PasswordHash = current.PasswordHash
        }, cancellationToken);
        if (updated) await unitOfWork.SaveChangesAsync(cancellationToken);
        return updated;
    }
}
