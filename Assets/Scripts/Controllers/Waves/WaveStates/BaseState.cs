using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveStates {

    public class BaseState : State {

        #region Fields

        protected WaveController waveController;
        protected GameObject gestureManager;
        protected PlayerController playerController;
        protected EnemyGestureSpawner enemyGestureSpawner;
        protected WaveSpawner waveSpawner;
        protected Wave currentWave;

        #endregion

        #region Mono Behaviour

        void Awake () {
            waveController = GetComponent<WaveController>();
            gestureManager = waveController.GestureManager;
            playerController = waveController.Player.GetComponent<PlayerController>();
            enemyGestureSpawner = waveController.EnemyGestureSpawner;
            waveSpawner = waveController.WaveSpawner;
            currentWave = waveController.CurrentWave;
        }

        #endregion

    }

}