using Core.Abstracts;
using Models.Collisions;

namespace CompositeRoots
{
    public class CollisionsCompositeRoot : CompositeRoot
    {
        private CollisionController _controller;
        private CollisionRecords _records;
        public CollisionController Controller => _controller;
        
        public override void Compose()
        {
            _records = new CollisionRecords();
            _controller = new CollisionController(_records.StartCollideValues());
        }
        
        private void Update()
        {
            _controller.Update();
        }
    }
}