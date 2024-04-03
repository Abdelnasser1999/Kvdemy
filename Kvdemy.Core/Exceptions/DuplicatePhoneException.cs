namespace Kvdemy.Core.Exceptions
{
    public class DuplicatePhoneException : Exception
    {
        public DuplicatePhoneException() : base("رقم الهاتف مستخدم بالفعل")
        {

        }
    }
}
