using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGD
{

    internal static class UserInfo
    {
        static string userName;
        public static string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }

}
