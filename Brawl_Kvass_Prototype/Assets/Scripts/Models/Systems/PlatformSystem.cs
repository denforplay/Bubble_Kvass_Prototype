using Core;
using Core.Abstracts;

namespace Models.Systems
{
    public class PlatformSystem : SystemBase<Platform>
    {
        public void Work(Platform platform)
        {
            Entity<Platform> entity = new Entity<Platform>(platform, platform);
            Work(entity);
        }
        
        public override void Update(float deltaTime)
        {
        }
    }
}