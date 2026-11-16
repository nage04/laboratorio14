using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Tickets.Queries;

public sealed record GetTicketsByUserQuery(Guid UserId) : IRequest<IEnumerable<TicketDto>>;

internal sealed class GetTicketsByUserQueryHandler(ITicketRepository tickets)
    : IRequestHandler<GetTicketsByUserQuery, IEnumerable<TicketDto>>
{
    public async Task<IEnumerable<TicketDto>> Handle(GetTicketsByUserQuery request, CancellationToken cancellationToken) =>
        (await tickets.GetByUserIdAsync(request.UserId, cancellationToken))
            .Select(ticket => new TicketDto(ticket.Id, ticket.UserId, ticket.Title, ticket.Description,
                ticket.Status, ticket.CreatedAt, ticket.ClosedAt));
}
