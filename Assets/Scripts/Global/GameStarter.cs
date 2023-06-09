using UnityEngine;
using Zenject;
namespace Ulf
{
    public class GameStarter : MonoBehaviour
    {
        // Start is called before the first frame update
        void Awake()
        {
            
            DiContainer diContainer = new DiContainer();

            diContainer.Bind<IRegister<IRound>>().AsSingle();
        }
    }
}