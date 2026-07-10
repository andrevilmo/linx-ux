using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Linx.Security.Core.Authorization
{

    public enum AuthorizationType
    {
        Query = 0,
        Update = 1,
        Insert = 2,
        Delete = 3
    }

    public class ApiAuthorizationRequirement : IAuthorizationRequirement
    {
        public string[] Controllers { get; private set; }

        public ApiAuthorizationRequirement(string[] controllers)
        {
            Controllers = controllers;
        }
    }

    public class DSAuthorizationRequirement : IAuthorizationRequirement
    {
        public string[] Controllers { get; private set; }
        public AuthorizationType AuthorizationType { get; set; }

        public DSAuthorizationRequirement(AuthorizationType authorizationType, string[] controllers)
        {
            Controllers = controllers;
            AuthorizationType = authorizationType;
        }
    }

    public class ApiAuthorizationHandler : AuthorizationHandler<ApiAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiAuthorizationRequirement requirement)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class DSAuthorizationHandler : AuthorizationHandler<DSAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DSAuthorizationRequirement requirement)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }

}
