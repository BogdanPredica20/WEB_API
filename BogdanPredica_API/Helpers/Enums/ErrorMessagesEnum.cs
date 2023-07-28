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
            public const string StartEndDatesError = "Data de final nu poate fi mai devreme decat data de inceput";
            public const string TitleExistsError = "Titlul exista deja in baza de date";
        }

        public static class CodeSnippet
        {
            public const string NotFound = "No code snippet found";
            public const string NotFoundById = "Code snippet with given id doesn't exist";
            public const string BadRequest = "Formatul transmis nu este corect";
        }
    }
}
