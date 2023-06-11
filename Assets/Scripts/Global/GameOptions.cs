
using System;

namespace Ulf
{
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