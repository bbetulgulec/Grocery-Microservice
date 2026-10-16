// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace manav.identityServer;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources => new[]
    {
        new ApiResource("ResourceCatalog")
        {
            Scopes =
            {
                "CatalogFullPermission", "CatalogReadPermission", "ProductFullPermission", "ProductReadPermission"
            }
        },
        new ApiResource("ResourceDiscount")
        {
            Scopes = { "DiscountFullPermission", "DiscountReadPermission" }
        },
        new ApiResource("ResourceOrder")
        {
            Scopes = { "OrderFullPermission" }
        },
        new ApiResource("ResourceCargo")
        {
            Scopes = { "CargoFullPermission" }
        },
        new ApiResource("ResourceBasket")
        {
            Scopes = { "BasketFullPermission" }
        },
        new ApiResource("ResourceComment")
        {
            Scopes = { "CommentFullPermission", "CommentReadPermission" }
        },
        new ApiResource("ResourcePayment")
        {
            Scopes = { "PaymentFullPermission" }
        },
        new ApiResource("ResourceImages")
        {
            Scopes = { "ImagesFullPermission", "ImagesReadPermission" }
        },
        new ApiResource("ResourceMessage")
        {
            Scopes = { "MessageFullPermission", "MessageReadPermission" }
        },
        new ApiResource("ResourceOcelot")
        {
            Scopes = { "OcelotFullPermission" }
        },
        new ApiResource("ResourceIdentity")
        {
            Scopes = { "IdentityFullPermission" }
        },
        new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
    };

    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Email(),
        new IdentityResources.Profile()
    };

    public static IEnumerable<ApiScope> ApiScopes => new[]
    {
        new ApiScope("CatalogFullPermission", "Full access to catalog full permission"),
        new ApiScope("CatalogReadPermission", "Full access to catalog read permission"),
        new ApiScope("DiscountFullPermission", "Full access to discount full permission"),
        new ApiScope("DiscountReadPermission", "Full access to discount read permission"),
        new ApiScope("OrderFullPermission", "Full access to order full permission"),
        new ApiScope("CargoFullPermission", "Full access to cargo full permission"),
        new ApiScope("BasketFullPermission", "Full access to basket full permission"),
        new ApiScope("CommentFullPermission", "Full access to comment full permission"),
        new ApiScope("CommentReadPermission", "Full access to product read permission"),
        new ApiScope("PaymentFullPermission", "Full access to payment full permission"),
        new ApiScope("ImagesFullPermission", "Full access to images full permission"),
        new ApiScope("ImagesReadPermission", "Full access to images read permission"),
        new ApiScope("OcelotFullPermission", "Full access to images full permission"),
        new ApiScope("MessageFullPermission", "Full access to message full permission"),
        new ApiScope("MessageReadPermission", "Full access to message full permission"),
        new ApiScope("ProductFullPermission", "Full access to product full permission"),
        new ApiScope("ProductReadPermission", "Full access to order read permission"),
        new ApiScope("IdentityFullPermission", "Full access to identity full permission"),
        new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
    };

    public static IEnumerable<Client> Clients => new[]
    {
        //Visitor
        new Client
        {
            ClientId = "manavVisitorID",
            ClientName = "Manav Visitor User",
            AllowedGrantTypes =
                GrantTypes.ClientCredentials, //neye izin verdiğini söylüyor(Burada kimlik işlmlerine izin veriyor)
            ClientSecrets = { new Secret("manavsecret".Sha256()) },
            AllowedScopes =
            {
                "CatalogReadPermission", "OcelotFullPermission", "CommentReadPermission", "ProductReadPermission",
                "BasketFullPermission", "OrderFullPermission", "ImagesReadPermission"
            }
        },

        //Manager
        new Client
        {
            ClientId = "manavManagerID",
            ClientName = "Manav Manager User",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, //giriş yapanlara veriyor
            ClientSecrets = { new Secret("manavsecret".Sha256()) },
            AllowedScopes =
            {
                "OcelotFullPermission", "BasketFullPermission", "OrderFullPermission", "CatalogReadPermission",
                "ProductReadPermission", "DiscountReadPermission", "CommentReadPermission", "ImagesReadPermission",
                "PaymentFullPermission", "MessageReadPermission",

                IdentityServerConstants.LocalApi.ScopeName,
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
            }
        },
        //Admin
        new Client
        {
            ClientId = "manavAdminID",
            ClientName = "Manav Admin User",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            ClientSecrets = { new Secret("manavsecret".Sha256()) },
            AllowedScopes =
            {
                "CatalogFullPermission", "DiscountFullPermission", "OrderFullPermission", "CargoFullPermission",
                "BasketFullPermission", "OcelotFullPermission", "IdentityFullPermission", "ProductFullPermission",
                "CommentFullPermission", "PaymentFullPermission", "ImagesFullPermission", "MessageFullPermission",
                IdentityServerConstants.LocalApi.ScopeName,
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
            },
            AccessTokenLifetime = 6000
        }
    };
}