using SenseNet.ContentRepository;
using SenseNet.ContentRepository.Schema;
using SenseNet.ContentRepository.Storage;
using System;

namespace SpaceBenderSnPlugin
{
    [ContentHandler]
    public class Contract : GenericContent
    {
        public Contract(Node parent) : this(parent, null) { }
        public Contract(Node parent, string nodeTypeName) : base(parent, nodeTypeName) { }
        protected Contract(NodeToken nt) : base(nt) { }


        private const string ContactNameProperty = nameof(ContactName);
        [RepositoryProperty(ContactNameProperty, RepositoryDataType.String)]
        public string ContactName
        {
            get { return base.GetProperty<string>(ContactNameProperty); }
            set
            {
                ValidateContactName(value);
                base.SetProperty(ContactNameProperty, value);
            }
        }

        private const string ContactEmailProperty = nameof(ContactEmail);
        [RepositoryProperty(ContactEmailProperty, RepositoryDataType.String)]
        public string ContactEmail
        {
            get { return base.GetProperty<string>(ContactEmailProperty); }
            set
            {
                ValidateContactEmail(value);
                base.SetProperty(ContactEmailProperty, value);
            }
        }

        private const string ContactPhoneProperty = nameof(ContactPhone);
        [RepositoryProperty(ContactPhoneProperty, RepositoryDataType.String)]
        public string ContactPhone
        {
            get { return base.GetProperty<string>(ContactPhoneProperty); }
            set
            {
                ValidateContactPhone(value);

                // remove +36 or 06 prefix.
                if (value.StartsWith("+36"))
                    value = value.Substring(3);
                else
                    value = value.Substring(2);

                // remove separators.
                value = value.Replace(" ", "").Replace("-", "");

                base.SetProperty(ContactPhoneProperty, value);
            }
        }


        public override object GetProperty(string name)
        {
            switch (name)
            {
                case ContactEmailProperty:
                    return this.ContactEmail;
                case ContactNameProperty:
                    return this.ContactName;
                case ContactPhoneProperty:
                    return this.ContactPhone;

                default:
                    return base.GetProperty(name);
            }
        }
        public override void SetProperty(string name, object value)
        {
            switch (name)
            {
                case ContactEmailProperty:
                    this.ContactEmail = (string)value;
                    break;
                case ContactNameProperty:
                    this.ContactName = (string)value;
                    break;
                case ContactPhoneProperty:
                    this.ContactPhone = (string)value;
                    break;
                default:
                    base.SetProperty(name, value);
                    break;
            }
        }


        public override void Save()
        {
            Validate();
            base.Save();
        }


        private void ValidateContactName(string value)
        {
            throw new NotImplementedException();
        }
        private void ValidateContactEmail(string value)
        {
            throw new NotImplementedException();
        }
        private void ValidateContactPhone(string value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value.StartsWith("+36"))
                value = value.Substring(3);
            else if (value.StartsWith("06"))
                value = value.Substring(2);
            else
                throw new ArgumentException("Phone number must start with '+36' or '06'");

            value = value.Replace(" ", "").Replace("-", "");

            foreach (var c in value)
                if (!Char.IsNumber(c))
                    throw new ArgumentException("Phone number can contain numbers and '-' or ' ' as separator.");

            var expectedLength = "201234567".Length;
            if (value.Length < expectedLength)
                throw new ArgumentException("Phone number is too short");
            if (value.Length > expectedLength)
                throw new ArgumentException("Phone number is too long");
        }
        private void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
