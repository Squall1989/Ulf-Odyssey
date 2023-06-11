using System;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Ulf
{
    
    public class GameStarter : MonoBehaviour
    {
        public Action<GameType> OnGameType;

        [Inject] GameOptions gameOptions;

        public void SetupSingle()
        {
            gameOptions.SetGameType(GameType.single);
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);

        }
    }
}