using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class BaseState : State {

        #region Fields

        protected GameController gameController;
        protected LevelController levelController;
        protected GameObject inputManager;
        protected GameObject mainMenuScreen;
        protected GameObject levelScreen;
        protected GameObject gameOverScreen;
        protected GameObject leaderboardScreen;
        protected GameObject pauseScreen;

        #endregion

        #region Mono Behaviour

        void Awake () {
            gameController = GetComponent<GameController>();
            levelController = gameController.LevelController;
            inputManager = gameController.InputManager;
            mainMenuScreen = gameController.MainMenuScreen;
            levelScreen = gameController.LevelScreen;
            gameOverScreen = gameController.GameOverScreen;
            leaderboardScreen = gameController.LeaderboardScreen;
            pauseScreen = gameController.PauseScreen;
        }

        #endregion

        #region Protected Behaviour

        protected LevelData GetCurrentLevelData () {
            return gameController.CurrentLevelData;
        }

        #endregion

    }

}