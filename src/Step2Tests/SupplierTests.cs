using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Step2;

namespace Step2Tests
{
    [TestClass]
    public class SupplierTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Supplier2_ContactName_Null()
        {
            var supplier = new Supplier();
            supplier.ContactName = null;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Supplier2_ContactName_Empty()
        {
            var supplier = new Supplier();
            supplier.ContactName = string.Empty;
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Supplier2_ContactEmail_Null()
        {
            var supplier = new Supplier();
            supplier.ContactEmail = null;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Supplier2_ContactEmail_Empty()
        {
            var supplier = new Supplier();
            supplier.ContactEmail = string.Empty;
        }


        [TestMethod]
        public void Supplier2_Phone_Valid()
        {
            var supplier = new Supplier();

            supplier.ContactPhone = "+36 20 123 45 67";
            Assert.AreEqual("201234567", supplier.ContactPhone);

            supplier.ContactPhone = "06 20 123 45 67";
            Assert.AreEqual("201234567", supplier.ContactPhone);

            supplier.ContactPhone = "06-20-123-45-67";
            Assert.AreEqual("201234567", supplier.ContactPhone);

            supplier.ContactPhone = "06-20  123-4567";
            Assert.AreEqual("201234567", supplier.ContactPhone);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Supplier2_Phone_Invalid_Null()
        {
            var supplier = new Supplier();
            supplier.ContactPhone = null;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Supplier2_Phone_Invalid_Empty()
        {
            var supplier = new Supplier();
            supplier.ContactPhone = string.Empty;
        }
        [TestMethod]
        public void Supplier2_Phone_WrongPrefix()
        {
            try
            {
                var supplier = new Supplier { ContactPhone = "+99 20 123 4567" };
                Assert.Fail("Exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("+36"));
                Assert.IsTrue(e.Message.Contains("06"));
            }
        }
        [TestMethod]
        public void Supplier2_Phone_WrongSeparator()
        {
            try
            {
                var supplier = new Supplier { ContactPhone = "06 20/123 4567" };
                Assert.Fail("Exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("separator"));
            }
        }
        [TestMethod]
        public void Supplier2_Phone_TooShort1()
        {
            try
            {
                var supplier = new Supplier { ContactPhone = "06" };
                Assert.Fail("Exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("too short"));
            }
        }
        [TestMethod]
        public void Supplier2_Phone_TooShort2()
        {
            try
            {
                var supplier = new Supplier { ContactPhone = "06 20 123 456" };
                Assert.Fail("Exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("too short"));
            }
        }
        [TestMethod]
        public void Supplier2_Phone_TooLong()
        {
            try
            {
                var supplier = new Supplier { ContactPhone = "06 20 123 45678" };
                Assert.Fail("Exception was not thrown.");
            }
            catch (ArgumentException e)
            {
                Assert.IsTrue(e.Message.Contains("too long"));
            }
        }
    }
}
