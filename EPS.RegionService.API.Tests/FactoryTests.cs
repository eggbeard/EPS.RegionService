using EPS.RegionService.Repository.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EPS.RegionService.API.Tests
{
    [TestClass]
    public class FactoryTests
    {
        [TestMethod]
        public void TestEntityRegionFactory()
        {
            //Arrange
            RegionFactory regionFactory = new RegionFactory();
            DTO.Region input = new DTO.Region()
            {
                RegionID = 1,
                Name = "Test Name",
                ZipCodes = { "06108","06109","06110","06115"}
            };

            //Act
            Repository.Entities.Region actual = regionFactory.CreateRegion(input);

            //Assert
            Assert.AreEqual(1, actual.Id);
            Assert.AreEqual("Test Name", actual.Name);
            Assert.AreEqual(2, actual.ZipCodes.Count);

            Assert.AreEqual(actual.ZipCodes[0].Start, 6108);
            Assert.AreEqual(actual.ZipCodes[0].End, 6110);
            Assert.AreEqual(actual.ZipCodes[1].Start, 6115);
            Assert.AreEqual(actual.ZipCodes[1].End, 6115);

        }
    }
}
