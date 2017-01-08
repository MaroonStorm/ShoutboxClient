using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenData
{
    class ForumData
    {
        private string xfToken = "";

        public void SetToken(string t)
        {
            xfToken = t;
        }

        public string GetToken()
        {
            return xfToken;
        }
    }
}
