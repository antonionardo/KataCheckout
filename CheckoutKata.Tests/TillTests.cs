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

        [Test]
        public void Scan_NullShopItem_ShouldReturnZeroItemsScanned()
        {
            // Arrange
            Item nullItem = null;

            // Act
            _till.Scan(nullItem);

            // Assert
            Assert.That(_till._scannedItems.Count(), Is.EqualTo(0));
        }

        [Test]
        public void Scan_NonExistingShopItem_ShouldReturnZeroItemsScanned()
        {
            // Arrange
            var fakeItem = new Item
            {
                StockKeepingUnit = 'Z',
                UnitPrice = 100
            };

            // Act
            _till.Scan(fakeItem);

            // Assert
            Assert.That(_till._scannedItems.Count(), Is.EqualTo(0));
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
