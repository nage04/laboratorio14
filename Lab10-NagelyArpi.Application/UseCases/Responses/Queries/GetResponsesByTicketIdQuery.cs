using Lab10_NagelyArpi.Domain.Entities;
using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Responses.Queries;

public sealed record GetResponsesByTicketIdQuery(Guid TicketId) : IRequest<IEnumerable<Response>>;

internal sealed class GetResponsesByTicketIdQueryHandler(IResponseRepository responses)
    : IRequestHandler<GetResponsesByTicketIdQuery, IEnumerable<Response>>
{
    public async Task<IEnumerable<Response>> Handle(GetResponsesByTicketIdQuery request, CancellationToken cancellationToken) =>
        await responses.GetByTicketIdAsync(request.TicketId, cancellationToken);
}
