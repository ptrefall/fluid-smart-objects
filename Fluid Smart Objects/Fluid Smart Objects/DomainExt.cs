using FluidHTN;

namespace Fluid
{
    public static class DomainExt
    {
        public static bool TrySetSlotDomain(this Domain<AIContext> domain, AIDomainSlots slot,
            Domain<AIContext> subDomain)
        {
            return domain.TrySetSlotDomain((int) slot, subDomain);
        }

        public static void ClearSlot(this Domain<AIContext> domain, AIDomainSlots slot)
        {
            domain.ClearSlot((int) slot);
        }

        public static DomainBuilder<AIContext> Slot(this DomainBuilder<AIContext> builder, AIDomainSlots slot)
        {
            return builder.Slot((int) slot);
        }
    }
}
