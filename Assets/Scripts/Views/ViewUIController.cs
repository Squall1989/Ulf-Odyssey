using System;
using UnityEngine;
using Zenject;

namespace Ulf
{
    public class ViewUIController : MonoBehaviour
    {
        [Inject] EnemyHpUi enemyHp;
        [Inject] EnemyHpUiWorld hpUiWorld;
        [Inject] PlayerView playerView;

        Unit _unitLookAt = null;

        private void Start()
        {
            enemyHp.InitHealth(new Health(0, ElementType.death));
            playerView.OnUnitSpotted += EnemySpotted;
        }

        private void EnemySpotted(Unit unit)
        {
            if(_unitLookAt != null)
            {
                _unitLookAt.Health.OnHealthChange -= enemyHp.ChangeHealth;
            }

            _unitLookAt = unit;

            if (unit == null)
            {
                enemyHp.InitHealth(new Health(0, ElementType.death));
                return;
            }

            enemyHp.InitHealth(unit.Health);

            unit.Health.OnHealthChange += enemyHp.ChangeHealth;

        }
    }
}