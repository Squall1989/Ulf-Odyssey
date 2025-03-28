using System;
using System.Collections.Generic;

namespace Ulf
{
    public class Health 
    {
        protected int _maxHealth;
        protected int _health;
        protected ElementType _element;

        public Action<int> OnHealthChange;
        private List<IKillable> _killables;

        public int MaxHealth => _maxHealth;
        public int CurrHealth => _health;
        public ElementType Element => _element;

        public Health(int maxHealth, ElementType element)
        {
            _element = element;
            _health = _maxHealth =  maxHealth;
        }

        public void ChangeHealth(int amount)
        {
            if(_health == 0 && amount > 0)
            {
                OnDeath(false);
            }

            _health = Math.Clamp(_health + amount, 0, _maxHealth);

            OnHealthChange?.Invoke(_health);
            if(_health <= 0)
            {
                OnDeath(true);
            }
        }

        private void OnDeath(bool isDead)
        {
            foreach (var killable in _killables)
            {
                if(isDead)
                    killable.Kill();
                else 
                    killable.Ressurect();
            }
        }

        internal void SetKillables(List<IKillable> killables)
        {
            _killables = killables;
        }
    }
}