using Microsoft.AspNetCore.Http;

namespace Kvdemy.Core.Constants
{
    public static class Constant
    {

        public const string Email = "Email";
        public const string Image = "Image";
        public const string ImageUrl = "ImageUrl";
        public const int NumberOne = 1;
        public const int NumberZero = 0;
        public const int NumberTwilve = 12;
        public const int NumberTwinty = 20;
        public const int NumberFourHundred = 400;
        public const float twintyfivePrecentage = (float)0.25;

        public const string Palestine = "Palestine";
        public const string English = "En";


        public static class RolesFilter
        {
            public const string SuperAdmin = "SuperAdmin";
            public const string Admin = "Admin";
            public const string SuperAdminAndAdmin = "SuperAdmin,Admin";
            public const string UserAndAdvisor = "User,Adviser";
            public const string User = "User";
            public const string Adviser = "Adviser";


        }
        public static class Area
        {
            public const string ControlPanel = "ControlPanel";
            public const string ProfileAccount = "ProfileAccount";

        }
        public static class FilterFileExtentions
        {
            public const string png = ".png";
            public const string jpg = ".jpg";
            public const string svg = ".svg";
            public const string jpeg = ".jpeg";
            public const string pdf = ".pdf";
            public const string docx = ".docx";
            // 1mb = 1048576
            public const int _maxAllowedPosterSize = 1048576;

        }
        public static class ViewBag
        {
            public const string id = "Id";
            public const string name = "Name";

            //Specializations
            public const string titleSpecializations = "Specializations";
            public const string idSpecializations = "Id";
            public const string nameSpecializations = "Name";
            public const string UserDetailes = "userDetailes";

            //Adviser
            public const string titleAdviserImage = "AdviserImage";
            public const string titleAdviserskills = "Adviserskills";
            public const string totalTransaction = "TotalTransaction";

            //user
            public const string titleUsers = "Users";
            public const string titleUserImage = "UserImage";
            public const string titleImageMain = "ImageMain";
            public const string titleIcon = "Icon";

            //Degree
            public const string titleDegree = "Degree";
            public const string idDegree = "Id";
            public const string nameDegree = "Name";
            public const string Success = "success";
            //AdvisorAppoiment
            

        }
        public static class DefaultImage
		{
			public const string ImagefileName = "default.jpeg";
			public const string ImagefileName2 = "shawernyicon.jpeg";
            public const string path = "./wwwroot/Metronic/" + ImagefileName;
            public const string path2 = "./wwwroot/assets/" + ImagefileName2;
            public const string FolderFileMessage = "FileMessage";
            public const string Images = "Images";

            public static IFormFile File = null;
        }
        public static class EmailTemplate
        {
            public const string userLink = "[userLink]";
            public const string userCode = "[userCode]";
            public const string Home = "[Home]";
            public const string TokenLink = "[TokenLink]";
            public const string userFullName = "[userFullName]";
            public const string CreatedAt = "[createdAt]";
            public const string BillNumber = "[billNumber]";
            public const string ApplicationUserFirstName = "[applicationUserFirstName]";
            public const string StartSession = "[startSession]";
            public const string EndSession = "[endSession]";

            public const string DayLeft = "[dayLeft]";
            public const string AdviserLastName = "[adviserLastName]";
            public const string ApplicationUserEmail = "[applicationUserEmail]";
            public const string AdviserSpecializationName = "[adviserSpecializationName]";
            public const string ApplicationUserLastName = "[applicationUserLastName]";

        }
        public static class Response
        {
            public const string Email = "Email";
            public const string Roles = "Roles";
            public const string UserName = "UserName";
            public const string RolesSelect = "الرجاء اختار صلاحية واحدة على الأقل";
            public const string EmailIsExist = "البريد الالكتروني موجود بالفعل.";
            public const string UserIsExist = "المستخدم موجود بالفعل.";
            public const string UserNameIsExist = "اسم المستخدم موجود بالفعل.";
            public const string MaxLimit1MB = "الحد الأقصى للملف هو 1MB ?.";
            public const string FileDenied = " .pdf and .docx فقط مسموح !?.";
            public const string ImageDenied = " .jpg , .svg and .png فقط مسموح !?.";
            public const string Success = "لقد تم الاضافة بنجاح.";
            public const string Error = "يوجد حطأ بالبيانات.";
            public const string ErrorExistNotActiveWithDrawRequest = "لقد قمت بطلب سحب المبلغ من قبل وجاري التدقيق والسحب من طرف مدراء المنصة ...";
            public const string SuccessWithDrawRequest = " لقد قمت بطلب سحب المبلغ   وجاري التدقيق والسحب من طرف مدراء المنصة وسيتم التواصل والرد عبر البريد الالكتروني خلال 3 أيام  ...";

            public const string ErrorDateNow = "تاريخ الجلسة يجب أن يكون خلال اليوم او لاحقا بعد التوقيت الحالي";
            public const string ErrorDate = "تاريخ بداية الجلسة يجب ان تكون أقل من نهاية الجلسة.";
            public const string ErrorDenied = "يوجد جلسة محجوزة بالتاريخ والوقت المدخل.";
            public const string AlertSucsess = "نشكرك على تواصلك معنا سنرد قريبا على استفساراتك.";
            public const string LoginSucsess = " تم تسجيل الدخول الى الحساب.";
            public const string LogoutSucsess = " تم تسجيل الخروج من الحساب.";
            public const string EditSucsess = " تم التعديل على بيانات المستخدم ";
            public const string Payment = " تم الحجز بنجاح يرجى تاكيد الحجز عبر الدفع  ";
            public const string BalnceEquelZero = " عذراً رصيدك أقل من أو يساوي 0   ";

        }
        public static class Links
        {
            public const string ProgileHomeIndex = "/ProfileAccount/AccountProfileHome/Index";

            public const string ConfirmEmailWithUserIdParameter = "/ProfileAccount/AccountProfileUsers/ConfirmEmail?userId=";
            public const string Login = "~/Identity/Account/Login";
            public const string Logout = "~/Identity/Account/Logout";
            public const string ProfileUsersEditUser = "~/ProfileAccount/AccountProfileUsers/EditUser/";
            public const string HomeClient = "/Home/Index";
            public const string ProfileUsersCallBack = "/ProfileAccount/AccountProfileUsers/CallBack";
            public const string LogoutEndpoint = "/ProfileAccount/AccountProfileUsers/Logout";
            public const string ProfileUsersEditUser2 = "/ProfileAccount/AccountProfileUsers/EditUser/";


        }
        public static class Actions
        {
            public const string Index = "Index";
            public const string Edit = "Edit"; 
            public const string Create = "Create";
 
        }
        public static class Functions
        {
            public static DateTime DateTimeSudiaArabia()
            {
                TimeZoneInfo saudiArabiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Arabian Standard Time");
                // Convert current time to Saudi Arabia time
                DateTime saudiArabiaTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, saudiArabiaTimeZone);
                return saudiArabiaTime;
                // return DateTime.Now;
            }
            public static DateTime DateTimeAdd3Houres()
            {

                //return DateTime.Now.AddHours(3);
                return DateTime.Now;

            }
            public static DateTime DateTimeDotDateAdd3Houres()
            {

               // return DateTime.Now.Date.AddHours(3);
                return DateTime.Now.Date;

            }
        }
    }
}
