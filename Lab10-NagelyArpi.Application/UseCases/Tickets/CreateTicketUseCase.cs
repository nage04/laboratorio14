using Lab10_NagelyArpi.Application.DTOs;
using Lab10_NagelyArpi.Domain.Entities;
using Lab10_NagelyArpi.Domain.Repositories;
using Lab10_NagelyArpi.Application.Ports.Services;

namespace Lab10_NagelyArpi.Application.UseCases.Tickets;

public sealed class CreateTicketUseCase(ITicketRepository tickets, IUserRepository users, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
{
    public async Task<TicketDto> ExecuteAsync(CreateTicketRequest request, CancellationToken cancellationToken = default)
    {
        if (request.UserId == Guid.Empty)
            throw new ArgumentException("Se requiere un usuario válido.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.Title))
            throw new ArgumentException("El título del ticket es obligatorio.", nameof(request));
        if (await users.GetByIdAsync(request.UserId, cancellationToken) is null)
            throw new KeyNotFoundException("No se encontró el usuario indicado.");

        var ticket = await tickets.AddAsync(new Ticket
        {
            Id = Guid.NewGuid(), UserId = request.UserId, Title = request.Title.Trim(),
            Description = request.Description?.Trim(), Status = "Open", CreatedAt = dateTimeProvider.UtcNow
        }, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new TicketDto(ticket.Id, ticket.UserId, ticket.Title, ticket.Description, ticket.Status, ticket.CreatedAt, ticket.ClosedAt);
    }
}
