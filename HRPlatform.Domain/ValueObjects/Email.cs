using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HRPlatform.Domain.ValueObjects
{
    public record Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Email cannot be empty");

            if (!IsValidEmail(value))
                throw new DomainException("Invalid email format");

            Value = value.ToLower().Trim();
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase,
                    TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static implicit operator string(Email email) => email.Value;
        public static explicit operator Email(string email) => new(email);

        public override string ToString() => Value;
    }
}
