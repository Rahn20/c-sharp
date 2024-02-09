using ChangeMoney.src;

namespace ChangeMoney.UnitTests
{
    public class ChangeMoneySystemTests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void ChangeMoneySystem_AmountLessThanPrice_ThrowArgumentException()
        {
            static void action() => new ChangeMoneySystem(price: 1000, amount: 500);

            var exception = Assert.Throws<ArgumentException>(action);

            // Checking exception message
            Assert.That(exception.Message, Is.EqualTo("FEL! - beloppet bör vara mer än priset."));
        }

        [Test]
        public void GetChangeResult_ChangeDoubleValue_ReturnsDict()
        {
            // Arrange
            ChangeMoneySystem changeMoney = new ChangeMoneySystem(500, 1500);
            int[] expected = [2, 0, 0, 0, 0, 0, 0, 0];

            // Act
            var actual = changeMoney.GetChangeResult();
            int count = 0;

            foreach (KeyValuePair<string, int> kv in actual)
            {
                // Assert
                Assert.That(kv.Value, Is.EqualTo(expected[count]));
                count++;
            }
        }

        [Test]
        public void GetChangeResult_ChangeThreeValues_ReturnsDict()
        {
            ChangeMoneySystem changeMoney = new ChangeMoneySystem(500, 850);
            int[] expected = [0, 1, 1, 1, 0, 0, 0, 0];

            var actual = changeMoney.GetChangeResult();
            int count = 0;

            foreach (KeyValuePair<string, int> kv in actual)
            {
                Assert.That(kv.Value, Is.EqualTo(expected[count]));
                count++;
            }
        }

        [Test]
        public void GetChangeResult_NoChange_ReturnsDict()
        {
            ChangeMoneySystem changeMoney = new ChangeMoneySystem(500, 500);
            int[] expected = [0, 1, 1, 1, 0, 0, 0, 0];

            var actual = changeMoney.GetChangeResult();
            int count = 0;

            // assert
            foreach (KeyValuePair<string, int> kv in actual)
            {
                Assert.That(kv.Value, Is.EqualTo(0));
                count++;
            }
        }
    }
}