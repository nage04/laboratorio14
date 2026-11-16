namespace Lab10_NagelyArpi.Application.Ports.Services;

public interface IPasswordVerifier
{
    bool Verify(string password, string passwordHash);
}
