using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Domain.Repositories;

namespace Lab10_NagelyArpi.Application.UseCases.Users;

public sealed class GetUserByIdUseCase(IUserRepository users)
{
    public async Task<UserDto?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await users.GetByIdAsync(id, cancellationToken);
        return user is null ? null : new UserDto(user.Id, user.Username, user.Email);
    }
}
