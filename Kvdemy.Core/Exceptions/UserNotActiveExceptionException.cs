namespace Kvdemy.Core.Exceptions
{
    public class UserNotActiveExceptionException : Exception
    {
        public UserNotActiveExceptionException() : base("المستخدم غير فعال يرجى مراجعة الدعم")
        {

        }
    }
}
