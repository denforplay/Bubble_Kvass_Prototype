using Core.Abstracts;
using Models;
using UnityEngine;

namespace Views.Factories
{
    public class BarrierFactory : Transformable2DFactoryBase<Barrier>
    {
        [SerializeField] private Transformable2DView _defaultBarrier;
        
        protected override Transformable2DView GetEntity(Barrier entity)
        {
            return _defaultBarrier;
        }
    }
}