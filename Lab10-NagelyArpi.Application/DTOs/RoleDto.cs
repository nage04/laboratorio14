namespace Lab10_NagelyArpi.Application.DTOs;

public sealed class RoleDto
{
    public RoleDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
}
