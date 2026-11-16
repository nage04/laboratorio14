using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Application.Ports.Services;
using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Users.Commands;

public sealed record LoginCommand(string Username, string Password) : IRequest<TokenDto?>;

internal sealed class LoginCommandHandler(IUserRepository users, IPasswordService passwords, ITokenService tokens)
    : IRequestHandler<LoginCommand, TokenDto?>
{
    public async Task<TokenDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password)) return null;
        var user = await users.GetByUsernameAsync(request.Username.Trim(), cancellationToken);
        if (user is null || !passwords.Verify(request.Password, user.PasswordHash)) return null;
        var (token, expiresAtUtc) = tokens.CreateAccessToken(user);
        return new TokenDto(token, expiresAtUtc);
    }
}
