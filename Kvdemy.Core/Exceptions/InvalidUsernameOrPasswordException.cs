namespace Kvdemy.Core.Exceptions
{
    public class InvalidUsernameOrPasswordException : Exception
    {
        public InvalidUsernameOrPasswordException() : base("الايميل او رقم الهاتف مستخدم بالفعل")
        {

        }
    }
}
