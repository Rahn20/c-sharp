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
            // register a new seller before each test
            registerSeller.AddSeller(name: "Test 1", personNr: "9999999999", district: "Stockholm", soldItems: 150);
        }


        [TestCleanup()]
        public void TearDown()
        {
            // remove file after each test
            File.Delete(path);
        }


        [TestMethod]
        public void AddSeller_FileDoesNotExists_TwoRowsInFile()
        {
            // arrange/act
            string firstRow = "Namn,Personnummer,Distrikt,Antal";
            string secondRow = "Test 1,9999999999,Stockholm,150";
            string[] lines = File.ReadAllLines(path);
            var sellers = registerSeller.Sellers;

            // assert
            Assert.AreEqual(lines[0], firstRow);
            Assert.AreEqual(lines[1], secondRow);

            Assert.AreEqual(sellers.Count, 1);
        }

        [TestMethod]
        public void AddSeller_FileAlreadyExists_ThreeRowsInFile()
        {
            // arrange
            string firstRow = "Namn,Personnummer,Distrikt,Antal";
            string secondRow = "Test 1,9999999999,Stockholm,150";
            string thirdRow = "Test 2,1111111111,Stockholm,200";

            // act
            registerSeller.AddSeller(name: "Test 2", personNr: "1111111111", district: "Stockholm", soldItems: 200);
            string[] lines = File.ReadAllLines(path);
            var sellers = registerSeller.Sellers;

            // assert, check file contents
            Assert.AreEqual(lines[0], firstRow);
            Assert.AreEqual(lines[1], secondRow);
            Assert.AreEqual(lines[2], thirdRow);

            Assert.AreEqual(sellers.Count, 2);
        }

        [TestMethod]
        public void AddToList_FileAlreadyExists_OneSellerInList()
        {
            // act
            registerSeller.AddToList();
            var sellers = registerSeller.Sellers;

            // assert, check list result
            Assert.AreEqual(sellers[0].Name, "Test 1");
            Assert.AreEqual(sellers[0].PersonNr, "9999999999");
            Assert.AreEqual(sellers[0].District, "Stockholm");
            Assert.AreEqual(sellers[0].SoldItems, 150);
        }

        [TestMethod]
        public void CheckPersonNr_PersonNrExists_ReturnsTrue()
        {
            // act
            bool actual = registerSeller.CheckPersonNr("9999999999");

            // assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CheckPersonNr_PersonNrDoesNotExists_ReturnsFalse()
        {
            // act
            bool actual = registerSeller.CheckPersonNr("1111111111");

            // assert
            Assert.IsFalse(actual);
        }


        [TestMethod]
        public void SortBySolditems_TwoSellers_SmallerToLarger()
        {
            // arrange
            registerSeller.AddSeller(name: "Test 2", personNr: "1111111111", district: "Stockholm", soldItems: 50);

            // act
            registerSeller.SortBySolditems(registerSeller.Sellers, (registerSeller.Sellers).Count);
            var sellers = registerSeller.Sellers;

            // assert
            Assert.AreEqual(sellers[0].PersonNr, "1111111111");
            Assert.AreEqual(sellers[1].PersonNr, "9999999999");
        }

        [TestMethod]
        public void GetItemLevels_TwoLevels_LevelOneAndThree()
        {
            // arrange
            registerSeller.AddSeller(name: "Test 2", personNr: "1111111111", district: "Stockholm", soldItems: 50);

            // act
            var actual = registerSeller.GetItemLevels();

            // assert
            Assert.AreEqual(actual["under 50 artiklar"], 0);
            Assert.AreEqual(actual["50-99 artiklar"], 1);
            Assert.AreEqual(actual["100-199 artiklar"], 1);
            Assert.AreEqual(actual["över 199 artiklar"], 0);
        }
    }
}