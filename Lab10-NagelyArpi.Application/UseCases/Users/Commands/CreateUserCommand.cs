using Lab10_NagelyArpi.Domain.Entities;
using Lab10_NagelyArpi.Domain.Repositories;
using Lab10_NagelyArpi.Application.Ports.Services;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Users.Commands;

public sealed record CreateUserCommand(string Username, string PasswordHash, string? Email) : IRequest<Guid>;

internal sealed class CreateUserCommandHandler(
    IUserRepository users,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var username = request.Username?.Trim();
        if (string.IsNullOrWhiteSpace(username) || username.Length is < 3 or > 100)
            throw new ArgumentException("El usuario debe tener entre 3 y 100 caracteres.");
        if (string.IsNullOrWhiteSpace(request.PasswordHash))
            throw new ArgumentException("La contraseña debe estar protegida antes de guardarla.");
        var email = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim();
        if (email is { Length: > 150 }) throw new ArgumentException("El correo no puede superar 150 caracteres.");
        if (await users.GetByUsernameAsync(username, cancellationToken) is not null)
            throw new InvalidOperationException("Ese nombre de usuario ya está registrado.");
        if (email is not null && await users.GetByEmailAsync(email, cancellationToken) is not null)
            throw new InvalidOperationException("Ese correo ya está registrado.");

        var user = new User { Id = Guid.NewGuid(), Username = username, Email = email, PasswordHash = request.PasswordHash };
        await users.AddAsync(user, dateTimeProvider.UtcNow, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}
