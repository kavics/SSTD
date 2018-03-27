using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SenseNet.ContentRepository;
using SenseNet.ContentRepository.Schema;
using SenseNet.ContentRepository.Storage;
using SenseNet.Tests;
using Step4;

namespace Step4Tests
{
    [TestClass]
    public class SupplierIntegrationTests : TestBase
    {
        [TestMethod]
        public void SupplierIntegration_Invalid_NoName()
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
        public void SupplierIntegration_Invalid_NoMail()
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
        public void SupplierIntegration_Invalid_NoPhone()
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
        public void SupplierIntegration_Valid()
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

        [TestMethod]
        public void SupplierIntegration_Save()
        {
            Test(() =>
            {
                InstallSupplierContentType();

                var root = new SystemFolder(Repository.Root);
                root.Save();

                for (var i = 1; i < 10; i++)
                {
                    var supplier = new Supplier(root)
                    {
                        Name = $"Supplier-{i}",
                        Index = i,
                        ContactName = $"Contact-{i}",
                        ContactEmail = $"contact-{i}@example.com",
                        ContactPhone = $"+36-20-12345{i:0#}",
                    };
                    supplier.Save();
                }

                var nodes = CreateSafeContentQuery("+TypeIs:Supplier +Index:>3 +Index:<8 .SORT:Name").Execute().Nodes;
                var allNames = string.Join(", ", nodes.Select(n => n.Name).ToArray());
                Assert.AreEqual("Supplier-4, Supplier-5, Supplier-6, Supplier-7", allNames);
            });
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidContentException))]
        public void SupplierIntegration_SaveInvalid()
        {
            Test(() =>
            {
                InstallSupplierContentType();

                var root = new SystemFolder(Repository.Root);
                root.Save();

                var supplier = new Supplier(root)
                {
                    Name = $"Supplier-1",
                    // missing line: ContactName = "Contact-1",
                    ContactEmail = "contact-1@example.com",
                    ContactPhone = "+36-20-1234576",
                };
                supplier.Save();
            });
        }

        /* ================================================================= Tools */

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
