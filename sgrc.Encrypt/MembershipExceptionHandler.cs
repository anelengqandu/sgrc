using System;
using System.Web.Security;

namespace sgrc.Encrypt
{
    public static class MembershipExceptionHandler
    {
        public static String Check(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "dwqed";
            }
            return null;
        }
    }
}
