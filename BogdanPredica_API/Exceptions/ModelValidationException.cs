﻿namespace BogdanPredica_API.Exceptions
{
    public class ModelValidationException : Exception
    {
        public ModelValidationException(string message) : base(message) { }
    }
}
