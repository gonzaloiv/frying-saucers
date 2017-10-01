using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates {

    public class LeaderboardState : BaseState {

        #region Public Behaviour

        public override void Enter () {
            base.Enter();
            leaderboardScreen.SetActive(true);
        }

        public override void Exit () {
            base.Exit();
            leaderboardScreen.SetActive(false);
        }

        #endregion

    }

}