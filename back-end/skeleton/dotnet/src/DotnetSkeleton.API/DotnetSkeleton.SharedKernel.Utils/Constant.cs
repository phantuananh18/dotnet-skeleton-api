namespace DotnetSkeleton.SharedKernel.Utils;

public class Constant
{
    public static class RoleType
    {
        public const string System = "System";
        public const string Admin = "Admin";
        public const string Client = "Client";
    }

    public static class AuthenticateAttribute
    {
        public const string Id = "id";
        public const string Role = "role";
        public const string BearerTokenPrefix = "Bearer";
    }

    public static class DatabaseAttribute
    {
        public static class DefaultUser
        {
            public const int UserId = 1;
        }
    }

    public static class FieldName
    {
        public const string Username = "Username";
        public const string UserId = "UserId";
        public const string Email = "Email";
        public const string User = "User";
    }

    public static class DateTimeFormatter
    {
        public static string ShortDate = "yyyy-MM-dd";
        public static string LongDate = "yyyy-MM-dd HH:mm:ss";
    }

    public static class HeaderAttribute
    {
        public const string Authorization = "Authorization";
        public const string ContentLanguage = "Content-Language";
        public const string DefaultContentLanguage = "en-US";
        public const string Timezone = "Timezone";
        public const string DefaultTimeZone = "Asia/Ho_Chi_Minh";
        public const string DeliveryCount = "x-delivery-count";
        public const string XRequestSource = "X-Request-Source";
    }

    public static class ParamAttribute
    {
        public const string PageNumber = "pageNumber";
        public const string PageSize = "pageSize";
        public const string Filter = "filter";
        public const string FilterCondition = "filterCondition";
        public const string Sort = "sort";
        public const string SearchText = "searchText";
        public const string RoleName = "roleName";
    }

    public static class EmailTemplatePath
    {
        public const string ForgotPasswordPath = "Assets/MailTemplates/ForgotPassword.html";
        public const string ResetPasswordSuccessfullyPath = "Assets/MailTemplates/ResetPasswordSuccessfully.html";
        public const string WelcomeEmailPath = "Assets/MailTemplates/WelcomeEmail.html";
    }

    public static class EmailTemplateName
    {
        public const string ResetPassword = "ResetPassword";
        public const string ResetPasswordSuccess = "ResetPasswordSuccess";
        public const string WelcomeEmail = "WelcomeEmail";
    }

    public static class EmailPlaceHolder
    {
        public const string UserName = "[Username]";
        public const string CallbackUrl = "[CallBackUrl]";
        public const string TempPassword = "[TempPassword]";
        public const string UserFullName = "[UserFullName]";
    }

    public static class FilterValueType
    {
        public const string DateTime = "datetime";
        public const string Date = "date";
        public const string Number = "number";
        public const string String = "string";
        public const string Enum = "enum";
        public const string Boolean = "boolean";
    }

    public static class FilterQueries
    {
        public const string FilterEqual = "{0} = {1}";
        public const string FilterAnd = "({0} AND {1})";
        public const string FilterOr = " OR ";
        public const string FilterIn = " {0} IN {1} ";
        public const string FilterApostrophe = "'{0}'";
        public const string FilterComma = ",";
        public const string FilterParentheses = "({0})";
        public const string And = " AND ";
    }

    public static class FilterOperator
    {
        public const string Equal = "equal";
        public const string NotEqual = "notequal";
        public const string LessThan = "lessthan";
        public const string LessThanOrEqual = "lessthanorequal";
        public const string GreaterThan = "greaterthan";
        public const string GreaterThanOrEqual = "greaterthanorequal";
        public const string In = "in";
        public const string NotIn = "iotin";
        public const string StartWith = "startwith";
        public const string EndWith = "endwith";
        public const string Contains = "contains";
    }

    public static class SortDirection
    {
        public const string Descending = "desc";
        public const string Ascending = "asc";
    }

    public static class BooleanValue
    {
        public const string True = "true";
        public const string False = "false";
    }

    public static class AuthMethod
    {
        public const string OAuth = "OAuth";
        public const string SSO = "SSO";
        public const string UsernamePassword = "UsernamePassword";
        public const string LDAP = "LDAP";
        public const string SAML = "SAML";
        public const string OpenID = "OpenID";
        public const string JWT = "JWT";
        public const string Biometric = "Biometric";
        public const string MFA = "MFA";
        public const string APIKey = "APIKey";
        public const string Certificate = "Certificate";
        public const string SocialLogin = "SocialLogin";
    }

    public static class SystemInfo
    {
        public const string AppName = "DC8-Framework v1.0";
        public const string UserModule = "UserModule";
        public const string EmailModule = "EmailModule";
        public const string MessageModule = "MessageModule";
        public const string NotificationModule = "NotificationModule";
        public const string ChatModule = "ChatModule";
        public const string IdentityModule = "IdentityModule";
        public const string MySqlDatabase = "MySQL";
        public const string MongoDbDatabase = "MongoDB";
        public const string TokenBucketRateLimit = "token";
        public const string Logging = "Logging";
    }

    public static class SupportedCulture
    {
        public const string EnUs = "en-US";
        public const string EsEs = "es-ES";
    }

    public enum HealthCheckStatus
    {
        /// <summary>
        /// The service is fully operational with no known issues.
        /// </summary>
        Good = 0,

        /// <summary>
        /// The service is operational, but there is some non-critical information or minor issues.
        /// </summary>
        Information = 1,

        /// <summary>
        /// The service is operational, but there are potential issues that could escalate if not addressed.
        /// </summary>
        Warning = 2,

        /// <summary>
        /// The service is down or experiencing significant issues that impact its functionality.
        /// </summary>
        Critical = 3,

        /// <summary>
        /// The service is not applicable or not required in the current context, so its status is ignored.
        /// </summary>
        NotApplicable = 4
    }

    public static class Regex
    {
        public const string PhoneNumberRegex = @"^\d{9,12}$";
    }

    public enum EmailType
    {
        Support = 0,
        NoReply = 1
    }

    public static class CommunicationType
    {
        public const string Email = "Email";
        public const string Text = "Text";
    }

    public static class CommunicationDirection
    {
        public const string Outgoing = "Outgoing";
        public const string Incoming = "Incoming";
    }

    public static class CommunicationStatus
    {
        public const string Pending = "Pending";
        public const string Delivered = "Delivered";
        public const string Failed = "Failed";
        public const string Bounced = "Bounced";
        public const string Queued = "Queued";
        public const string Rejected = "Rejected";
    }

    public static class UserSearchKeyword
    {
        public const string SearchUser = @" AND (u.FirstName LIKE '%{0}%' OR u.LastName LIKE '%{0}%' OR u.Email LIKE '%{0}%' OR u.MobilePhone LIKE '%{0}%') ";
    }

    public static class RoleSearchKeyword
    {
        public const string SearchRole = @" WHERE Name LIKE '%{0}%' ";
        public const string SearchRoleId = @" AND rp.RoleId = {0}";
    }

    public static class NotificationType
    {
        public const string NewUser = "New User";
        public const string NewEmail = "New Email";
        public const string NewSms = "New Sms";
    }

    public static class NotificationFilter
    {
        public const string NewUser = " r.Name IN ('Admin') ";
        public const string NewEmail = " u.UserId = {0}";
    }

    public static class ContentType
    {
        public const string TextPlain = "text/plain";
        public const string TextHtml = "text/html";
        public const string TextCss = "text/css";
        public const string TextJavaScript = "text/javascript";
        public const string ApplicationJson = "application/json";
        public const string Json = "json";
        public const string ApplicationXml = "application/xml";
        public const string ApplicationOctetStream = "application/octet-stream";
        public const string ApplicationFormUrlEncoded = "application/x-www-form-urlencoded";
        public const string ApplicationPdf = "application/pdf";
        public const string ApplicationZip = "application/zip";
        public const string ImageJpeg = "image/jpeg";
        public const string ImagePng = "image/png";
        public const string ImageGif = "image/gif";
        public const string ImageSvg = "image/svg+xml";
        public const string AudioMpeg = "audio/mpeg";
        public const string VideoMp4 = "video/mp4";
        public const string MultipartFormData = "multipart/form-data";
        public const string MultipartAlternative = "multipart/alternative";
    }

    public static class ApiEndpoints
    {
        public static class UserEndpoints
        {
            public const string Base = "/api/v1/users";
            public const string CreateUser = "/api/v1/users";
            public const string UpdateUser = @"/api/v1/users/{0}";
            public const string DeleteUser = @"/api/v1/users/{0}";
            public const string GetUserByUserId = @"/api/v1/users/{0}";
            public const string GetAllUsersWithPagination = "/api/v1/users";
        }

        public static class RoleEndpoints
        {
            public const string Base = "/api/v1/roles";
            public const string GetRoleByRoleId = @"/api/v1/roles/{0}";
            public const string GetAllRolesWithPagination = "/api/v1/roles";
            public const string CreateRole = "/api/v1/roles";
            public const string AssignRolePermission = @"/api/v1/roles/{0}/assign-permission";
            public const string UpdateRole = "/api/v1/roles";
            public const string DeleteRole = @"/api/v1/roles/{0}";
        }

        public static class PermissionEndpoints
        {
            public const string Base = "/api/v1/permissions";
            public const string GetPermissionByPermissionId = @"/api/v1/permissions/{0}";
            public const string GetAllPermissionsWithPagination = "/api/v1/permissions";
            public const string CreatePermission = "/api/v1/permissions";
            public const string UpdatePermission = "/api/v1/permissions";
            public const string DeletePermission = @"/api/v1/permissions/{0}";
        }

        public static class RolePermissionEndpoints
        {
            public const string Base = "/api/v1/role-permissions";
            public const string CreateRolePermission = "/api/v1/role-permissions";
            public const string UpdateRolePermission = "/api/v1/role-permissions";
            public const string DeleteRolePermission = @"/api/v1/role-permissions/{0}";
        }

        public static class IdentityEndpoints
        {
            public const string Base = "/api/v1/auth";
            public const string SignIn = "/api/v1/auth/sign-in";
            public const string SignUp = "/api/v1/auth/sign-up";
            public const string RefreshToken = "/api/v1/auth/refresh-token";
            public const string ForgotPassword = "/api/v1/auth/forgot-password";
            public const string ResetPassword = @"/api/v1/auth/reset-password?token={0}";
            public const string OAuthCallBack = "/api/v1/auth/oauth-callback";
        }

        public static class EmailEndpoints
        {
            public const string Base = "/api/v1/emails";
            public const string SendMail = "/api/v1/emails/send-out";
            public const string QueueMail = "/api/v1/emails/send-out/queue";
        }

        public static class MessageEndpoints
        {
            public const string Base = "/api/v1/message";
            public const string SendSms = "/api/v1/message/send-sms";
            public const string ReceiveSms = "/api/v1/message/receive-sms";
            public const string StartVerification = "/api/v1/message/start-verification";
            public const string CheckVerification = "/api/v1/message/check-verification";
        }
    }

    public static class QueryPrefix
    {
        public const string Filter = "&filter=";
        public const string Sort = "&sort=";
    }

    public static class ServiceName
    {
        public const string EmailService = "EmailService";
        public const string UserService = "UserService";
        public const string IdentityService = "IdentityService";
        public const string NotificationService = "NotificationService";
        public const string MessageService = "MessageService";
        public const string CoreService = "CoreService";
    }
}