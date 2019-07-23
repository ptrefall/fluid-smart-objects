using System;
using System.Threading.Tasks;
using FluidHTN;
using TaskStatus = FluidHTN.TaskStatus;

namespace Fluid
{
    public class Player
    {
        private Planner<AIContext> _planner;
        private AIContext _context;
        private Domain<AIContext> _domain;

        public AIContext Context => _context;

        public Player(World world)
        {
            _planner = new Planner<AIContext>();
            _context = new AIContext(this, world);
            _context.Init();

            _domain = BuildDomain();
        }

        public void Think()
        {
            _planner.Tick(_domain, _context);

            if (_context.LogDecomposition)
            {
                Console.WriteLine("---------------------- DECOMP LOG --------------------------");
                while (_context.DecompositionLog?.Count > 0)
                {
                    var entry = _context.DecompositionLog.Dequeue();
                    var depth = FluidHTN.Debug.Debug.DepthToString(entry.Depth);
                    Console.ForegroundColor = entry.Color;
                    Console.WriteLine($"{depth}{entry.Name}: {entry.Description}");
                }
                Console.ResetColor();
                Console.WriteLine("-------------------------------------------------------------");
            }
        }

        public void Subscribe(SmartObject smartObject)
        {
            _domain.TrySetSlotDomain(smartObject.Slot, smartObject.Domain);
        }

        public void Unsubscribe(SmartObject smartObject)
        {
            _domain.ClearSlot(smartObject.Slot);
        }

        private Domain<AIContext> BuildDomain()
        {
            return new DomainBuilder<AIContext>("Player")
                .Slot(AIDomainSlots.HighPriority)
                .Select("Walk about")
                    .Action("Walk about action")
                        .Condition("At location 0", context => context.GetState(AIWorldState.Location) == 0)
                        .Do(OnArriveAtLocation)
                        .Effect("At location 1", EffectType.PlanAndExecute, (context, type) => { context.SetState(AIWorldState.Location, 1, type); })
                    .End()
                .End()
                .Select("Idle")
                    .Action("Idle action")
                        .Do(OnIdle)
                    .End()
                .End()
                .Build();
        }

        private TaskStatus OnArriveAtLocation(AIContext context)
        {
            Console.WriteLine("Arrived at location 1");
            context.Player.Subscribe(context.World.ImportantThing);
            return TaskStatus.Success;
        }

        private TaskStatus OnIdle(AIContext context)
        {
            Console.WriteLine("Idle");
            return TaskStatus.Continue;
        }
    }
}
