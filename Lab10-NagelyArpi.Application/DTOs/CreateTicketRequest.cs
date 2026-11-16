namespace Lab10_NagelyArpi.Application.DTOs;

public sealed class CreateTicketRequest
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}
