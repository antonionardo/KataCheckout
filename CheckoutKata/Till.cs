using CheckoutKata.Interfaces;
using CheckoutKata.Models;

namespace CheckoutKata
{
    public class Till : ITill
    {
        private IEnumerable<Item> _existingShopItems;

        public List<Item> _scannedItems;

        public Till(IEnumerable<Item> existingShopItems)
        {
            _existingShopItems = existingShopItems;

            _scannedItems = new List<Item>();
        }

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public void Scan(Item item)
        {
            if (item is not null && CheckItemExistsInTheShop(item))
            {
                _scannedItems.Add(item);
            }
        }

        private bool CheckItemExistsInTheShop(Item item)
        {
            return _existingShopItems.Contains(item);
        }
    }
}
