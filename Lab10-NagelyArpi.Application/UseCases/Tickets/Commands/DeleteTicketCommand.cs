using Lab10_NagelyArpi.Domain.Repositories;
using MediatR;

namespace Lab10_NagelyArpi.Application.UseCases.Tickets.Commands;

public sealed record DeleteTicketCommand(Guid TicketId) : IRequest<bool>;

internal sealed class DeleteTicketCommandHandler(ITicketRepository tickets, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteTicketCommand, bool>
{
    public async Task<bool> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        var deleted = await tickets.DeleteAsync(request.TicketId, cancellationToken);
        if (deleted) await unitOfWork.SaveChangesAsync(cancellationToken);
        return deleted;
    }
}
