using System.Runtime.InteropServices;
using EdgeQ.Features;

namespace EdgeQ.Profiles
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public class PosProfile : Profile
    {
        // 
        // resolved features

        IRecognizer recog;

        IScale scale;

        IShopShow shopshow;

        IBillPrint billprint;

        public PosProfile() : base("pos", "农贸智能台秤",
            new[]
            {
                typeof(IScale)
            }
        )
        {
        }


        public void PrintBuyReceipt()
        {
        }
    }
}