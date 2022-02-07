using Views;

namespace Inputs
{
    public class RunGameBarrierController
    {
        public Transformable2DView _view;

        public RunGameBarrierController(Transformable2DView view)
        {
            _view = view;
        }

        public void Update(float velocity)
        {
            if (_view != null)
                _view.SetHorizontalVelocity(velocity);
        }
    }
}