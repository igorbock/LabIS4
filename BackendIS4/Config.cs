namespace BackendIS4;

public static class Config
{
    public static IEnumerable<ApiScope> ApiScopes
    {
        get
        {
            return new List<ApiScope>
            {
                new("API-LAB", "API Laboratório")
                {
                    UserClaims = new[] { "username" }
                }
            };
        }
    }

    public static IEnumerable<Client> Clients
    {
        get
        {
            return new List<Client>
            {
                new() {
                    ClientId = "ClientLab",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("lab_segredo".Sha256())
                    },

                    AllowedScopes = { "API-LAB" }
                },
                new() {
                    ClientId = "ClientLab2",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AccessTokenType = AccessTokenType.Jwt,
                    //AccessTokenLifetime = 120, //86400,
                    //IdentityTokenLifetime = 120, //86400,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    SlidingRefreshTokenLifetime = 30,
                    AllowOfflineAccess = true,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AlwaysSendClientClaims = true,
                    ClientSecrets=  new List<Secret> { new("lab_segredo".Sha256()) },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "API-LAB"
                    }
                }
            };
        }
    }

    public static IEnumerable<ApiResource> ApiResources
    {
        get
        {
            return new List<ApiResource>
            {
                new("API-Resource-LAB", "API Resource Laboratório")
            };
        }
    }

    public static IEnumerable<IdentityResource> IdentityResources
    {
        get
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
    }

    public static List<TestUser> TestUsers
    {
        get
        {
            return new List<TestUser>
            {
                new() {
                    SubjectId = new Guid().ToString(),
                    Username = "Igor",
                    Password = "Igor123@",
                    IsActive = true
                },
                new() {
                    SubjectId = new Guid().ToString(),
                    Username = "Rafaela",
                    Password = "Rafa123@",
                    IsActive = true
                }
            };
        }
    }

    public static List<IdentityRole> IdentityRoles
    {
        get
        {
            return new List<IdentityRole>
            {
                new() {
                    Id = "fd37d87b-35f9-46f1-b008-0111fa22ea58",
                    Name = "ADM",   //Administrador
                    NormalizedName = "ADM",
                    ConcurrencyStamp = new Guid().ToString()
                },
                new() {
                    Id = "d73fba0d-f6cd-427b-8ba8-cfcd9c1e9abc",
                    Name = "CLB",   //Colaborador
                    NormalizedName = "CLB",
                    ConcurrencyStamp = new Guid().ToString()
                }
            };
        }
    }
}
