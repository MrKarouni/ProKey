using System;
using System.Collections.Generic;
using System.Linq;
using ProKey.DomainClasses;
using ProKey.ServiceLayer.Contracts;
using ProKey.DataLayer.Context;
using System.Data.Entity;

namespace ProKey.ServiceLayer
{
    public class TokenStoreService : ITokenStoreService
    {
        //TODO: replace it with `public IDbSet<UserToken> Tokens {set;get;}`
        //private static readonly IList<UserToken> _tokens = new List<UserToken>();

        IUnitOfWork _uow;
        readonly IDbSet<UserToken> _tokens;
        private readonly ISecurityService _securityService;
        public TokenStoreService(IUnitOfWork uow, ISecurityService securityService)
        {
            _uow = uow;
            _tokens = _uow.Set<UserToken>();
            _securityService = securityService;
        }

        public void CreateUserToken(UserToken userToken)
        {
            InvalidateUserTokens(userToken.OwnerUserId);
            _tokens.Add(userToken);
            _uow.SaveAllChanges();
        }

        public void UpdateUserToken(int userId, string accessTokenHash)
        {
            var token = _tokens.FirstOrDefault(x => x.OwnerUserId == userId);
            if (token != null)
            {
                token.AccessTokenHash = accessTokenHash;
            }
            _uow.SaveAllChanges();
        }

        public void DeleteExpiredTokens()
        {
            var now = DateTime.UtcNow;
            var userTokens = _tokens.Where(x => x.RefreshTokenExpiresUtc < now).ToList();
            foreach (var userToken in userTokens)
            {
                _tokens.Remove(userToken);
            }
            _uow.SaveAllChanges();
        }

        public void DeleteToken(string refreshTokenIdHash)
        {
            var token = FindToken(refreshTokenIdHash);
            if (token != null)
            {
                _tokens.Remove(token);
            }
            _uow.SaveAllChanges();
        }

        public UserToken FindToken(string refreshTokenIdHash)
        {
            return _tokens.FirstOrDefault(x => x.RefreshTokenIdHash == refreshTokenIdHash);
        }

        public void InvalidateUserTokens(int userId)
        {
            var userTokens = _tokens.Where(x => x.OwnerUserId == userId).ToList();
            foreach (var userToken in userTokens)
            {
                _tokens.Remove(userToken);
            }
            _uow.SaveAllChanges();
        }

        public bool IsValidToken(string accessToken, int userId)
        {
            var accessTokenHash = _securityService.GetSha256Hash(accessToken);
            var userToken = _tokens.FirstOrDefault(x => x.AccessTokenHash == accessTokenHash && x.OwnerUserId == userId);
            return userToken?.AccessTokenExpirationDateTime >= DateTime.UtcNow;
        }
    }
}