namespace Lab10_NagelyArpi.Application.DTOs;

public sealed class ResponseDto
{
    public ResponseDto(Guid id, Guid ticketId, Guid responderId, string message, DateTime createdAt)
    {
        Id = id;
        TicketId = ticketId;
        ResponderId = responderId;
        Message = message;
        CreatedAt = createdAt;
    }

    public Guid Id { get; set; }
    public Guid TicketId { get; set; }
    public Guid ResponderId { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
}
