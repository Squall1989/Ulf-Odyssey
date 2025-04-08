using System;
using UnityEngine;
using Zenject;

namespace Ulf
{
    public class EnemyHpUiWorld : MonoBehaviour
    {
        [Inject] private WorldView worldView;
        [Inject] private GameState gameState;

        private void Start()
        {
            gameState.OnChangeCondition += GameStateChange;
            worldView.OnUnitDamaged += DamageUI;
        }

        private void GameStateChange(GameCondition condition)
        {
            switch(condition)
            {
                case GameCondition.allIsReady:
                    worldView.Init();
                    break;
            }
        }

        private void DamageUI((int damage, int attackable, int attacker) tuple)
        {
            UnityEngine.Debug.Log(tuple);
        }
    }
}