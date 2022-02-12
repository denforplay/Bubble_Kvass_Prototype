using Core;
using Core.Abstracts;
using Models.Concretes;

namespace Models.Systems
{
    public class BarrierSystem : SystemBase<Barrier>
    {
        public void Work(Barrier barrier)
        {
            Entity<Barrier> entity = new Entity<Barrier>(barrier, barrier);
            Work(entity);
        }
        
        public override void Update(float deltaTime)
        {
        }
    }
}