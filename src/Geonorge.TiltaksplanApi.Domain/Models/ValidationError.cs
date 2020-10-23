using System;

namespace Geonorge.TiltaksplanApi.Domain.Models
{
    public class ValidationError : IEquatable<ValidationError>
    {
        public string Property { get; set; }
        public string ErrorCode { get; set; }

        public ValidationError(string property, string errorCode)
        {
            Property = property;
            ErrorCode = errorCode;
        }

        public bool Equals(ValidationError other)
        {
            return other != null && Property == other.Property && ErrorCode == other.ErrorCode;
        }
    }
}
