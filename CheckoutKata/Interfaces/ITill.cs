using CheckoutKata.Models;

namespace CheckoutKata.Interfaces
{
    public interface ITill
    {
        void Scan(char item);

        int GetTotalPrice();
    }
}
