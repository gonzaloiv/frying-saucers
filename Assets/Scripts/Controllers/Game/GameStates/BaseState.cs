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
        protected Camera gameCamera;
        protected GameObject mainMenuScreen;
        protected GameObject levelScreen;
        protected GameObject gameOverScreen;
        protected GameObject leaderboardScreen;
        protected GameObject creditsScreen;
        protected GameObject tutorialScreen;

        #endregion

        #region Mono Behaviour

        void Awake () {
            gameController = GetComponent<GameController>();
            gameData = gameController.GameData;
            gameConfigData = gameController.GameConfigData;
            levelController = gameController.LevelController;
            gameCamera = gameController.GameCamera;
            mainMenuScreen = gameController.MainMenuScreen;
            levelScreen = gameController.LevelScreen;
            gameOverScreen = gameController.GameOverScreen;
            leaderboardScreen = gameController.LeaderboardScreen;
            creditsScreen = gameController.CreditsScreen;
            tutorialScreen = gameController.TutorialScreen;
        }

        #endregion

        #region Protected Behaviour

        protected LevelData GetRandomLevelData () {
            return gameController.RandomLevelData;
        }

        protected LevelData GetTutorialLevelData () {
            return gameController.TutorialLevelData;   
        }

        #endregion

    }

}