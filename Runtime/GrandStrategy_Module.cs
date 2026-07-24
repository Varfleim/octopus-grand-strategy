
using GBB;

namespace GS
{
    internal class GrandStrategy_Module : GameModule
    {
        public override void Initialization()
        {
            mainAspect = new A_MainGrandStrategy();
        }
    }
}
