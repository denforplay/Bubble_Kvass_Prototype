using Core.Abstracts;
using Models;
using Models.Collisions;
using UnityEngine;

namespace Views.Factories
{
    public class PlatformFactory : Transformable2DFactoryBase<Platform>
    {
        [SerializeField] private Transformable2DView _defaultPlatform;
        
        protected override Transformable2DView GetEntity(Platform entity)
        {
            return _defaultPlatform;
        }
    }
}