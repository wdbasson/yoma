﻿using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Yoma.Core.Api.Middleware
{
    public class KeyCloakClaimsTransformer : IClaimsTransformation
    {
        private const string ClaimType_RealmAccess = "realm_access";

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if(principal == null)
                throw new ArgumentNullException(nameof(principal)); 

            var claimsIdentity = principal.Identity as ClaimsIdentity;

            if (claimsIdentity == null)
                throw new ArgumentNullException($"{nameof(principal)}.{principal.Identity}");    

            // flatten realm_access because Microsoft identity model doesn't support nested claims
            // by map it to Microsoft identity model, because automatic JWT bearer token mapping already processed here
            if (claimsIdentity.IsAuthenticated && claimsIdentity.HasClaim((claim) => claim.Type == ClaimType_RealmAccess))
            {
                var realmAccessClaim = claimsIdentity.FindFirst((claim) => claim.Type == ClaimType_RealmAccess);
                if (realmAccessClaim == null)
                    throw new InvalidOperationException($"Failed to obtain 'claimsIdentity' '{ClaimType_RealmAccess}'");

                var realmAccessAsDict = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(realmAccessClaim.Value);
                if (realmAccessAsDict == null)
                    throw new InvalidOperationException($"Failed to deserialize '{ClaimType_RealmAccess}' as {nameof(Dictionary<string, string[]>)}");

                if (realmAccessAsDict["roles"] != null)
                {
                    foreach (var role in realmAccessAsDict["roles"])
                    {
                        claimsIdentity.AddClaim(new Claim("role", role));
                    }
                }
            }

            return Task.FromResult(principal);
        }
    }
}
