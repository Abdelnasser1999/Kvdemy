namespace Kvdemy.Core.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException() : base("البريد الالكتروني مستخدم بالفعل")
        {

        }
    }
}
