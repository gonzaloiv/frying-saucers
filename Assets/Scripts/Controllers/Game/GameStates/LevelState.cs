using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class LevelState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            levelController.gameObject.SetActive(true);
            levelController.ToInitState(GetRandomLevelData());
            levelScreen.SetActive(true);
        }

        #endregion

    }

}