using System.Collections.Generic;
using ProKey.DomainClasses;

namespace ProKey.ServiceLayer.Contracts
{
    public interface IUsersService
    {
        void SeedDataBase();
        string GetSerialNumber(int userId);
        IEnumerable<string> GetUserRoles(int userId);
        User FindUser(string username, string password);
        User FindUser(int userId);
        void UpdateUserLastActivityDate(int userId);
    }
}