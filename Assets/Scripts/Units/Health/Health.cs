using System;

namespace Ulf
{

    public class Health 
    {
        protected int _maxHealth;
        protected int _health;
        protected ElementType _element;

        public Action<int> OnHealthChange;

        public Health(int maxHealth, ElementType element)
        {
            _element = element;
            _maxHealth = maxHealth;
        }

        public void ChangeHealth(int amount)
        {
            _health = Math.Clamp(_health + amount, 0, _maxHealth);

            OnHealthChange?.Invoke(_health);
        }
    }


}