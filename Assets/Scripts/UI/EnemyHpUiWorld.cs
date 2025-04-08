using System;
using UnityEngine;
using Zenject;

namespace Ulf
{
    public class EnemyHpUiWorld : MonoBehaviour
    {
        [Inject] private WorldView worldView;

        private void Start()
        {
            worldView.OnUnitDamaged += DamageUI;
        }

        private void DamageUI((int damage, int attackable, int attacker) tuple)
        {
            UnityEngine.Debug.Log(tuple);
        }
    }
}