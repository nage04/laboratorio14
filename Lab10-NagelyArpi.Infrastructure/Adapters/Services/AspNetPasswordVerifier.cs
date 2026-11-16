using Lab10_NagelyArpi.Application.Ports.Services;
using Lab10_NagelyArpi.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lab10_NagelyArpi.Infrastructure.Adapters.Services;

public sealed class AspNetPasswordVerifier : IPasswordVerifier
{
    private readonly PasswordHasher<User> _passwordHasher = new();

    public bool Verify(string password, string passwordHash) =>
        _passwordHasher.VerifyHashedPassword(new User(), passwordHash, password)
        is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
}
