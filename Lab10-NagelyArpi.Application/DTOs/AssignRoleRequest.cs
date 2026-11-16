namespace Lab10_NagelyArpi.Application.DTOs;

public sealed class AssignRoleRequest
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
