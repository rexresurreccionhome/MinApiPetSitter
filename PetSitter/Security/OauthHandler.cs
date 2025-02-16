namespace PetSitter.Security;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;



public static class OauthHandler
{
    public const string baseUrl = "/security/oauth/token";

    public static Results<Ok<Dictionary<string, string>>, UnauthorizedHttpResult> GenerateToken(JwtClientIdAndSecret jwtClientIdAndSecret, JwtTokenProvider jwtTokenProvider)
    {
        string? token = null;
        if (jwtClientIdAndSecret.ClientId == "member" && jwtClientIdAndSecret.CLientSecret == "1234")
        {
            token = jwtTokenProvider.GenerateToken(jwtClientIdAndSecret.ClientId, AuthorizationByClaims.Member.Claims());
        }
        else if (jwtClientIdAndSecret.ClientId == "sitter" && jwtClientIdAndSecret.CLientSecret == "1234")
        {
            token = jwtTokenProvider.GenerateToken(jwtClientIdAndSecret.ClientId, AuthorizationByClaims.Staff.Claims());
        }
        if (token is not null)
        {
            return TypedResults.Ok(
                new Dictionary<string, string>(){
                    {"access_token", token},
                }
            );
        }
        return TypedResults.Unauthorized();
    }
}