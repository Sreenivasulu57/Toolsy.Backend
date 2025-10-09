

namespace VSC.Toolsy.Common.Constants
{
    public static class RouteMap
    {
        private const string Base = "api/v1/";

        public static class Address
        {
            public const string Base = RouteMap.Base + "address";
            public const string GetByProfileId = "by-profileid";
        }

        public static class Admin
        {
            public const string Base = RouteMap.Base + "admin";
            public const string GetAllUsers = "users";
            public const string GetAllVerifiedUsers = "users/verified";
            public const string GetAllUnverifiedUsers = "users/unverified";
            public const string GetAllDeletedUsers = "users/deleted";
            public const string ApproveProfile = "profiles/approve-by-profileid";
            public const string DeleteUser = "user/delete-by-profileid";
            public const string GetUserByProfileId = "user/profileid";
        }

        public static class Email
        {
            public const string Base = RouteMap.Base + "email";
            public const string SendOtp = "send-otp";
            public const string VerifyOtp = "verify-otp";
        }

        public static class Owner
        {
            public const string Base = RouteMap.Base + "owner";
            public const string Register = "register";
            public const string GetByEmail = "by-email";
            public const string GetByOwnerId = "by-ownerid";
        }

        public static class Profile
        {
            public const string Base = RouteMap.Base + "profile";
        }

        public static class Storage
        {
            public const string Base = RouteMap.Base + "storage";
            public const string SaveFile = "save-file";
            public const string GetByFileName = "by-file-name";
            public const string DeleteByFileName = "by-file-name";
        }

        public static class Tool
        {
            public const string Base = RouteMap.Base + "tool";
            public const string Save = "save";
            public const string UpdateByToolId = "update-by-toolid";
            public const string DeleteByToolId = "delete-by-toolid";
            public const string FetchAllByOwnerId = "fetch-all-by-ownerid";
            public const string FetchAll = "fetch-all";
        }

        public static class User
        {
            public const string Base = RouteMap.Base + "user";
            public const string Save = "save";
            public const string DeleteByProfileId = "delete-user-by-profileid";
            public const string Update = "update";
        }
    }
}
