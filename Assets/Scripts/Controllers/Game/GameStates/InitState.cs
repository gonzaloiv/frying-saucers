using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class InitState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            new Board(gameCamera);
            GameConfig.Init(gameConfigData);
            levelController.gameObject.SetActive(false);
            DataManager.Init(); // Called the last because of DataLoadedEvent.
        }

        public void OnDataLoadedEvent (DataLoadedEventArgs dataLoadedEventArgs) {
            if (dataLoadedEventArgs.TotalPlaysAmount > 0) {
                gameController.ToMainMenuState();
            } else {
                gameController.ToTutorialState(); // TODO: gameController.ToTutorialState();
            }
            DataManager.IncreaseUserDataTotalPlaysAmount();
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            DataManager.DataLoadedEvent += OnDataLoadedEvent;
        }

        protected override void RemoveListeners () {
            DataManager.DataLoadedEvent -= OnDataLoadedEvent;
        }

        #endregion

    }

}