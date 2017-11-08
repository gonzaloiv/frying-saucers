using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveStates {

    public class BaseState : State {

        #region Fields

        protected WaveController waveController;
        protected GameObject gestureManager;
        protected GameObject player;
        protected EnemyTypeLabelSpawner enemyTypeLabelSpawner;
        protected WaveSpawner waveSpawner;
        protected Wave currentWave;

        #endregion

        #region Mono Behaviour

        void Awake () {
            waveController = GetComponent<WaveController>();
            gestureManager = waveController.GestureManager;
            player = waveController.Player;
            enemyTypeLabelSpawner = waveController.EnemyTypeLabelSpawner;
            waveSpawner = waveController.WaveSpawner;
            currentWave = waveController.CurrentWave;
        }

        #endregion

    }

}