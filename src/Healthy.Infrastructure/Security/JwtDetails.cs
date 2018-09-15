using System.Collections.Generic;
using System.Security.Claims;

namespace Healthy.Infrastructure.Security
{
    public class JwtDetails
    {
        public string Subject { get; set; }
        public string State { get; set; }
        public string Role { get; set; }
        public long Expires { get; set; }
        public IEnumerable<Claim> Claims { get; set; }        
    }
}