using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SenseNet.ContentRepository;
using SenseNet.ContentRepository.Schema;
using SenseNet.Tests;
using Step4;

namespace Step4Tests
{
    [TestClass]
    public class SupplierTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Supplier_ContactName_Null()
        {
            ValidateSupplierName(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Supplier_ContactName_Empty()
        {
            ValidateSupplierName(string.Empty);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Supplier_ContactEmail_Null()
        {
            ValidateSupplierEmail(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Supplier_ContactEmail_Empty()
        {
            ValidateSupplierEmail(string.Empty);
        }
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Supplier_ContactEmail_Invalid()
        {
            ValidateSupplierEmail("@");
        }


        [TestMethod]
        public void Supplier_ContactPhone_Valid()
        {
            Assert.AreEqual("+36201234567", GetValidSupplierPhone("+36 20 123 45 67"));
            Assert.AreEqual("+36201234567", GetValidSupplierPhone("06 20 123 45 67"));
            Assert.AreEqual("+36201234567", GetValidSupplierPhone("06-20-123-45-67"));
            Assert.AreEqual("+36201234567", GetValidSupplierPhone("06-20  123-4567"));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Supplier_ContactPhone_Invalid_Null()
        {
            GetValidSupplierPhone(null);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Supplier_ContactPhone_Invalid_Empty()
        {
            GetValidSupplierPhone(string.Empty);
        }
        [TestMethod]
        public void Supplier_ContactPhone_WrongPrefix()
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
        public void Supplier_ContactPhone_WrongSeparator()
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
        public void Supplier_ContactPhone_TooShort1()
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
        public void Supplier_ContactPhone_TooShort2()
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
        public void Supplier_ContactPhone_TooLong()
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
    }
}
