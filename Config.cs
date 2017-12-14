// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServerWithAspNetIdentity
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(IConfigurationRoot Configuration)
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },

                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1", IdentityServerConstants.StandardScopes.OfflineAccess },
                    AllowOfflineAccess = true,
                    AllowedCorsOrigins = { Configuration["IdentityServerConfig:ResourceOwnerJsClientUri"] }
                },

                // OpenID Connect hybrid flow and client credentials client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes =  GrantTypes.CodeAndClientCredentials,

                    RequireConsent = false,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    //RedirectUris = { $"{Configuration["IdentityServerConfig:HybridMvcClientUri"]}/signin-oidc"},
                    RedirectUris = { "http://localhost/IdentityServer" },
                    PostLogoutRedirectUris = { "http://identityserverexample.azurewebsites.net" },
                    
                    AllowedScopes =

                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowOfflineAccess = true,
                    AllowedCorsOrigins = { "http://identityserverexample.azurewebsites.net", "http://localhost" }
                },

                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RequireConsent = false,

                    RedirectUris = { $"{Configuration["IdentityServerConfig:ImplicitJsClientUri"]}/callback" },
                    PostLogoutRedirectUris = { $"{Configuration["IdentityServerConfig:ImplicitJsClientUri"]}" },
                    AllowedCorsOrigins = { Configuration["IdentityServerConfig:ImplicitJsClientUri"] },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1",
                        "role"
                    },
                },

                // React Client
                new Client
                {
                    ClientId = "react",
                    ClientName = "React Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{Configuration["IdentityServerConfig:ImplicitReactClientUri"]}/callback.html" },
                    PostLogoutRedirectUris = { Configuration["IdentityServerConfig:ImplicitReactClientUri"] },
                    AllowedCorsOrigins = { Configuration["IdentityServerConfig:ImplicitReactClientUri"] },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                },

                // React Native Client
                new Client
                {
                    ClientId = "native.code",
                    ClientName = "Native Client (Code with PKCE)",

                    RedirectUris = { "https://notused" },
                    PostLogoutRedirectUris = { "https://notused" },

                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AllowedScopes = { "openid", "profile", "email", "api1" },
                    AllowOfflineAccess = true
                }
            };
        }

    }
}