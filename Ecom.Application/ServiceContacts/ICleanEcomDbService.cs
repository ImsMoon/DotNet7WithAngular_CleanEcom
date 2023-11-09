
namespace Ecom.Application.ServiceContacts
{
    public interface ICleanEcomDbService
    {
        Task<int> ApplyMigrationsAsync();
    }
}