namespace Fluid
{
    public class World
    {
        private SmartObject _importantThing;
        public SmartObject ImportantThing => _importantThing;

        public World()
        {
            _importantThing = new SmartObject("Important thing", AIDomainSlots.HighPriority);
        }
    }
}
