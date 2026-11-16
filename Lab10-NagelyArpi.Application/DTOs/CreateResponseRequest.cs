namespace Lab10_NagelyArpi.Application.DTOs;

public sealed class CreateResponseRequest
{
    public Guid TicketId { get; set; }
    public Guid ResponderId { get; set; }
    public string Message { get; set; } = string.Empty;
}
