using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelStates {

  public class BaseState : State {

    #region Fields

    protected WaveController waveController;
    protected PlayerSpawner playerSpawner;
    protected HUDController hudController;
    protected GameObject player;

    private LevelController levelController;

    #endregion

    #region Mono Behaviour

    void Awake() {
      levelController = GetComponent<LevelController>();
      waveController = levelController.WaveController;
      playerSpawner = levelController.PlayerSpawner;
      hudController = levelController.HUDController;
      player = levelController.Player;
    }

    #endregion
  	
  }

}