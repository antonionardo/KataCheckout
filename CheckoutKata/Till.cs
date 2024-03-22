using CheckoutKata.Interfaces;
using CheckoutKata.Models;

namespace CheckoutKata
{
    public class Till : ITill
    {
        public Dictionary<char, (Item item, ItemTracker count)> _scannedItems;

        private IEnumerable<Item> _existingShopItems;

        public Till(IEnumerable<Item> existingShopItems)
        {
            _scannedItems = new Dictionary<char, (Item item, ItemTracker count)>();

            _existingShopItems = existingShopItems;
        }

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public void Scan(char item)
        {
            if (CheckItemExistsInTheShop(item))
            {
                if (_scannedItems.ContainsKey(item))
                {
                    _scannedItems[item].count.ScannedCount++;
                }
                else
                {
                    var itemToAdd = _existingShopItems.First(x => x.StockKeepingUnit == item);
                    var itemScannedCount = new ItemTracker { ScannedCount = 1 };

                    _scannedItems.Add(item, (itemToAdd, itemScannedCount));
                }
            }
        }

        private bool CheckItemExistsInTheShop(char item)
        {
            return _existingShopItems.Any(x => x.StockKeepingUnit == item);
        }
    }
}
