using System;
using FluidHTN;

namespace Fluid
{
    public class SmartObject
    {
        private string _name;
        private Domain<AIContext> _domain;
        private AIDomainSlots _slot;

        public string Name => _name;
        public Domain<AIContext> Domain => _domain;
        public AIDomainSlots Slot => _slot;

        public SmartObject(string name, AIDomainSlots slot)
        {
            _name = name;
            _slot = slot;
            _domain = BuildDomain();
        }

        private Domain<AIContext> BuildDomain()
        {
            return new DomainBuilder<AIContext>(_name)
                .Select("Walk away")
                    .Action("Walk away")
                        .Condition("At location 1", context => context.GetState(AIWorldState.Location) == 1)
                        .Do(OnArriveAtLocation)
                        .Effect("At location 2", EffectType.PlanAndExecute, (context, type) => { context.SetState(AIWorldState.Location, 2, type); })
                    .End()
                .End()
                .Build();
        }

        private TaskStatus OnArriveAtLocation(AIContext context)
        {
            Console.WriteLine("Arrived at location 2");
            context.Player.Unsubscribe(context.World.ImportantThing);
            return TaskStatus.Success;
        }
    }
}
