using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Application.Ports.Services;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Users.Commands;

public sealed record RegisterUserCommand(string Username, string Password, string? Email) : IRequest<UserDto>;

internal sealed class RegisterUserCommandHandler(IPasswordService passwords, ISender sender)
    : IRequestHandler<RegisterUserCommand, UserDto>
{
    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 8)
            throw new ArgumentException("La contraseña debe tener al menos 8 caracteres.");
        var passwordHash = passwords.Hash(request.Password);
        var id = await sender.Send(new CreateUserCommand(request.Username, passwordHash, request.Email), cancellationToken);
        return new UserDto(id, request.Username.Trim(), string.IsNullOrWhiteSpace(request.Email) ? null : request.Email.Trim());
    }
}
