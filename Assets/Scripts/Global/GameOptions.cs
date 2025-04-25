
using System;

namespace Ulf
{
    public class GameState
    {
        private GameCondition _condition;
        public GameCondition Condition 
        { 
            get => _condition; 
            set 
            { 
                _condition = value; 
                OnChangeCondition?.Invoke(_condition);
            }
        }
        public Action<GameCondition> OnChangeCondition;
    }

    public class GameOptions
    {
        private GameType gameType;
        public GameType GameType => gameType;
        public Action<GameType> OnGameTypeChange;

        public void SetGameType(GameType gameType)
        { 
            this.gameType = gameType;
            OnGameTypeChange?.Invoke(gameType);
        }

    }
}