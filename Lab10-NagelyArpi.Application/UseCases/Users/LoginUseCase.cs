using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Application.Ports.Services;
using Lab10_NagelyArpi.Domain.Repositories;

namespace Lab10_NagelyArpi.Application.UseCases.Users;

public sealed class LoginUseCase(
    IUserRepository users,
    IPasswordVerifier passwordVerifier,
    ITokenService tokenService)
{
    public async Task<TokenDto?> ExecuteAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            return null;

        var user = await users.GetByUsernameAsync(request.Username.Trim(), cancellationToken);
        if (user is null || !passwordVerifier.Verify(request.Password, user.PasswordHash))
            return null;

        var (token, expiresAtUtc) = tokenService.CreateAccessToken(user);
        return new TokenDto(token, expiresAtUtc);
    }
}
