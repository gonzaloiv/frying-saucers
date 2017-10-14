using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class BaseState : State {

        #region Fields

        protected GameData gameData;
        protected GameConfigData gameConfigData;
        protected GameController gameController;
        protected LevelController levelController;
        protected GameObject inputManager;
        protected GameObject mainMenuScreen;
        protected GameObject levelScreen;
        protected GameObject gameOverScreen;
        protected GameObject leaderboardScreen;
        protected GameObject creditsScreen;

        #endregion

        #region Mono Behaviour

        void Awake () {
            gameController = GetComponent<GameController>();
            gameData = gameController.GameData;
            gameConfigData = gameController.GameConfigData;
            levelController = gameController.LevelController;
            inputManager = gameController.InputManager;
            mainMenuScreen = gameController.MainMenuScreen;
            levelScreen = gameController.LevelScreen;
            gameOverScreen = gameController.GameOverScreen;
            leaderboardScreen = gameController.LeaderboardScreen;
            creditsScreen = gameController.CreditsScreen;
        }

        #endregion

        #region Protected Behaviour

        protected LevelData GetCurrentLevelData () {
            return gameController.CurrentLevelData;
        }

        protected LevelData GetTutorialLevelData () {
            return gameController.TutorialLevelData;   
        }

        #endregion

    }

}