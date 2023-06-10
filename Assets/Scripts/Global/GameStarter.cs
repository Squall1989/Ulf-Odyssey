using UnityEngine;

namespace Ulf
{
    
    public class GameStarter : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {

        }
    }
}