using CheckoutKata.Models;

namespace CheckoutKata.Interfaces
{
    public interface ITill
    {
        void Scan(Item item);

        int GetTotalPrice();
    }
}
