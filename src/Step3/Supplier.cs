using System;
using System.Linq;
using System.Net.Mail;

namespace Step3
{
    public class Supplier
    {
        private string _contactName;
        public string ContactName
        {
            get => _contactName;
            set
            {
                ValidateName(value);
                _contactName = value;
            }
        }

        private string _contactEmail;
        public string ContactEmail
        {
            get => _contactEmail;
            set
            {
                ValidateEmail(value);
                _contactEmail = value;
            }
        }

        private string _contactPhone;
        public string ContactPhone
        {
            get => _contactPhone;
            set => _contactPhone = GetValidPhone(value);
        }

        internal string Validate()
        {
            try
            {
                ValidateName(ContactName);
                ValidateEmail(ContactEmail);
                GetValidPhone(ContactPhone);
                return null;
            }
            catch(Exception e)
            {
                // suppressed
                return e.Message;
            }
        }
        private void ValidateName(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(ContactName));
            if (value.Length == 0)
                throw new ArgumentException($"{nameof(ContactName)} cannot be empty.");
        }
        private void ValidateEmail(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(ContactEmail));
            if (value.Length == 0)
                throw new ArgumentException($"{nameof(ContactEmail)} cannot be empty.");
            var m = new MailAddress(value);
        }
        private string GetValidPhone(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(ContactPhone));
            if (value.Length == 0)
                throw new ArgumentException($"{nameof(ContactPhone)} cannot be empty.");

            if (value.StartsWith("+36"))
                value = value.Substring(3);
            else if (value.StartsWith("06"))
                value = value.Substring(2);
            else
                throw new ArgumentException($"{nameof(ContactPhone)} must start with '+36' or '06'.");

            value = value.Replace("-", "").Replace(" ", "");
            if (value.Any(c => !char.IsNumber(c)))
                throw new ArgumentException($"{nameof(ContactPhone)} accepts '-' or ' ' characters as separator.");

            if (value.Length < 9)
                throw new ArgumentException($"{nameof(ContactPhone)} is too short.");
            if (value.Length > 9)
                throw new ArgumentException($"{nameof(ContactPhone)} is too long.");

            return "+36" + value;
        }
    }
}
