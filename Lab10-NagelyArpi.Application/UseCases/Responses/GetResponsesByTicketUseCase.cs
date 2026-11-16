using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Domain.Repositories;

namespace Lab10_NagelyArpi.Application.UseCases.Responses;

public sealed class GetResponsesByTicketUseCase(IResponseRepository responses)
{
    public async Task<IReadOnlyList<ResponseDto>> ExecuteAsync(Guid ticketId, CancellationToken cancellationToken = default) =>
        (await responses.GetByTicketIdAsync(ticketId, cancellationToken))
            .Select(x => new ResponseDto(x.Id, x.TicketId, x.ResponderId, x.Message, x.CreatedAt)).ToList();
}
