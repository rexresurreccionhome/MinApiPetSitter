namespace PetSitter.Security;

using Microsoft.AspNetCore.Authorization;

public static class AuthorizationByClaims
{
    public static class Member
    {
        public const string authName = "MemberAuthorization";
        public const string roleName = "Member";

        public static void Configure(AuthorizationPolicyBuilder policy)
        {
            policy.RequireClaim("RoleName", roleName);
        }

        public static Dictionary<string, object> Claims()
        {
            return new Dictionary<string, object> {
                {"RoleName", roleName}
            };
        }
    }

    public static class Staff
    {
        public const string authName = "StaffAuthorization";
        public const string roleName = "Staff";

        public static void Configure(AuthorizationPolicyBuilder policy)
        {
            policy.RequireClaim("RoleName", roleName);
        }

        public static Dictionary<string, object> Claims()
        {
            return new Dictionary<string, object> {
                {"RoleName", roleName}
            };
        }
    }
}