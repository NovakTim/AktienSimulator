using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class ErrorCodes
    {
        public enum Register
        {
            NoError = 1000,
            NameAlreadyTaken = 2000
        }

        public enum Login
        {
            NoError = 1000,
            NicknameNotFound = 2000,
            WrongPassword = 3000
        }
    }
}
