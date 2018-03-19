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
                    throw new ArgumentNullException(ContactName);
                if(value.Length == 0)
                    throw new ArgumentException("ContactName cannot be empty.");

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
                    throw new ArgumentNullException(ContactEmail);
                if (value.Length == 0)
                    throw new ArgumentException("ContactEmail cannot be empty.");

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
                    throw new ArgumentNullException(ContactEmail);
                if (value.Length == 0)
                    throw new ArgumentException("ContactPhone cannot be empty.");

                if (value.StartsWith("+36"))
                    value = value.Substring(3);
                else if (value.StartsWith("06"))
                    value = value.Substring(2);
                else
                    throw new ArgumentException("ContactPhone must start with '+36' or '06'.");

                value = value.Replace("-", "").Replace(" ", "");
                if(value.Any(c=>!char.IsNumber(c)))
                    throw new ArgumentException("ContactPhone accepts '-' or ' ' characters as separator.");

                var expectedLentgth = "201234567".Length;
                if (value.Length < expectedLentgth)
                    throw new ArgumentException("ContactPhone is too short.");
                if (value.Length > expectedLentgth)
                    throw new ArgumentException("ContactPhone is too long.");

                _contactPhone = value;
            }
        }
    }
}
