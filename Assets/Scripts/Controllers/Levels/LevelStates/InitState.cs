using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace LevelStates {

    public class InitState : BaseState {

        #region State Behaviour

        public override void Enter () {
            player.Init(currentLevelData.PlayerInitialLives);
            playerController.gameObject.SetActive(true);
            playerController.Init(player);
            levelScreenController.gameObject.SetActive(true);
            levelScreenController.Init(player);
            levelController.ToWaveStartState();
        }

        #endregion

    }

}