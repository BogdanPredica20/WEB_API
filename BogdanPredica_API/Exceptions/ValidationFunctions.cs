﻿using BogdanPredica_API.Helpers.Enums;

namespace BogdanPredica_API.Exceptions
{
    public class ValidationFunctions
    {
        public static void ThrowExceptionWhenDateIsNotValid (DateTime? startTime, DateTime? endTime)
        {
            if(startTime.HasValue && endTime.HasValue && startTime > endTime)
            {
                throw new ModelValidationException(ErrorMessagesEnum.Announcement.StartEndDatesError);
            }
        }
    }
}
