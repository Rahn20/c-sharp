using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegisterSellerConsole.src;
using System.IO;


namespace RegisterSellerConsole.Unittests
{
    [TestClass]
    public class RegisterSellerTests
    {
        private RegisterSeller registerSeller = new RegisterSeller("test.csv");
        private string path = Directory.GetCurrentDirectory().Replace("\\bin\\Debug", "") + "\\test.csv";

        [TestInitialize]
        public void Startup()
        {
            // Register a new seller before each test.
            registerSeller.AddSeller(name: "Test 1", personNr: "9999999999", district: "Stockholm", soldItems: 150);
        }


        [TestCleanup()]
        public void TearDown()
        {
            // Remove the file after each test.
            File.Delete(path);
        }


        [TestMethod]
        public void AddSeller_FileDoesNotExist_TwoRowsInFile()
        {
            // Arrange
            string firstRow = "Namn,Personnummer,Distrikt,Antal";
            string secondRow = "Test 1,9999999999,Stockholm,150";

            // Act
            string[] lines = File.ReadAllLines(path);
            var sellers = registerSeller.Sellers;

            // Assert
            Assert.AreEqual(lines[0], firstRow);
            Assert.AreEqual(lines[1], secondRow);
            Assert.AreEqual(sellers.Count, 1);
        }

        [TestMethod]
        public void AddSeller_FileAlreadyExists_ThreeRowsInFile()
        {
            string firstRow = "Namn,Personnummer,Distrikt,Antal";
            string secondRow = "Test 1,9999999999,Stockholm,150";
            string thirdRow = "Test 2,1111111111,Stockholm,200";

            registerSeller.AddSeller(name: "Test 2", personNr: "1111111111", district: "Stockholm", soldItems: 200);
            string[] lines = File.ReadAllLines(path);
            var sellers = registerSeller.Sellers;

            // Assert and check file contents
            Assert.AreEqual(lines[0], firstRow);
            Assert.AreEqual(lines[1], secondRow);
            Assert.AreEqual(lines[2], thirdRow);
            Assert.AreEqual(sellers.Count, 2);
        }

        [TestMethod]
        public void AddToList_FileAlreadyExists_OneSellerInList()
        {
            registerSeller.AddToList();
            var sellers = registerSeller.Sellers;

            // Assert, check list result
            Assert.AreEqual(sellers[0].Name, "Test 1");
            Assert.AreEqual(sellers[0].PersonNr, "9999999999");
            Assert.AreEqual(sellers[0].District, "Stockholm");
            Assert.AreEqual(sellers[0].SoldItems, 150);
        }

        [TestMethod]
        public void CheckPersonNr_PersonNrExists_ReturnsTrue()
        {
            bool actual = registerSeller.CheckPersonNr("9999999999");

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CheckPersonNr_PersonNrDoesNotExists_ReturnsFalse()
        {
            // Act
            bool actual = registerSeller.CheckPersonNr("1111111111");

            // Assert
            Assert.IsFalse(actual);
        }


        [TestMethod]
        public void SortBySolditems_TwoSellers_SmallToLarge()
        {
            registerSeller.AddSeller(name: "Test 2", personNr: "1111111111", district: "Stockholm", soldItems: 50);

            registerSeller.SortBySolditems(registerSeller.Sellers, (registerSeller.Sellers).Count);
            var sellers = registerSeller.Sellers;

            Assert.AreEqual(sellers[0].PersonNr, "1111111111");
            Assert.AreEqual(sellers[1].PersonNr, "9999999999");
        }

        [TestMethod]
        public void GetItemLevels_TwoLevels_LevelTwoAndThree()
        {
            registerSeller.AddSeller(name: "Test 2", personNr: "1111111111", district: "Stockholm", soldItems: 50);

            var actual = registerSeller.GetItemLevels();

            Assert.AreEqual(actual["under 50 artiklar"], 0);
            Assert.AreEqual(actual["50-99 artiklar"], 1);
            Assert.AreEqual(actual["100-199 artiklar"], 1);
            Assert.AreEqual(actual["över 199 artiklar"], 0);
        }
    }
}