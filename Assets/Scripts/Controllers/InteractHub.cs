using Assets.Scripts.Interfaces;

namespace Ulf
{
    public abstract class InteractHub
    {
        protected IRegister<IMovable> _registerMovable;
        protected IRegister<IControl> _registerControl;
        protected IRegister<IRound> _registerRound;

        public InteractHub(IRegister<IMovable> registerMovable, IRegister<IControl> registerControl, IRegister<IRound> registerRound)
        {
            _registerMovable = registerMovable;
            _registerControl = registerControl;
            _registerRound = registerRound;
        }
    }
}
