using Views;

namespace Inputs
{
    public class RunGameBarrierController
    {
        private float _velocity;
        private Transformable2DView _view;

        public RunGameBarrierController(Transformable2DView view, float velocity)
        {
            _velocity = velocity;
            _view = view;
        }

        public void Update()
        {
            if (_view != null)
                _view.SetHorizontalVelocity(_velocity);
        }
    }
}