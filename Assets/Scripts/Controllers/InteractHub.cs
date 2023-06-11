using Assets.Scripts.Interfaces;

namespace Ulf
{
    public class InteractHub
    {
        IRegister<IMovable> _registerMovable;
        IRegister<IControl> _registerControl;
        IRegister<IRound> _registerRound;

        public InteractHub(IRegister<IMovable> registerMovable, IRegister<IControl> registerControl, IRegister<IRound> registerRound)
        {
            _registerMovable = registerMovable;
            _registerControl = registerControl;
            _registerRound = registerRound;
        }
    }
}
