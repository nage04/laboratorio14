using Lab10_NagelyArpi.Domain.Entities;
using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Tickets.Queries;

public sealed record GetTicketByIdQuery(Guid TicketId) : IRequest<Ticket?>;

internal sealed class GetTicketByIdQueryHandler(ITicketRepository tickets)
    : IRequestHandler<GetTicketByIdQuery, Ticket?>
{
    public Task<Ticket?> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken) =>
        tickets.GetByIdAsync(request.TicketId, cancellationToken);
}
