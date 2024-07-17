﻿using CandyStoreApi.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CandyStoreApi.Services
{
    public class IdentityService
    {
        private readonly JwtSettings? _jwtSettings;
        private readonly byte[] _key;

        public IdentityService(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
            ArgumentNullException.ThrowIfNull(_jwtSettings);
            ArgumentNullException.ThrowIfNull(_jwtSettings.SigningKey);
            ArgumentNullException.ThrowIfNull(_jwtSettings.Audiences);
            ArgumentNullException.ThrowIfNull(_jwtSettings.Audiences[0]);
            ArgumentNullException.ThrowIfNull(_jwtSettings.Issuer);
            _key = Encoding.ASCII.GetBytes(_jwtSettings?.SigningKey!);
        }

        private static JwtSecurityTokenHandler TokenHandler => new();
        
        public SecurityToken CreateSecurityToken(ClaimsIdentity identity)
        {
            var tokenDescriptor = GetTokenDescriptor(identity);

            return TokenHandler.CreateToken(tokenDescriptor);
        }

        public string WriteToken(SecurityToken token)
        {
            return TokenHandler.WriteToken(token);
        }

        private SecurityTokenDescriptor GetTokenDescriptor(ClaimsIdentity identity)
        {
            return new SecurityTokenDescriptor()
            {
                Subject = identity,
                Expires = DateTime.Now.ToUniversalTime().AddHours(2),
                Audience = _jwtSettings!.Audiences?[0]!,
                Issuer = _jwtSettings?.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256Signature)
            };
        }
    }
}
