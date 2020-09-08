using System;
using System.Collections.Generic;
using System.Text;

namespace msi_function.Models
{
    public class SecretRequest
    {
        public string Secret { get; set; }

        public SecretRequest(string _Secret)
        {
            Secret = _Secret;
        }

    }
}
