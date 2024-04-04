using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krooti.Core.Constant
{
    public static class MessagesKey
    {

        //  USER Messages
        public const string UserDeletedSuccss = "user_delete_success";
        public const string UserDeletedFailed = "user_delete_failed";
        public const string UserUpdatedSuccss = "user_updated_success";
        public const string UserUpdatedFailed = "user_updated_failed";
        public const string UserNotFound = "user_not_found";
        public const string LoginSuccess = "login_success";
        public const string InvalidCredentials = "invalid_credentials";
        public const string InfoEmailSend = "admin_info_email_send";
        public const string DuplicateEmail = "duplicate_email";
        public const string DuplicateEmirate = "duplicate_emirate";
        public const string DuplicatePhone = "duplicate_phone";
        public const string DuplicateUsername = "duplicate_username";
        public const string InvalidUsername = "invalid_username"; // must contain lowercase and min char is 5
        public const string InvalidToken = "invalid_token"; 

        // OTP Messages 
        public const string OtpSuccess = "otp_success";
        public const string OtpWrong = "otp_wrong";

        //  Item Messages
        public const string DataSuccess = "data_success";
        public const string DataFailed = "data_failed";
        public const string ItemNotFound  = "item_not_found";
        public const string QuestionNotFound  = "question_not_found";
        public const string ItemDeletedSuccss  = "delete_success";
        public const string ItemDeletedFailed  = "delete_failed";
        public const string ItemUpdatedSuccss  = "updated_success";
        public const string ItemCreatedSuccss  = "created_success";
        public const string ItemCreatedFailed = "created_failed";
        public const string ItemUpdatedFailed  = "updated_failed";


        public const string CacheClearSuccess = "cache_clear_success";
        public const string CacheClearFailed = "cache_clear_failed";


        public const string ErrorJsonStructure = "error_json_structure";


 

    }
}
