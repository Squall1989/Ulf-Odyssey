using UnityEngine;
using Zenject;

namespace Ulf
{
    public class ViewController : MonoBehaviour
    {
        [Inject] EnemyHpUi enemyHp;
        [Inject] ElementSpritesScriptable sprites;

        private void Awake()
        {
            
        }
    }
}