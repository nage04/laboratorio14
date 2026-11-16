using Lab10_NagelyArpi.Domain.Entities;
using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Tickets.Queries;

public sealed record GetAllTicketsQuery : IRequest<IEnumerable<Ticket>>;

internal sealed class GetAllTicketsQueryHandler(ITicketRepository tickets)
    : IRequestHandler<GetAllTicketsQuery, IEnumerable<Ticket>>
{
    public async Task<IEnumerable<Ticket>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken) =>
        await tickets.GetAllAsync(cancellationToken);
}
