using System;

namespace FormsCreator.Domain.Core.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string code)
        {
            Code = code;
        }

        public DomainException(string code, string message)
            : base(message)
        {
            Code = code;
        }

        public string Code { get; }
        
        public static class AuthExceptionCodes
        {
            public const string NotFound = "10001";
            public const string InvalidCredentials = "10002";
        }

        public static class AppExceptionCodes
        {
            public const string StatusChangeError = "20001";
            public const string FormNotPublished = "20002";
        }
    }
}