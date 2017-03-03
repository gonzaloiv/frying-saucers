using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelStates {

  public class BaseState : State {

    #region Fields

    protected WaveController waveController;
    protected PlayerSpawner playerSpawner;
    protected BackgroundController backgroundController;
    protected HUDController hudController;

    protected List<GameObject> currentLevelObjects;
    protected GameObject player;

    private LevelController levelController;

    #endregion

    #region Mono Behaviour

    void Awake() {

      levelController = GetComponent<LevelController>();
      waveController = levelController.WaveController;
      playerSpawner = levelController.PlayerSpawner;
      backgroundController = levelController.BackgroundController;
      hudController = levelController.HUDController;

      currentLevelObjects = levelController.CurrentLevelObjects;
      player = levelController.Player;

    }

    #endregion
  	
  }

}