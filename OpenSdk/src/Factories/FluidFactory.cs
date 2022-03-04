using Fluid;

namespace OpenSdk.Factories
{
    public class FluidFactory : IFluidFactory
    {
        public FluidParser Create()
        {
            return new FluidParser();
        }
    }
}
