using CheckoutKata.Interfaces;
using CheckoutKata.Models;

namespace CheckoutKata
{
    public class Till : ITill
    {
        public Dictionary<char, (Item item, ItemTracker count)> _scannedItems;

        private IEnumerable<Item> _existingShopItems;
        private IEnumerable<SpecialOffer> _existingSpecialOffers;

        public Till(IEnumerable<Item> existingShopItems, IEnumerable<SpecialOffer> specialOffers)
        {
            _scannedItems = new Dictionary<char, (Item item, ItemTracker count)>();

            _existingShopItems = existingShopItems;
            _existingSpecialOffers = specialOffers;
        }

        public int GetTotalPrice()
        {
            var totalPrice = 0;

            foreach(var shopItem in _scannedItems.Values)
            {
                var itemQuantity = shopItem.count.ScannedCount;
                var itemFullPrice = shopItem.item.UnitPrice;
                var specialOffer = _existingSpecialOffers.FirstOrDefault(x => x.StockKeepingUnit == shopItem.item.StockKeepingUnit);

                if (specialOffer is not null)
                {
                    var discountedItems = itemQuantity / specialOffer.QuantityNeeded;
                    var fullPriceItems = itemQuantity % specialOffer.QuantityNeeded;

                    totalPrice += discountedItems * specialOffer.SpecialPrice;
                    totalPrice += fullPriceItems * itemFullPrice;
                }
                else
                {
                    totalPrice += itemQuantity * itemFullPrice;
                }
            }

            return totalPrice;
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
