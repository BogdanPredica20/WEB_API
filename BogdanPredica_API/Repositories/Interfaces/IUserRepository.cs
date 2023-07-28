using BogdanPredica_API.Models.Authentication;

namespace BogdanPredica_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<AuthenticationResponse> Authenticate(AuthenticateRequest request);
    }
}
