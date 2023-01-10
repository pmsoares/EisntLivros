using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EisntLivros.Utility
{
    public class StripeSettings
    {
        public string SecretKey { get; set; } = null!;

        public string PublishableKey { get; set; } = null!;
    }
}
