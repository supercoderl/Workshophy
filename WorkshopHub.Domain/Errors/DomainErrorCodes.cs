using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Errors
{
    public static class DomainErrorCodes
    {
        public static class User
        {
            // User Validation
            public const string EmptyId = "USER_EMPTY_ID";
            public const string EmptyFirstName = "USER_EMPTY_FIRST_NAME";
            public const string EmptyLastName = "USER_EMPTY_LAST_NAME";
            public const string EmailExceedsMaxLength = "USER_EMAIL_EXCEEDS_MAX_LENGTH";
            public const string FirstNameExceedsMaxLength = "USER_FIRST_NAME_EXCEEDS_MAX_LENGTH";
            public const string LastNameExceedsMaxLength = "USER_LAST_NAME_EXCEEDS_MAX_LENGTH";
            public const string InvalidEmail = "USER_INVALID_EMAIL";
            public const string InvalidRole = "USER_INVALID_ROLE";

            // User Password Validation
            public const string EmptyPassword = "USER_PASSWORD_MAY_NOT_BE_EMPTY";
            public const string ShortPassword = "USER_PASSWORD_MAY_NOT_BE_SHORTER_THAN_6_CHARACTERS";
            public const string LongPassword = "USER_PASSWORD_MAY_NOT_BE_LONGER_THAN_50_CHARACTERS";
            public const string UppercaseLetterPassword = "USER_PASSWORD_MUST_CONTAIN_A_UPPERCASE_LETTER";
            public const string LowercaseLetterPassword = "USER_PASSWORD_MUST_CONTAIN_A_LOWERCASE_LETTER";
            public const string NumberPassword = "USER_PASSWORD_MUST_CONTAIN_A_NUMBER";
            public const string SpecialCharPassword = "USER_PASSWORD_MUST_CONTAIN_A_SPECIAL_CHARACTER";

            // General
            public const string AlreadyExists = "USER_ALREADY_EXISTS";
            public const string PasswordIncorrect = "USER_PASSWORD_INCORRECT";
        }

        public static class  RefreshToken
        {
            // Refresh Token Validation
            public const string EmptyId = "REFRESH_TOKEN_EMPTY_ID";
            public const string EmptyUserId = "REFRESH_TOKEN_EMPTY_USER_ID";
            public const string EmptyToken = "REFESH_TOKEN_EMPTY_TOKEN";
        }

        public static class Workshop
        {
            // Workshop Validation
            public const string EmptyId = "WORKSHOP_EMPTY_ID";
            public const string EmptyOrganizerId = "WORKSHOP_EMPTY_ORGANIZER_ID";
            public const string EmptyTitle = "WORKSHOP_EMPTY_TITLE";
            public const string EmptyCategoryId = "WORKSHOP_EMPTY_CATEGORY_ID";
            public const string EmptyLocation = "WORKSHOP_EMPTY_LOCATION";
        }

        public static class Ticket
        {
            // Ticket Validation
            public const string EmptyId = "TICKET_EMPTY_ID";
            public const string EmptyUserId = "TICKET_EMPTY_USER_ID";
            public const string EmptyWorkshopId = "TICKET_EMPTY_WORKSHOP_ID";
        }

        public static class Booking
        {
            // Booking Validation
            public const string EmptyId = "BOOKING_EMPTY_ID";
            public const string EmptyUserId = "BOOKING_EMPTY_USER_ID";
            public const string EmptyWorkshopId = "BOOKING_EMPTY_WORKSHOP_ID";
        }

        public static class Payment
        {
            // Payment Validation
            public const string EmptyDescription = "PAYMENT_EMPTY_DESCIPRTION";
        }

        public static class Review
        {
            // Review Validation
            public const string EmptyId = "REVIEW_EMPTY_ID";
        }

        public static class Badge
        {
            // Badge Validation
            public const string EmptyId = "BADGE_EMPTY_ID";
            public const string EmptyName = "BADGE_EMPTY_NAME";
            public const string EmptyArea = "BADGE_EMPTY_AREA";
            public const string EmptyImageUrl = "BADGE_EMPTY_IMAGE_URL";
        }

        public static class BlogPost
        {
            // Blog Post Validation
            public const string EmptyId = "BLOG_POST_EMPTY_ID";
            public const string EmptyTitle = "BLOG_POST_EMPTY_TITLE";
            public const string EmptyContent = "BLOG_POST_EMPTY_CONTENT";
            public const string EmptyUserId = "BLOG_POST_EMPTY_USER_ID";
        }
    }
}
