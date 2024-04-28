using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Constants
{
    public class MessageResults
    {
        //public static string GetSuccessResult()
        //{
        //    return "s: تم جلب البيانات بنجاح";
        //}
        //public const string GetSuccessResult = "تم جلب البيانات بنجاح";
        //public const string AddSuccessResult = "تمت اضافة العنصر بنجاح";
        //public const string EditSuccessResult = "تم تحديث بيانات العنصر بنجاح";
        //public const string DeleteSuccessResult = "تم حذف العنصر بنجاح";
        //public const string ChangePasswordSuccessResult = "تم تغيير كلمة المرور بنجاح";
        //public const string ChangeActiveStatus = "تم تغيير الحالة بنجاح";
        //public const string CategoryStatus = "تم اضافة تصنيف جديد من قبل";
        //public const string PostStatus = "تم اضافة خبر جديد من قبل";
        //public const string ActivityStatus = "تم اضافة نشاط جديد من قبل";
        //public const string ProgrammeStatus = "تم اضافة برنامج جديد من قبل";
        //public const string ProjectStatus = "تم اضافة مشروع جديد من قبل";
        //public const string UserStatus = "تم اضافة مستخدم جديد من قبل";
        //public const string DonaterStatus = "تم اضافة متبرع جديد من قبل";
        //public const string SponsorStatus = "تم اضافة شريك جديد من قبل";


        public const string AddItemToCart = "تم الاضافة إلى السلة بنجاح";
        public const string DeleteItemFromCart = "تم الحذف من السلة بنجاح";


        public static string GetSuccessResult()
        {
            return "s: تم جلب البيانات بنجاح";
		}
		public static string LogoutSuccessResult()
		{
			return "s: تم تسجيل الخروج بنجاح";
		}
		public static string LoginSuccessResult()
		{
			return "s: تم تسجيل الدخول بنجاح";
		}
        public static string RegisterSuccessResult()
        {
            return "s: تم  انشاء حساب بنجاح ";
        }
        public static string ErrorResult()
		{
			return "d: يوجد خطأ بالبيانات  المدخلة";
        }
        public static string ChangePasswordResult()
        {
            return "s:  تم تغيير كلمة المرور بنجاح";
        }
        public static string SendPasswordResult()
        {
            return "s:  تم ارسال كلمة المرور  بنجاح";
        }
        public static string ErrorDateNow()
        {
            return "d: تاريخ  يجب أن يكون خلال اليوم او لاحقا بعد التوقيت الحالي";
    }
    public static string InvalidUserNameOrEmailResult()
		{
			return "d: البريد الالكتروني  أو اسم المستخدم غير صحيح";
		}
        public static string InvalidExtentionImageResult()
        {
            return "d: امتداد الملف أو الصورة غير مسموح";
        }
        public static string InvalidAllowedImageSizeResult()
        {
            return "d: حجم الملف أو الصورة أكبر من المسموح بها";
        }
        public static string UserIsLoginResult()
		{
			return "d: عذرا يجب الدخول من حساب واحد فقط يرجى اغلاق الحساب الاخر ثم الدخول مرة أخرى";
        }
        public static string AddSuccessResultString()
        {
            return "s: تمت اضافة العنصر بنجاح";
        }  
        
        public static string EditSuccessResultString()
        {
            return "s: تمت اضافة العنصر بنجاح";
        }
        public static object AddSuccessResult()
        {
            return new { status = 1, msg = "s: تمت اضافة العنصر بنجاح", close = 1 };
        }
        public static object UpdateStatusResult()
        {
            return new { status = 1, msg = "s: تمت تحديث الحالة بنجاح", close = 1 };
        }
        public static object EditSuccessResult()
        {
            return new { status = 1, msg = "s: تم تحديث بيانات العنصر بنجاح ", close = 1 };
        }

        public static object DeleteSuccessResult()
        {
            return new { status = 1, msg = "s: تم حذف العنصر بنجاح", close = 1 };
        }
        public static object ChangeActiveSuccessResult()
        {
            return new { status = 1, msg = "s: تم تغيير التفعيل بنجاح", close = 1 };
        }

    }
}
