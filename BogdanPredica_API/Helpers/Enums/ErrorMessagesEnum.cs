namespace BogdanPredica_API.Helpers.Enums
{
    public static class ErrorMessagesEnum
    {
        public static class Announcement
        {
            public const string NotFound = "No announcement found";
            public const string NotFoundById = "Announcement with given id doesn't exist";
            public const string BadRequest = "Formatul transmis nu este corect";
        }
    }
}
