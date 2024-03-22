using CheckoutKata.Models;
using NUnit.Framework;

namespace CheckoutKata.Tests
{
    [TestFixture]
    public class TillTests
    {
        private Till _till;
        private IEnumerable<Item> _existingShopItems;

        [OneTimeSetUp]
        public void SetUpExistingShopItems()
        {
            _existingShopItems = CreateExistingShopItems();
            
        }

        [SetUp]
        public void TillTestsSetup()
        {
            _till = new Till(_existingShopItems);
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
