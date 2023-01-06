using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizManager.Types.Account
{
    public class AuthenticationResponse
    {
        public bool IsAuthenticationSuccessful { get; set; }
        public string Token { get; set; }
    }
}
