using Lab10_NagelyArpi.Application.Ports.Services;
using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Tickets.Commands;

public sealed record UpdateTicketStatusCommand(Guid TicketId, string Status) : IRequest<bool>;

internal sealed class UpdateTicketStatusCommandHandler(
    ITicketRepository tickets,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : IRequestHandler<UpdateTicketStatusCommand, bool>
{
    public async Task<bool> Handle(UpdateTicketStatusCommand request, CancellationToken cancellationToken)
    {
        var status = request.Status?.Trim().ToLowerInvariant();
        if (status is not ("abierto" or "en_proceso" or "cerrado"))
            throw new ArgumentException("El estado debe ser abierto, en_proceso o cerrado.");
        var updated = await tickets.UpdateStatusAsync(
            request.TicketId, status, status == "cerrado" ? dateTimeProvider.UtcNow : null, cancellationToken);
        if (updated) await unitOfWork.SaveChangesAsync(cancellationToken);
        return updated;
    }
}
