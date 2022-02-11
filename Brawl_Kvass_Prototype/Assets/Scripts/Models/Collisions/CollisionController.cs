using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;

namespace Models.Collisions
{
    public class CollisionController
    {
        private Collisions _collisions = new Collisions();
        private readonly IEnumerable<IRecord> _startCollideRecordsProvider;

        public CollisionController(IEnumerable<IRecord> startCollideRecords)
        {
            _startCollideRecordsProvider = startCollideRecords;
        }

        public void Update()
        {
            foreach (var pair in _collisions.CollisionPairs)
                TryCollide(pair);

            _collisions = new Collisions();
        }
        
        public void TryAddCollision(object modelA, object modelB)
        {
            _collisions.TryBind(modelA, modelB);
        }

        private void TryCollide((object, object) pair)
        {
            IEnumerable<IRecord> records = _startCollideRecordsProvider.Where(record => record.IsTarget(pair));

            foreach (var record in records)
                ((dynamic) record).Do((dynamic) pair.Item1, (dynamic) pair.Item2);
        }
    }
}