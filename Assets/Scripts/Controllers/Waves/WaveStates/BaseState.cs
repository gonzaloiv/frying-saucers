using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaveStates {

    public class BaseState : State {

        #region Fields

        protected WaveController waveController;
        protected GameObject gestureManager;
        protected EnemyTypeLabelSpawner enemyTypeLabelSpawner;
        protected WaveSpawner waveSpawner;
        protected GameObject player;
        protected PlayerController playerController;
        protected GameObject[] currentWaveEnemies;

        #endregion

        #region Mono Behaviour

        void Awake () {
            waveController = GetComponent<WaveController>();
            gestureManager = waveController.GestureManager;
            enemyTypeLabelSpawner = waveController.EnemyTypeLabelSpawner;
            waveSpawner = waveController.WaveSpawner;
            player = waveController.Player;
            playerController = player.GetComponent<PlayerController>();
            currentWaveEnemies = waveController.CurrentWaveEnemies;
        }

        #endregion

        #region Protected Behaviour

        protected WaveData GetCurrentWaveData() {
            return waveController.CurrentWaveData;
        }

        #endregion

    }

}