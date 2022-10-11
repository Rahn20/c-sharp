using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegisterSellerConsole.src;

namespace RegisterSellerConsole.Unittests
{
    [TestClass]
    public class ValidationsTests
    {

        [TestMethod]
        public void ValidateData_CorrectData_ReturnsTrue()
        {
            // act
            var actual = Validations.ValidateData(name: "Test 1", id: "9999999999", district: "Stockholm", items: "150");

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ValidateData_EmptyField_ValidatorsException()
        {
            // arrange and act
            void action() => Validations.ValidateData(name: "", id: "9999999999", district: "Stockholm", items: "150");

            // assert
            var exception = Assert.ThrowsException<ValidatorsException>(action);

            // Checking exception message
            Assert.AreEqual("\nDet bör inte finnas tomma fält. Fyll i namn, personnummer, distrikt och antal sålda artiklar.\nFörsök igen!", exception.Message);
        }

        [TestMethod]
        public void ValidatePersonNr_NotValid_ValidatorsException()
        {
            // arrange and act
            void action() => Validations.ValidatePersonNr("2222222");

            // assert
            var exception = Assert.ThrowsException<ValidatorsException>(action);

            // Checking exception message
            Assert.AreEqual("\nPersonnummer bör bestå av 10 siffror, Försök igen!", exception.Message);
        }

        [TestMethod]
        public void ValidatePersonNr_ValidPersonNr_ReturnsTrue()
        {
            // act
            var actual = Validations.ValidatePersonNr("2222222222");

            // assert
            Assert.IsTrue(actual);
        }
    }
}
