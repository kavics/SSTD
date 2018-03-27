using System;
using System.Linq;
using System.Net.Mail;
using SenseNet.ContentRepository;
using SenseNet.ContentRepository.Schema;
using SenseNet.ContentRepository.Storage;

namespace Step4
{
    [ContentHandler]
    public class Supplier : GenericContent
    {
        public Supplier(Node parent) : this(parent, null) { }
        public Supplier(Node parent, string nodeTypeName) : base(parent, nodeTypeName) { }
        protected Supplier(NodeToken nt) : base(nt) { }

        [RepositoryProperty(nameof(ContactName), RepositoryDataType.String)]
        public string ContactName
        {
            get => base.GetProperty<string>(nameof(ContactName));
            set
            {
                ValidateName(value);
                base.SetProperty(nameof(ContactName), value);
            }
        }

        [RepositoryProperty(nameof(ContactEmail), RepositoryDataType.String)]
        public string ContactEmail
        {
            get => base.GetProperty<string>(nameof(ContactEmail));
            set
            {
                ValidateEmail(value);
                base.SetProperty(nameof(ContactEmail), value);
            }
        }

        [RepositoryProperty(nameof(ContactPhone), RepositoryDataType.String)]
        public string ContactPhone
        {
            get => base.GetProperty<string>(nameof(ContactPhone));
            set => base.SetProperty(nameof(ContactPhone), GetValidPhone(value));
        }

        public override object GetProperty(string name)
        {
            switch (name)
            {
                case nameof(ContactEmail):
                    return ContactEmail;
                case nameof(ContactName):
                    return ContactName;
                case nameof(ContactPhone):
                    return ContactPhone;
                default:
                    return base.GetProperty(name);
            }
        }
        public override void SetProperty(string name, object value)
        {
            switch (name)
            {
                case nameof(ContactEmail):
                    ContactEmail = (string)value;
                    break;
                case nameof(ContactName):
                    ContactName = (string)value;
                    break;
                case nameof(ContactPhone):
                    ContactPhone = (string)value;
                    break;
                default:
                    base.SetProperty(name, value);
                    break;
            }
        }

        public override void Save(NodeSaveSettings settings)
        {
            Validate();
            base.Save(settings);

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
        private static void ValidateName(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(ContactName));
            if (value.Length == 0)
                throw new ArgumentException($"{nameof(ContactName)} cannot be empty.");
        }
        private static void ValidateEmail(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(ContactEmail));
            if (value.Length == 0)
                throw new ArgumentException($"{nameof(ContactEmail)} cannot be empty.");
            var m = new MailAddress(value);
        }
        private static string GetValidPhone(string value)
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
