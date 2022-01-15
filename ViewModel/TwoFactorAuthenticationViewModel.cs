using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomePhysio.ViewModel
{
    public class TwoFactorAuthenticationViewModel
    {
        //for login
        public string Code { get; set; }

        //for register
        public string Token { get; set; }
    }
}
