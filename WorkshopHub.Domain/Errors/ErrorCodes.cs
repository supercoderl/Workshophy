using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Errors
{
    public static class ErrorCodes
    {
        public const string CommitFailed = "COMMIT_FAILED";
        public const string ObjectNotFound = "OBJECT_NOT_FOUND";
        public const string InsufficientPermissions = "UNAUTHORIZED";
        public const string InvalidObject = "INVALID_OBJECT";
        public const string NotAllowChange = "NOT_ALLOW_CHANGE";
        public const string NotHavePermission = "NOT_HAVE_PERMISSION";
        public const string ExceptionThrow = "EXCEPTION_THROW";
    }
}
