using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Domain.Entities;
using Lab10_NagelyArpi.Domain.Repositories;
using Lab10_NagelyArpi.Application.Ports.Services;

namespace Lab10_NagelyArpi.Application.UseCases.Responses;

public sealed class CreateResponseUseCase(IResponseRepository responses, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
{
    public async Task<ResponseDto> ExecuteAsync(CreateResponseRequest request, CancellationToken cancellationToken = default)
    {
        if (request.TicketId == Guid.Empty || request.ResponderId == Guid.Empty || string.IsNullOrWhiteSpace(request.Message))
            throw new ArgumentException("Ticket, usuario y mensaje son obligatorios.", nameof(request));
        var response = await responses.AddAsync(new Response
        {
            Id = Guid.NewGuid(), TicketId = request.TicketId, ResponderId = request.ResponderId,
            Message = request.Message.Trim(), CreatedAt = dateTimeProvider.UtcNow
        }, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return new ResponseDto(response.Id, response.TicketId, response.ResponderId, response.Message, response.CreatedAt);
    }
}
