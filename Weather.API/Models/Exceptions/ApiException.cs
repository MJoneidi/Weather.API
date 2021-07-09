using System;

namespace Weather.API.Models.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message, string errorCode) : base(message)
        {
            this.ErrorCode = errorCode;
        }

        public string ErrorCode { get; set; }
    }
}
