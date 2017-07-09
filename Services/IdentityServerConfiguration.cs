using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerWithAspNetIdentity.Services
{
    public class IdentityServerConfiguration
    {
        public string IdentityServerUri { get; set; }
        public string ApiUri { get; set; }
        public string ImplicitJsClientUri { get; set; }
        public string ResourceOwnerJsClientUri { get; set; }
        public string HybridMvcClientUri { get; set; }
        public string ImplicitReactClientUri { get; set; }

    }
}
