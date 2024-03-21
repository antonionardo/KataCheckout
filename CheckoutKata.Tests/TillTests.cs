using CheckoutKata.Interfaces;
using NUnit.Framework;

namespace CheckoutKata.Tests
{
    [TestFixture]
    public class TillTests
    {
        ITill till;

        public TillTests()
        {
            till = new Till();
        }
    }
}
