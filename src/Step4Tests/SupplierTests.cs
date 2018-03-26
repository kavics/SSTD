using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SenseNet.ContentRepository;
using SenseNet.ContentRepository.Schema;
using SenseNet.Tests;
using Step4;

namespace Step4Tests
{
    [TestClass]
    public class SupplierTests : TestBase
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Supplier4_ContactName_Null()
        {
            ValidateSupplierName(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Supplier4_ContactName_Empty()
        {
            ValidateSupplierName(string.Empty);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Supplier4_ContactEmail_Null()
        {
            ValidateSupplierEmail(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Supplier4_ContactEmail_Empty()
        {
            ValidateSupplierEmail(string.Empty);
        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Supplier3_ContactEmail_Invalid()
        {
            ValidateSupplierEmail("@");
        }


        [TestMethod]
        public void Supplier4_Phone_Valid()
        {
            Assert.AreEqual("+36201234567", GetValidSupplierPhone("+36 20 123 45 67"));
            Assert.AreEqual("+36201234567", GetValidSupplierPhone("06 20 123 45 67"));
            Assert.AreEqual("+36201234567", GetValidSupplierPhone("06-20-123-45-67"));
            Assert.AreEqual("+36201234567", GetValidSupplierPhone("06-20  123-4567"));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Supplier4_Phone_Invalid_Null()
        {
            GetValidSupplierPhone(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Supplier4_Phone_Invalid_Empty()
        {
            GetValidSupplierPhone(string.Empty);
        }
        [TestMethod]
        public void Supplier4_Phone_WrongPrefix()
        {
            try
            {
                GetValidSupplierPhone("+99 20 123 4567");
                Assert.Fail("Exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("+36"));
                Assert.IsTrue(e.Message.Contains("06"));
            }
        }
        [TestMethod]
        public void Supplier4_Phone_WrongSeparator()
        {
            try
            {
                GetValidSupplierPhone("06 20/123 4567");
                Assert.Fail("Exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("separator"));
            }
        }
        [TestMethod]
        public void Supplier4_Phone_TooShort1()
        {
            try
            {
                GetValidSupplierPhone("06");
                Assert.Fail("Exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("too short"));
            }
        }
        [TestMethod]
        public void Supplier4_Phone_TooShort2()
        {
            try
            {
                GetValidSupplierPhone("06 20 123 456");
                Assert.Fail("Exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("too short"));
            }
        }
        [TestMethod]
        public void Supplier4_Phone_TooLong()
        {
            try
            {
                GetValidSupplierPhone("06 20 123 45678");
                Assert.Fail("Exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("too long"));
            }
        }


        [TestMethod]
        public void Supplier4_NoName()
        {
            Test(() =>
            {
                InstallSupplierContentType();

                var supplier = new Supplier(Repository.Root)
                {
                    ContactEmail = "supplier1@host.hu",
                    ContactPhone = "06 20 123 4567"
                };

                Assert.AreNotEqual(null, supplier.Validate());
            });
        }
        [TestMethod]
        public void Supplier4_NoMail()
        {
            Test(() =>
            {
                InstallSupplierContentType();

                var supplier = new Supplier(Repository.Root)
                {
                    ContactEmail = "supplier1@host.hu",
                    ContactPhone = "06 20 123 4567"
                };

                Assert.AreNotEqual(null, supplier.Validate());
            });
        }

        [TestMethod]
        public void Supplier4_NoPhone()
        {
            Test(() =>
            {
                InstallSupplierContentType();

                var supplier = new Supplier(Repository.Root)
                {
                    ContactEmail = "supplier1@host.hu",
                    ContactPhone = "06 20 123 4567"
                };

                Assert.AreNotEqual(null, supplier.Validate());
            });
        }

        [TestMethod]
        public void Supplier4_Valid()
        {
            Test(() =>
            {
                InstallSupplierContentType();

                var supplier = new Supplier(Repository.Root)
                {
                    ContactName = "supplier1",
                    ContactEmail = "supplier1@host.hu",
                    ContactPhone = "06 20 123 4567"
                };

                Assert.AreEqual(null, supplier.Validate());
            });
        }

        /* ================================================================= Tools */

        private void ValidateSupplierName(string value)
        {
            var accessor = new PrivateType(typeof(Supplier));
            try
            {
                accessor.InvokeStatic("ValidateName", value);
            }
            catch (Exception e)
            {
                throw (e.InnerException ?? e);
            }
        }
        private void ValidateSupplierEmail(string value)
        {
            var accessor = new PrivateType(typeof(Supplier));
            try
            {
                accessor.InvokeStatic("ValidateEmail", value);
            }
            catch (Exception e)
            {
                throw (e.InnerException ?? e);
            }
        }
        private string GetValidSupplierPhone(string value)
        {
            var accessor = new PrivateType(typeof(Supplier));
            try
            {
                return (string)accessor.InvokeStatic("GetValidPhone", value);
            }
            catch (Exception e)
            {
                throw (e.InnerException ?? e);
            }
        }

        private void InstallSupplierContentType()
        {
            ContentTypeInstaller.InstallContentType(@"<?xml version='1.0' encoding='utf-8'?>
<ContentType name = 'Supplier' parentType='GenericContent' handler='Step4.Supplier' xmlns='http://schemas.sensenet.com/SenseNet/ContentRepository/ContentTypeDefinition'>
  <DisplayName>Beszállító</DisplayName>
  <Description>Beszállító magyar kontakt személlyel.</Description>
  <Icon>Application</Icon>
  <Fields>
    <Field name = 'ContactName' type='ShortText' />
    <Field name = 'ContactEmail' type='ShortText' />
    <Field name = 'ContactPhone' type='ShortText' />
  </Fields>
</ContentType>");
        }
    }
}
