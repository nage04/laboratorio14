using Lab10_NagelyArpi.Application.Ports.Services;
using Lab10_NagelyArpi.Domain.Entities;
using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Tickets.Commands;

public sealed record CreateTicketCommand(Guid UserId, string Title, string? Description, string Status = "abierto") : IRequest<Guid>;

internal sealed class CreateTicketCommandHandler(
    ITicketRepository tickets,
    IUserRepository users,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : IRequestHandler<CreateTicketCommand, Guid>
{
    public async Task<Guid> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId == Guid.Empty) throw new ArgumentException("Se requiere un usuario válido.");
        if (string.IsNullOrWhiteSpace(request.Title)) throw new ArgumentException("El título es obligatorio.");
        if (await users.GetByIdAsync(request.UserId, cancellationToken) is null)
            throw new KeyNotFoundException("No se encontró el usuario indicado.");
        var status = request.Status.Trim().ToLowerInvariant();
        if (status is not ("abierto" or "en_proceso" or "cerrado"))
            throw new ArgumentException("El estado debe ser abierto, en_proceso o cerrado.");
        var ticket = new Ticket
        {
            Id = Guid.NewGuid(), UserId = request.UserId, Title = request.Title.Trim(),
            Description = request.Description?.Trim(), Status = status,
            CreatedAt = dateTimeProvider.UtcNow,
            ClosedAt = status == "cerrado" ? dateTimeProvider.UtcNow : null
        };
        await tickets.AddAsync(ticket, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return ticket.Id;
    }
}
