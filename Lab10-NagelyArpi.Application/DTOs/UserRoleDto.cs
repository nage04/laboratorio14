namespace Lab10_NagelyArpi.Application.DTOs;

public sealed class UserRoleDto
{
    public UserRoleDto(Guid userId, Guid roleId, DateTime assignedAt)
    {
        UserId = userId;
        RoleId = roleId;
        AssignedAt = assignedAt;
    }

    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public DateTime AssignedAt { get; set; }
}
