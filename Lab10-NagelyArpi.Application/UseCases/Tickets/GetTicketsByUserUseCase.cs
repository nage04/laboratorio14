using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Domain.Repositories;

namespace Lab10_NagelyArpi.Application.UseCases.Tickets;

public sealed class GetTicketsByUserUseCase(ITicketRepository tickets)
{
    public async Task<IReadOnlyList<TicketDto>> ExecuteAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var result = await tickets.GetByUserIdAsync(userId, cancellationToken);
        return result.Select(ticket => new TicketDto(ticket.Id, ticket.UserId, ticket.Title, ticket.Description, ticket.Status, ticket.CreatedAt, ticket.ClosedAt)).ToList();
    }
}
