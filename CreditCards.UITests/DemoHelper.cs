using System.Threading;
using System.Threading.Tasks;

namespace CreditCards.UITests
{
    internal static class DemoHelper
    { 
        public static void Pause(int secondToPause = 3000)
        {
            Thread.Sleep(secondToPause);
        }


    }
}
