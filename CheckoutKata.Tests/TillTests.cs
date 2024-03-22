using CheckoutKata.Models;
using NUnit.Framework;

namespace CheckoutKata.Tests
{
    [TestFixture]
    public class TillTests
    {
        private Till _till;
        private IEnumerable<Item> _existingShopItems;
        private IEnumerable<SpecialOffer> _specialPriceOffers;

        [OneTimeSetUp]
        public void SetUpExistingShopItems()
        {
            _existingShopItems = CreateExistingShopItems();
            _specialPriceOffers = CreateSpecialPriceOffers();
        }

        [SetUp]
        public void TillTestsSetup()
        {
            _till = new Till(_existingShopItems, _specialPriceOffers);
        }

        [TestCase("A", 1)]
        [TestCase("AAA", 3)]
        [TestCase("ABC", 3)]
        [TestCase("ABCCBA", 6)]
        [TestCase("DCBAABCD", 8)]
        public void Scan_ExistingShopItems_ShouldReturnTheCorrectCountOfItemsScanned(string items, int expectedScannedCount)
        {
            // Arrange
            var stockKeepingUnits = items.ToCharArray();

            // Act
            foreach(var stockKeepingUnit in stockKeepingUnits)
            {
                _till.Scan(stockKeepingUnit);
            }

            var totalItemsScanned = _till._scannedItems.Sum(x => x.Value.count.ScannedCount);

            // Assert
            Assert.That(totalItemsScanned, Is.EqualTo(expectedScannedCount));
        }

        [TestCase("Z", 0)]
        [TestCase("ZZZ", 0)]
        [TestCase("ABCZZZ", 3)]
        [TestCase("ZBZAZCZD", 4)]
        public void Scan_ExistingAndNonExistingShopItems_ShouldReturnTheCorrectCountOfItemsScanned(string items, int expectedScannedCount)
        {
            // Arrange
            var stockKeepingUnits = items.ToCharArray();

            // Act
            foreach(var stockKeepingUnit in stockKeepingUnits)
            {
                _till.Scan(stockKeepingUnit);
            }

            var totalItemsScanned = _till._scannedItems.Sum(x => x.Value.count.ScannedCount);

            // Assert
            Assert.That(totalItemsScanned, Is.EqualTo(expectedScannedCount));
        }

        [TestCase('A', 50)]
        [TestCase('B', 30)]
        [TestCase('C', 20)]
        [TestCase('D', 15)]
        public void GetTotalPrice_SingleShopItem_ShouldReturnTheCorrectTotalPrice(char item, int expectedTotalPrice)
        {
            // Act
            _till.Scan(item);

            var totalPrice = _till.GetTotalPrice();

            // Assert
            Assert.That(totalPrice, Is.EqualTo(expectedTotalPrice));
        }

        [TestCase("AAA", 130)]
        [TestCase("BB", 45)]
        public void GetTotalPrice_SpecialOfferItems_ShouldReturnTheCorrectTotalPrice(string items, int expectedTotalPrice)
        {
            // Arrange
            var stockKeepingUnits = items.ToCharArray();

            // Act
            foreach (var stockKeepingUnit in stockKeepingUnits)
            {
                _till.Scan(stockKeepingUnit);
            }

            var totalPrice = _till.GetTotalPrice();

            // Assert
            Assert.That(totalPrice, Is.EqualTo(expectedTotalPrice));
        }
        
        [TestCase("ABCD", 115)]
        [TestCase("DCBA", 115)]
        [TestCase("ZABZCDZ", 115)]
        [TestCase("ABABA", 175)]
        [TestCase("ABABABA", 255)]
        [TestCase("ZDBCAAB", 180)]
        [TestCase("ZDBCAAAB", 210)]
        public void GetTotalPrice_MultipleItemsBothWithAndWithoutOffers_ShouldReturnTheCorrectTotalPrice(string items, int expectedTotalPrice)
        {
            // Arrange
            var stockKeepingUnits = items.ToCharArray();

            // Act
            foreach (var stockKeepingUnit in stockKeepingUnits)
            {
                _till.Scan(stockKeepingUnit);
            }

            var totalPrice = _till.GetTotalPrice();

            // Assert
            Assert.That(totalPrice, Is.EqualTo(expectedTotalPrice));
        }

        private List<SpecialOffer> CreateSpecialPriceOffers()
        {
            return new List<SpecialOffer>
            {
                new SpecialOffer
                {
                    StockKeepingUnit = 'A',
                    SpecialPrice = 130,
                    QuantityNeeded = 3
                },
                new SpecialOffer
                {
                    StockKeepingUnit = 'B',
                    SpecialPrice = 45,
                    QuantityNeeded = 2
                },
            };
        }

        private List<Item> CreateExistingShopItems()
        {
            return new List<Item>
            {
                new Item
                {
                    StockKeepingUnit = 'A',
                    UnitPrice = 50
                },
                new Item
                {
                    StockKeepingUnit = 'B',
                    UnitPrice = 30
                },
                new Item
                {
                    StockKeepingUnit = 'C',
                    UnitPrice = 20
                },
                new Item
                {
                    StockKeepingUnit = 'D',
                    UnitPrice = 15
                }
            };
        }
    }
}
