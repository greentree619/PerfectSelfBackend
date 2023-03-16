using System.Collections;

namespace PerfectSelf.WebAPI.Common
{
    public class Global
    {
        public static Hashtable onlineAllUsers = new Hashtable();
        public static String GenToken()
        {
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            return token;
        }
    }
}
