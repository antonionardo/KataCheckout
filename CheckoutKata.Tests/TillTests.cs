using CheckoutKata.Models;
using NUnit.Framework;

namespace CheckoutKata.Tests
{
    [TestFixture]
    public class TillTests
    {
        Till _till;

        public TillTests()
        {
            var existingShopItems = CreateExistingShopItems();

            _till = new Till(existingShopItems);
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
