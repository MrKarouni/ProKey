using System;
using System.Collections.Generic;
using System.Linq;
using ProKey.DomainClasses;
using ProKey.ServiceLayer.Contracts;
using ProKey.DataLayer.Context;
using System.Data.Entity;
using ProKey.DomainClasses.Enums;

namespace ProKey.ServiceLayer
{
    public class UsersService : IUsersService
    {
        //TODO: replace it with `public IDbSet<User> Users {set;get;}`
        //private static readonly IList<User> _users = new List<User>
        //{
        //    // initial `seed`, just for the demo
        //    new User
        //    {
        //     Id = 1,
        //     UserName = "Vahid",
        //     DisplayName = "وحيد",
        //     IsActive = true,
        //     LastLoggedIn = null,
        //     Password = new SecurityService().GetSha256Hash("1234"),
        //     Roles = new List<UserRole>() { },
        //     TokenSerialNumber = Guid.NewGuid().ToString("N")
        //    }
        //};

        IUnitOfWork _uow;
        readonly IDbSet<User> _users;
        readonly IDbSet<UserRole> _userRoles;
        readonly IDbSet<Role> _roles;
        private readonly ISecurityService _securityService;
        public UsersService(ISecurityService securityService, IUnitOfWork uow)
        {
            _uow = uow;
            _users = _uow.Set<User>();
            _roles = _uow.Set<Role>();
            _userRoles = _uow.Set<UserRole>();
            _securityService = securityService;
        }

        public void SeedDataBase()
        {
            var existRoles = _roles.ToList();
            if(!existRoles.Any())
            {
                _roles.Add(new Role() { Name = RoleName.Admin.ToString(), Description = "Administrator" });
                _uow.SaveAllChanges();
            }

            var adminRole = _roles.FirstOrDefault(x => x.Name == RoleName.Admin.ToString());
            if(!_users.Any())
            {
                _users.Add(new User()
                {
                    UserName = "Admin",
                    Password = _securityService.GetSha256Hash("QazWsx!23"),
                    DisplayName = "Administrator",
                    IsActive = true,
                    LastLoggedIn = null,
                    UserTypeIndex = 1,//TODO
                    TokenSerialNumber = Guid.NewGuid().ToString("N"),
                    Roles = new List<UserRole>() { new UserRole() { RoleId = adminRole.Id } },
                });
                _uow.SaveAllChanges();
            }
        }

        public User FindUser(int userId)
        {
            return _users.FirstOrDefault(x => x.Id == userId);
        }

        public User FindUser(string username, string password)
        {
            var passwordHash = _securityService.GetSha256Hash(password);
            return _users.FirstOrDefault(x => x.UserName == username && x.Password == passwordHash);
        }

        public string GetSerialNumber(int userId)
        {
            var user = FindUser(userId);
            return user.TokenSerialNumber;
        }

        public IEnumerable<string> GetUserRoles(int userId)
        {
            var userRoles = _userRoles.Include(x => x.Role).Where(x => x.UserId == userId).ToList();
            if(userRoles.Any())
            {
                return userRoles.Select(x => x.Role.Name).ToList();
            }
            return new List<string>() { };
        }

        public void UpdateUserLastActivityDate(int userId)
        {
            var user = FindUser(userId);
            user.LastLoggedIn = DateTime.UtcNow;
            _uow.SaveAllChanges();
        }
    }
}