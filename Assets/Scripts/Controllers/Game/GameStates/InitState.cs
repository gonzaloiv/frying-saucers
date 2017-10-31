using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class InitState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            new Board();
            GameConfig.Init(gameConfigData);
            levelController.gameObject.SetActive(false);
            inputManager.SetActive(false);
            DataManager.Init(); // Has to be called the last because of DataLoadedEvent.
        }

        public void OnDataLoadedEvent (DataLoadedEventArgs dataLoadedEventArgs) {
            if (dataLoadedEventArgs.TotalPlaysAmount > 0) {
                gameController.ToMainMenuState();
            } else {
                gameController.ToMainMenuState(); // TODO: gameController.ToTutorialState();
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