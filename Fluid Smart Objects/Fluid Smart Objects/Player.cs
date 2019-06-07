using FluidHTN;

namespace Fluid
{
    public class Player
    {
        private Planner<AIContext> _planner;
        private AIContext _context;

        public AIContext Context => _context;

        public Player()
        {
            _planner = new Planner<AIContext>();
            _context = new AIContext(this);
            _context.Init();
        }

        public void Think(Domain<AIContext> domain)
        {
            _planner.Tick(domain, _context);
        }
    }
}
