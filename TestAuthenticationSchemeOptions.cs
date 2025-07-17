using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Security.Claims;

namespace PIM_API.Tests
{
    public class TestAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public IEnumerable<Claim> Claims { get; set; }
    }
}
