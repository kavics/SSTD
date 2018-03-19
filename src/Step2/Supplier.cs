using System;
using System.Linq;

namespace Step2
{
    public class Supplier
    {
        private string _contactName;
        public string ContactName
        {
            get => _contactName;
            set
            {
                if(value == null)
                    throw new ArgumentNullException(nameof(ContactName));
                if(value.Length == 0)
                    throw new ArgumentException($"{nameof(ContactName)} cannot be empty.");

                _contactName = value;
            }
        }

        private string _contactEmail;
        public string ContactEmail
        {
            get => _contactEmail;
            set
            {
                if(value == null)
                    throw new ArgumentNullException(nameof(ContactEmail));
                if (value.Length == 0)
                    throw new ArgumentException($"{nameof(ContactEmail)} cannot be empty.");

                _contactEmail = value;
            }
        }

        private string _contactPhone;
        public string ContactPhone
        {
            get => _contactPhone;
            set
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
                if(value.Any(c=>!char.IsNumber(c)))
                    throw new ArgumentException($"{nameof(ContactPhone)} accepts '-' or ' ' characters as separator.");

                if (value.Length < 9)
                    throw new ArgumentException($"{nameof(ContactPhone)} is too short.");
                if (value.Length > 9)
                    throw new ArgumentException($"{nameof(ContactPhone)} is too long.");

                _contactPhone = value;
            }
        }
    }
}
