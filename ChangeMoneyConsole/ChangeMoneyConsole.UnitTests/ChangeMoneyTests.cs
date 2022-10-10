using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ChangeMoneyConsole.src;
using System.Collections.Generic;

namespace ChangeMoneyConsole.UnitTests
{

    /**
     * <summary>
     *      This class contains some tests for the class ChangeMoney.
     * </summary>
     */
    [TestClass]
    public class ChangeMoneyTests
    {

        [TestMethod]
        public void Constructor_AmountLessThanPrice_ThrowArgumentException()
        {
            // arrange and act
            void action() => new ChangeMoney(price: 1000, amount: 500);

            // assert
            var exception = Assert.ThrowsException<ArgumentException>(action);

            // Checking exception message
            Assert.AreEqual("FEL! - beloppet bör vara mer än priset.", exception.Message);
        }


        [TestMethod]
        public void GetChangeResult_ChangeDoubleValue_ReturnsDic()
        {
            // arrange
            ChangeMoney changeMoney = new ChangeMoney(500, 1500);
            int[] expected = new int[] { 2, 0, 0, 0, 0, 0, 0, 0 };

            // act
            var actual = changeMoney.GetChangeResult();
            int count = 0;

            foreach (KeyValuePair<string, int> kv in actual)
            {
                // assert
                Assert.AreEqual(kv.Value, expected[count]);
                count++;
            }
        }


        [TestMethod]
        public void GetChangeResult_ChangeThreeValues_ReturnsDic()
        {
            // arrange
            ChangeMoney changeMoney = new ChangeMoney(500, 850);
            int[] expected = new int[] { 0, 1, 1, 1, 0, 0, 0, 0 };

            // act
            var actual = changeMoney.GetChangeResult();
            int count = 0;

            // assert
            foreach (KeyValuePair<string, int> kv in actual)
            {
                Assert.AreEqual(kv.Value, expected[count]);
                count++;
            }
        }

        [TestMethod]
        public void GetChangeResult_NoChange_ReturnsDic()
        {
            // arrange
            ChangeMoney changeMoney = new ChangeMoney(850, 850);

            // act
            var actual = changeMoney.GetChangeResult();
            int count = 0;

            // assert
            foreach (KeyValuePair<string, int> kv in actual)
            {
                Assert.AreEqual(kv.Value, 0);
                count++;
            }
        }
    }
}
