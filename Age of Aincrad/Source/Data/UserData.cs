using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenData
{
    class UserData
    {
        private string clientUsername = "";
        private string clientPassword = "";

        private string messageColour = "";

        private bool isLoggedIn = false;

        public void SetLoginState(bool l)
        {
            isLoggedIn = l;
        }

        public bool IsLoggedIn()
        {
            return isLoggedIn;
        }

        public void SetUsername(string u)
        {
            clientUsername = u;
        }

        public void SetPassword(string p)
        {
            clientPassword = p;
        }

        public void SetColour(string c)
        {
            messageColour = c;
        }

        public string GetUsername()
        {
            return clientUsername;
        }

        public string GetPassword()
        {
            return clientPassword;
        }
    }
}
