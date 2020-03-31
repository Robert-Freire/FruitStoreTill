using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Till.Data;
using Till.Model;

namespace TillTest.Data
{
    [TestClass]
    public class TillContextTest
    {
        // private Mock<IFileSystem> fileSystemMock;
        private ITillContext tillContextUT;

        [TestInitialize]
        public void InitializeTest()
        {
            tillContextUT = new TillContext();
        }

        [TestMethod]
        public void LoadCsv_ValidLine_DataIsLoaded()
        {
            // Arrange
            var expectedFruit = new Fruit { Name = "some fruit", Price = 22.22m };
            var fileLine = $"{expectedFruit.Name}, {expectedFruit.Price}";
            var fileName = @"c:\test.csv";

            var fileSystemMock = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { fileName, new MockFileData(fileLine) },
            });

            // Action
            tillContextUT.LoadCsv(fileSystemMock, fileName);

            // Assert 
            Assert.IsTrue(tillContextUT.Fruits.Count(f => f.Name == expectedFruit.Name) == 1);
            Assert.IsTrue(tillContextUT.Fruits.Count(f => f.Price == expectedFruit.Price) == 1);

        }

        [TestMethod]
        public void LoadCsv_InValidLine_ExceptionIsThrow()
        {
            var fileName = @"c:\test.csv";

            var fileSystemMock = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { fileName, new MockFileData("this is not valid") },
            });

            Assert.ThrowsException<System.Exception>(() => tillContextUT.LoadCsv(fileSystemMock, fileName));
        }
    }
}