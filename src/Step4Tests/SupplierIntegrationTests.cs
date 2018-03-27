using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SenseNet.ContentRepository;
using SenseNet.ContentRepository.Schema;
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
