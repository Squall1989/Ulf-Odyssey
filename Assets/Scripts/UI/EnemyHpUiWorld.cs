using System;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Ulf
{
    public class EnemyHpUiWorld : PiecesAnimStatic
    {
        [SerializeField] private PiecesPool poolPieces;
        [Inject] private ElementSpritesScriptable sprites;
        [Inject] private WorldView worldView;
        [Inject] private GameState gameState;

        private void Start()
        {
            gameState.OnChangeCondition += GameStateChange;
            worldView.OnUnitDamaged += AnimDamageWorld;
            Init(poolPieces);
        }

        private void AnimDamageWorld(float2 startPos, float2 direct, ElementType type)
        {
            HealthDestroy(startPos, type, true);
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
    }
}