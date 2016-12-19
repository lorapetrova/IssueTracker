using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace IssueTracker.Extensions
{
    public static class PrincipalExtensions
    {
        public static bool CanAdministrateIssue(this IPrincipal principal, string username)
        {
            return principal.IsInRole("Admin") || principal.Identity.Name == username;
        }
    }
}