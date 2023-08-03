namespace BogdanPredica_API.Helpers.Enums
{
    public static class ErrorMessagesEnum
    {
        public static class Announcement
        {
            public const string NotFound = "No announcement found";
            public const string NotFoundById = "Announcement with given id doesn't exist";
            public const string BadRequest = "Formatul transmis nu este corect";
            public const string ZeroUpdatesToSave = "Nu sunt modificari pe anunt pentru a fi salvate";
            public const string StartEndDatesError = "Data de final nu poate fi inaintea celei de inceput";
            public const string TitleExistsError = "Titlul exista deja in baza de date";
        }

        public static class CodeSnippet
        {
            public const string NotFound = "No code snippet found";
            public const string NotFoundById = "Code snippet with given id doesn't exist";
            public const string BadRequest = "Formatul transmis nu este corect";
        }

        public static class Member
        {
            public const string NotFound = "No member found";
            public const string NotFoundById = "Member with given id doesn't exist or they have an active membership or code snippets";
            public const string BadRequest = "Formatul transmis nu este corect";
            public const string UsernameExistsError = "Username-ul exista deja";
        }

        public static class MembershipType
        {
            public const string NotFound = "No membership type found";
            public const string NotFoundById = "Membership type with given id doesn't exist";
            public const string BadRequest = "Formatul transmis nu este corect";
        }

        public static class Membership
        {
            public const string NotFound = "No membership found";
            public const string NotFoundById = "Membership with given id doesn't exist";
            public const string BadRequest = "Formatul transmis nu este corect";
        }
    }
}
