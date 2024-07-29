

using Papara.Data.Domain;

namespace Papara.Bussiness.Token;

public interface ITokenService
{
    Task<string> GetToken(User user);
}