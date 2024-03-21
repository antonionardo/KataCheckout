namespace CheckoutKata.Interfaces
{
    public interface ITill
    {
        void Scan(string item);

        int GetTotalPrice();
    }
}
