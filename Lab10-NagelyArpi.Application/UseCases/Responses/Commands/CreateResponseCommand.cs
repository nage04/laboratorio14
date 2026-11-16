using Lab10_NagelyArpi.Application.Ports.Services;
using Lab10_NagelyArpi.Domain.Entities;
using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Responses.Commands;

public sealed record CreateResponseCommand(Guid TicketId, Guid ResponderId, string Message) : IRequest<Guid>;

internal sealed class CreateResponseCommandHandler(
    IResponseRepository responses,
    ITicketRepository tickets,
    IUserRepository users,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : IRequestHandler<CreateResponseCommand, Guid>
{
    public async Task<Guid> Handle(CreateResponseCommand request, CancellationToken cancellationToken)
    {
        if (request.TicketId == Guid.Empty || request.ResponderId == Guid.Empty || string.IsNullOrWhiteSpace(request.Message))
            throw new ArgumentException("Ticket, usuario y mensaje son obligatorios.");
        if (await tickets.GetByIdAsync(request.TicketId, cancellationToken) is null)
            throw new KeyNotFoundException("No se encontró el ticket indicado.");
        if (await users.GetByIdAsync(request.ResponderId, cancellationToken) is null)
            throw new KeyNotFoundException("No se encontró el usuario que responde.");
        var response = new Response
        {
            Id = Guid.NewGuid(), TicketId = request.TicketId, ResponderId = request.ResponderId,
            Message = request.Message.Trim(), CreatedAt = dateTimeProvider.UtcNow
        };
        await responses.AddAsync(response, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return response.Id;
    }
}
