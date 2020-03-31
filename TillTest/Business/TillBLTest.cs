using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Till.BusinessLogic;
using Till.Data;
using Till.Model;

namespace TillTest.Business
{
    [TestClass]
    public class TillBLTest
    {
        // private Mock<IFileSystem> fileSystemMock;
        private ITillBL tillBLut;
        private Mock<ITillContext> tillContextMock;

        [TestInitialize]
        public void InitializeTest()
        {
            tillContextMock = new Mock<ITillContext>();
            tillBLut = new TillBL(tillContextMock.Object);
        }

        [TestMethod]
        public void AddInvoice_ValidInvoice_InvoiceIsAdded()
        {
            // Arrange
            var invoices = new List<Invoice>();
            tillContextMock.SetupGet(m => m.Invoices).Returns(invoices);

            // Action
            tillBLut.AddInvoice();

            // Assert 
            Assert.AreEqual(1, invoices.Count);
        }

        [TestMethod]
        public void AddInvoiceLine_ValidFruit_InvoiceLineIsAdded()
        {
            // Arrange
            var invoices = new List<Invoice>();
            var someFruit = new Fruit { Name = "some name", Price = 12 };
            var quantity = 2;

            var fruits = new List<Fruit>() { someFruit };
            tillContextMock.SetupGet(m => m.Invoices).Returns(invoices);
            tillContextMock.SetupGet(m => m.Fruits).Returns(fruits);

            // Action
            tillBLut.AddLine(someFruit.Name, quantity.ToString());

            // Assert 
            Assert.AreEqual(someFruit.Name, tillBLut.CurrentInvoice.Items.FirstOrDefault().Fruit.Name);
            Assert.AreEqual(someFruit.Price * quantity, tillBLut.CurrentInvoice.Items.FirstOrDefault().InvoiceRowAmount);
        }

        [TestMethod]
        public void AddInvoiceLine_InValidFruit_ExceptionIsThrow()
        {
            // Arrange
            var invoices = new List<Invoice>();
            var fruits = new List<Fruit>() { };

            tillContextMock.SetupGet(m => m.Invoices).Returns(invoices);
            tillContextMock.SetupGet(m => m.Fruits).Returns(fruits);

            // Action Assert
            Assert.ThrowsException<TillBLException>(() => tillBLut.AddLine("wrong fruit name", "23"));
        }
    }
}