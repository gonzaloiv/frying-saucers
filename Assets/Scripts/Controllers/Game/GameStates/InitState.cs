using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class InitState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            DataManager.Init();
        }

        public void OnDataLoadedEvent (DataLoadedEvent dataLoadedEvent) {
            if (dataLoadedEvent.Leaderboard.HasBeenTutorialPlayed) {
                gameController.ToMainMenuState();
            } else {
                // TODO: gameController.ToTutorialState();
                gameController.ToMainMenuState();   
            }
        }

        #endregion

        #region Protected Behaviour

        protected override void AddListeners () {
            EventManager.StartListening<DataLoadedEvent>(OnDataLoadedEvent);
        }

        protected override void RemoveListeners () {
            EventManager.StopListening<DataLoadedEvent>(OnDataLoadedEvent);
        }

        #endregion

    }

}